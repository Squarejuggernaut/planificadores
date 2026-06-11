using System;
using System.Collections.Generic;
using System.Linq;
using PlanificacionDeProcesos;

class PCB
{
    public int PID { get; set; }
    public required string Nombre { get; set; }
    public int RáfagaCPU { get; set; }
    public int TiempoLlegada { get; set; }
    public EstadoProceso Estado { get; set; }
}

class Program
{
    static void Main(string[] args)
    {
        List<PCB> procesos =
            [
                new PCB { PID = 1, Nombre = "P1", RáfagaCPU = 6, TiempoLlegada = 1, Estado = EstadoProceso.NEW },
                new PCB { PID = 2, Nombre = "P2", RáfagaCPU = 3, TiempoLlegada = 2, Estado = EstadoProceso.NEW },
                new PCB { PID = 3, Nombre = "P3", RáfagaCPU = 2, TiempoLlegada = 3, Estado = EstadoProceso.NEW }
            ];

        Console.WriteLine("=== SIMULADOR DE PLANIFICACIÓN ===\n");

        Console.WriteLine("--- FCFS (First Come, First Served) ---");
        FCFS(procesos);

        Console.WriteLine("\n--- SJF (Shortest Job First) ---");
        SJF(procesos);
    }

    static void FCFS(List<PCB> procesos)
    {
        var ordenados = procesos.OrderBy(p => p.TiempoLlegada).ToList();
        Console.WriteLine("Orden de ejecución (FCFS):");
        foreach (var p in ordenados)
        {
            Console.WriteLine($"  {p.Nombre} - Estado: {p.Estado} (ráfaga: {p.RáfagaCPU}, llegada: {p.TiempoLlegada})");
        }
    }

    static void SJF(List<PCB> procesos)
    {
        var ordenados = procesos.OrderBy(p => p.RáfagaCPU).ToList();
        Console.WriteLine("\nOrden de ejecución (SJF):");
        foreach (var p in ordenados)
        {
            Console.WriteLine($"  {p.Nombre} - Estado: {p.Estado} (ráfaga: {p.RáfagaCPU}, llegada: {p.TiempoLlegada})");
        }
    }
}