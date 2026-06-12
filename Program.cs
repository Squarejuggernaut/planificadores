using PlanificacionDeProcesos;
class Program
{
    static void Main(string[] args)
    {
        List<PCB> procesos =
[
    new PCB(5, 0),
    new PCB(3, 0),
    new PCB(7, 0)
];

        Console.WriteLine("=== SIMULADOR DE PLANIFICACIÓN ===\n");

        PlanificadorFCFS.Ejecutar(procesos);
        PlanificadorSJF.Ejecutar(procesos);
    }
}