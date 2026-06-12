using PlanificacionDeProcesos;
class Program
{
    static void Main(string[] args)
    {
        List<PCB> procesos =
        [
            new PCB(6, 1),   // P1, PID=1
            new PCB(3, 2),   // P2, PID=2
            new PCB(2, 3)    // P3, PID=3
        ];

        Console.WriteLine("=== SIMULADOR DE PLANIFICACIÓN ===\n");

        PlanificadorFCFS.Ejecutar(procesos);
        PlanificadorSJF.Ejecutar(procesos);
    }
}