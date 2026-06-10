using System;
using System.Collections.Generic;
using System.Linq;

enum EstadoProceso
{
    READY,
    RUNNING,
    TERMINATED
}
class PCB
{
    public int PID { get; set; }
    public required string Nombre { get; set; }
    public int RáfagaCPU { get; set; }
    public int TiempoLlegada { get; set; }
    public EstadoProceso Estado { get; set; }  // READY, RUNNING, TERMINATED
}

class Program
{
    static void Main(string[] args)
    {
        // Datos hardcodeados
        List<PCB> procesos =
            [
                new PCB { PID = 1, Nombre = "P1", RáfagaCPU = 6, TiempoLlegada = 1, Estado = EstadoProceso.READY },
                new PCB { PID = 2, Nombre = "P2", RáfagaCPU = 3, TiempoLlegada = 2, Estado = EstadoProceso.READY },
                new PCB { PID = 3, Nombre = "P3", RáfagaCPU = 2, TiempoLlegada = 3, Estado = EstadoProceso.READY }
            ];

        Console.WriteLine("=== SIMULADOR DE PLANIFICACIÓN ===\n");

        // FCFS
        Console.WriteLine("--- FCFS (First Come, First Served) ---");
        FCFS(procesos);

        // SJF
        Console.WriteLine("\n--- SJF (Shortest Job First) ---");
        SJF(procesos);
    }

    static void FCFS(List<PCB> procesos)
    {
        // Ordenar por tiempo de llegada
        var ordenados = procesos.OrderBy(p => p.TiempoLlegada).ToList();
        Console.Write("Orden de ejecución: ");
        foreach (var p in ordenados)
        {
            Console.Write(p.Nombre + " ");
        }
        Console.WriteLine();
    }

    static void SJF(List<PCB> procesos)
    {
        // Ordenar por ráfaga de CPU (menor primero)
        var ordenados = procesos.OrderBy(p => p.RáfagaCPU).ToList();
        Console.Write("Orden de ejecución: ");
        foreach (var p in ordenados)
        {
            Console.Write(p.Nombre + " ");
        }
        Console.WriteLine();
    }
}