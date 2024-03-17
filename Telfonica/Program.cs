class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Alejandro Gomez (23-0573)\n\n---------------------Centralita Telefonica---------------------\n");
        Console.WriteLine(" Reporte de llamadas:\n");

        Centralita centralita = new Centralita();


        LlamadaLocal llamadaLocal = new LlamadaLocal("767772", "185436", 104, 0.15);
        LlamadaLocal llamadaLocal2 = new LlamadaLocal("033398", "761655", 3310, 0.15);
        LlamadaLocal llamadaLocal3 = new LlamadaLocal("148633", "676363", 301, 0.15);

        LlamadaProvincial llamadaProvincial = new LlamadaProvincial("822487", "198713", 152, 0.15, 0.20, 0.25, 5);
        LlamadaProvincial llamadaProvincial2 = new LlamadaProvincial("127634", "776536", 1708, 0.15, 0.20, 0.25, 5);
        LlamadaProvincial llamadaProvincial3 = new LlamadaProvincial("315777", "4543134", 340, 0.15, 0.20, 0.25, 5);



        centralita.AgregarLlamada(llamadaLocal);
        centralita.AgregarLlamada(llamadaLocal);

        centralita.AgregarLlamada(llamadaLocal2);
        centralita.AgregarLlamada(llamadaLocal2);

        centralita.AgregarLlamada(llamadaLocal3);
        centralita.AgregarLlamada(llamadaLocal3);

        centralita.AgregarLlamada(llamadaProvincial);
        centralita.AgregarLlamada(llamadaProvincial);

        centralita.AgregarLlamada(llamadaProvincial2);
        centralita.AgregarLlamada(llamadaProvincial2);

        centralita.AgregarLlamada(llamadaProvincial3);
        centralita.AgregarLlamada(llamadaProvincial3);


        double totalLocal = centralita.CalcularTotalLocal();
        double totalProvincial = centralita.CalcularTotalProvincial();

        Console.WriteLine($"    Hubo un total de {centralita.acum} llamadas. \n");

        Console.WriteLine($"    Total de llamadas locales: {totalLocal:C}");
        Console.WriteLine($"    Total de llamadas provinciales: {totalProvincial:C}");
        Console.WriteLine();
        Console.WriteLine($"    Total de todas las llamadas: {totalLocal + totalProvincial:C}\n\n\n\n");
    }
}



public abstract class Llamada
{

    protected string numOrigen;
    protected string numDestino;
    protected double duracion;

    public Llamada (string numOrigen, string numDestino, double duracion)
    {
        this.numOrigen = numOrigen;
        this.numDestino = numDestino;
        this.duracion = duracion;
    }


    public string NumDestino { get => numDestino; }
    public string NumOrigen { get => numOrigen; }
    

    public abstract double CalcularPrecio();
}

public class LlamadaProvincial:Llamada
{
    private double precio1;
    private double precio2;
    private double precio3;
    private int franja;

    public LlamadaProvincial(string numOrigen, string numDestino, double duracion, double precio1, double precio2, double precio3, int franja) : base(numOrigen, numDestino, duracion)
    {
        this.precio1 = precio1;
        this.precio2 = precio2;
        this.precio3 = precio3;
        this.franja = franja;
    }

    public override double CalcularPrecio()
    {
        if (duracion <= franja)
        {
            return duracion * precio1;
        }
        else if (duracion <= franja*2)
        {
            return duracion * precio2;
        }

        return duracion * precio3;

    }

}

public class LlamadaLocal : Llamada
{
    private double precio;

    public LlamadaLocal(string numOrigen, string numDestino, double duracion, double precio):base(numOrigen, numDestino, duracion)
    {
        this.precio = precio;
    }

    public override double CalcularPrecio()
    {
        return precio * duracion;
    }

}

public class Centralita
{
    private int cont;
    public int acum { get; private set; }

    private List<Llamada> llamadas;

    public Centralita()
    {
        llamadas = new List<Llamada>();
        acum = 0;
    }

    public void AgregarLlamada(Llamada llamada)
    {
        llamadas.Add(llamada);
        acum ++;
    }

    public double CalcularTotalLocal()
    {
        double total = 0;

        foreach (var llamada in llamadas)
        {

            if (llamada is LlamadaLocal)
            {
                total += llamada.CalcularPrecio();
            }

        }

        return total;
    }

    public double CalcularTotalProvincial()
    {
        double total = 0;

        foreach (var llamada in llamadas)
        {

            if (llamada is LlamadaProvincial)
            {
                total += llamada.CalcularPrecio();
            }

        }

        return total;
    }
}