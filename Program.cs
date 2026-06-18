using PlanificacionDeProcesos;
class Program
{
    static void Main(string[] args)
    {

        // // CASO 1: FCFS con llegadas escalonadas
        // List<PCB> procesos = new List<PCB>
        // {
        //     new PCB(6, 1),   // P1 llega 1, ráfaga 6
        //     new PCB(3, 2),   // P2 llega 2, ráfaga 3
        //     new PCB(2, 3)    // P3 llega 3, ráfaga 2
        // };
        // Console.WriteLine("=== DEMO FCFS (llegadas escalonadas) ===");
        // PlanificadorFCFS.Ejecutar(procesos);

        // // CASO 2: SJF con llegadas escalonadas
        // List<PCB> procesos = new List<PCB>
        // {
        //     new PCB(6, 1),   // P1 llega 1, ráfaga 6
        //     new PCB(3, 2),   // P2 llega 2, ráfaga 3
        //     new PCB(2, 3)    // P3 llega 3, ráfaga 2
        // };
        // Console.WriteLine("=== DEMO SJF (llegadas escalonadas) ===");
        // PlanificadorSJF.Ejecutar(procesos);

        // // CASO 3: SJF con llegadas simultáneas
        // List<PCB> procesos = new List<PCB>
        // {
        //     new PCB(6, 0),   // P1 llega 0, ráfaga 6
        //     new PCB(3, 0),   // P2 llega 0, ráfaga 3
        //     new PCB(2, 0)    // P3 llega 0, ráfaga 2
        // };
        // Console.WriteLine("=== DEMO SJF (llegadas simultáneas) ===");
        // PlanificadorSJF.Ejecutar(procesos);

        // // CASO 4: Comparativa FCFS vs SJF (mismos datos)
        // List<PCB> procesosFCFS = new List<PCB>
        // {
        //     new PCB(6, 1),
        //     new PCB(3, 2),
        //     new PCB(2, 3)
        // };
        // Console.WriteLine("=== COMPARATIVA: FCFS vs SJF ===");
        // Console.WriteLine("\n--- FCFS ---");
        // PlanificadorFCFS.Ejecutar(procesosFCFS);
        // Console.WriteLine("\n--- SJF ---");
        // PlanificadorSJF.Ejecutar(procesosFCFS);

        // // CASO 5: Proceso largo vs varios cortos (SJF)
        // List<PCB> procesos = new List<PCB>
        // {
        //     new PCB(10, 0),   // P1 llega 0, ráfaga 10 (largo)
        //     new PCB(2, 1),    // P2 llega 1, ráfaga 2
        //     new PCB(3, 2),    // P3 llega 2, ráfaga 3
        //     new PCB(1, 3)     // P4 llega 3, ráfaga 1
        // };
        // Console.WriteLine("=== DEMO SJF (proceso largo vs cortos) ===");
        // PlanificadorSJF.Ejecutar(procesos);
    }
}