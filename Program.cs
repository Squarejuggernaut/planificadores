using PlanificacionDeProcesos;

class Program
{
    static void Main(string[] args)
    {
        var fcfs = new PlanificadorFCFS();
        var sjf = new PlanificadorSJF();

        // // CASO 1: FCFS con llegadas escalonadas
        // List<PCB> procesos =
        // [
        //     new PCB(6, 1),   // P1 llega 1, ráfaga 6
        //     new PCB(3, 2),   // P2 llega 2, ráfaga 3
        //     new PCB(2, 3)    // P3 llega 3, ráfaga 2
        // ];
        // Console.WriteLine("=== DEMO FCFS (llegadas escalonadas) ===");
        // fcfs.Ejecutar(procesos);

        // // CASO 2: SJF con llegadas escalonadas
        // List<PCB> procesosSJF =
        // [
        //     new PCB(6, 1),   // P1 llega 1, ráfaga 6
        //     new PCB(3, 2),   // P2 llega 2, ráfaga 3
        //     new PCB(2, 3)    // P3 llega 3, ráfaga 2
        // ];
        // Console.WriteLine("=== DEMO SJF (llegadas escalonadas) ===");
        // sjf.Ejecutar(procesosSJF);

        // // CASO 3: SJF con llegadas simultáneas
        // List<PCB> procesosSimultaneos =
        // [
        //     new PCB(6, 0),   // P1 llega 0, ráfaga 6
        //     new PCB(3, 0),   // P2 llega 0, ráfaga 3
        //     new PCB(2, 0)    // P3 llega 0, ráfaga 2
        // ];
        // Console.WriteLine("=== DEMO SJF (llegadas simultáneas) ===");
        // sjf.Ejecutar(procesosSimultaneos);

        // // CASO 4: FCFS con llegadas simultáneas
        // List<PCB> procesosSimultaneos =
        // [
        //     new PCB(6, 0),   // P1 llega 0, ráfaga 6
        //     new PCB(3, 0),   // P2 llega 0, ráfaga 3
        //     new PCB(2, 0)    // P3 llega 0, ráfaga 2
        // ];
        // Console.WriteLine("=== DEMO FCFS (llegadas simultáneas) ===");
        // fcfs.Ejecutar(procesosSimultaneos);

        // // CASO 5: Comparativa FCFS vs SJF (mismos datos)
        // List<PCB> procesosComp =
        // [
        //     new PCB(6, 1),
        //     new PCB(3, 2),
        //     new PCB(2, 3)
        // ];
        // Console.WriteLine("=== COMPARATIVA: FCFS vs SJF ===");
        // Console.WriteLine("\n--- FCFS ---");
        // fcfs.Ejecutar(procesosComp);
        // Console.WriteLine("\n--- SJF ---");
        // sjf.Ejecutar(procesosComp);

        // // CASO 6: Proceso largo vs varios cortos (SJF)
        // List<PCB> procesosLargo =
        // [
        //     new PCB(10, 0),   // P1 llega 0, ráfaga 10 (largo)
        //     new PCB(2, 1),    // P2 llega 1, ráfaga 2
        //     new PCB(3, 2),    // P3 llega 2, ráfaga 3
        //     new PCB(1, 3)     // P4 llega 3, ráfaga 1
        // ];
        // Console.WriteLine("=== DEMO SJF (proceso largo vs varios cortos) ===");
        // sjf.Ejecutar(procesosLargo);

        // // CASO 7: Límite de procesos (10) - SJF
        // List<PCB> procesosLimites =
        // [
        //     new PCB(10, 0),   // P1 llega 0, ráfaga 10 (largo)
        //     new PCB(2, 1),    // P2 llega 1, ráfaga 2
        //     new PCB(3, 2),    // P3 llega 2, ráfaga 3
        //     new PCB(1, 3),    // P4 llega 3, ráfaga 1
        //     new PCB(10, 0),   // P1 llega 0, ráfaga 10 (largo)
        //     new PCB(2, 1),    // P2 llega 1, ráfaga 2
        //     new PCB(3, 2),    // P3 llega 2, ráfaga 3
        //     new PCB(1, 3),     // P4 llega 3, ráfaga 1
        //     new PCB(10, 0),   // P1 llega 0, ráfaga 10 (largo)
        //     new PCB(2, 1),    // P2 llega 1, ráfaga 2
        // ];
        // Console.WriteLine("=== DEMO SJF (comportamiento con límite de procesos) ===");
        // sjf.Ejecutar(procesosLimites);

        // // CASO 8: Error fuera de los límites - SJF
        // List<PCB> procesosSuperaLimites =
        // [
        //     new PCB(10, 0),   // P1 llega 0, ráfaga 10 (largo)
        //     new PCB(2, 1),    // P2 llega 1, ráfaga 2
        //     new PCB(3, 2),    // P3 llega 2, ráfaga 3
        //     new PCB(1, 3),    // P4 llega 3, ráfaga 1
        //     new PCB(10, 0),   // P1 llega 0, ráfaga 10 (largo)
        //     new PCB(2, 1),    // P2 llega 1, ráfaga 2
        //     new PCB(3, 2),    // P3 llega 2, ráfaga 3
        //     new PCB(1, 3),     // P4 llega 3, ráfaga 1
        //     new PCB(10, 0),   // P1 llega 0, ráfaga 10 (largo)
        //     new PCB(2, 1),    // P2 llega 1, ráfaga 2
        //     new PCB(2, 1),    // P2 llega 1, ráfaga 2
        // ];
        // Console.WriteLine("=== DEMO SJF (comportamiento cuando supera límite de procesos) ===");
        // sjf.Ejecutar(procesosSuperaLimites);
    }
}