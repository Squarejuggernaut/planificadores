namespace PlanificacionDeProcesos
{
    public static class PlanificadorSJF
    {
        public static void Ejecutar(List<PCB> procesos)
        {
            var ordenados = procesos.OrderBy(p => p.RafagaCPU).ToList();
            Console.WriteLine("\nOrden de ejecución (SJF):");
            foreach (var p in ordenados)
            {
                Console.WriteLine($"  {p.Nombre} - Estado: {p.Estado} (PID: {p.PID}, ráfaga: {p.RafagaCPU}, llegada: {p.TiempoLlegada})");
            }
        }
    }
}