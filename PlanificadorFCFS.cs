namespace PlanificacionDeProcesos
{
    public static class PlanificadorFCFS
    {
        public static void Ejecutar(List<PCB> procesos)
        {
            var ordenados = procesos.OrderBy(p => p.TiempoLlegada).ToList();
            Console.WriteLine("Orden de ejecución (FCFS):");
            foreach (var p in ordenados)
            {
                Console.WriteLine($"  {p.Nombre} - Estado: {p.Estado} (PID: {p.PID}, ráfaga: {p.RáfagaCPU}, llegada: {p.TiempoLlegada})");
            }
        }
    }
}