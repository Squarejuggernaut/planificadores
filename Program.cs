using System;
using System.Collections.Generic;
using System.Linq;
using PlanificacionDeProcesos;

class Program
{
    static void Main(string[] args)
    {
        // Crear procesos usando el constructor (ya no hace falta asignar PID, Nombre, Estado)
        List<PCB> procesos =
        [
            new PCB(6, 1),   // P1, PID=1
            new PCB(3, 2),   // P2, PID=2
            new PCB(2, 3)    // P3, PID=3
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
            Console.WriteLine($"  {p.Nombre} - Estado: {p.Estado} (PID: {p.PID}, ráfaga: {p.RáfagaCPU}, llegada: {p.TiempoLlegada})");
        }
    }

    static void SJF(List<PCB> procesos)
    {
        var ordenados = procesos.OrderBy(p => p.RáfagaCPU).ToList();
        Console.WriteLine("\nOrden de ejecución (SJF):");
        foreach (var p in ordenados)
        {
            Console.WriteLine($"  {p.Nombre} - Estado: {p.Estado} (PID: {p.PID}, ráfaga: {p.RáfagaCPU}, llegada: {p.TiempoLlegada})");
        }
    }
}