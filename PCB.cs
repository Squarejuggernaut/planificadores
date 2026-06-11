namespace PlanificacionDeProcesos
{
    public class PCB
    {
        private static int _ultimoPID = 0;

        public int PID { get; private set; }
        public string Nombre { get; private set; }
        public int RáfagaCPU { get; set; }
        public int TiempoLlegada { get; private set; }
        public EstadoProceso Estado { get; set; }

        public PCB(int ráfagaCPU, int tiempoLlegada)
        {
            _ultimoPID++;
            PID = _ultimoPID;
            Nombre = $"P{PID}";
            RáfagaCPU = ráfagaCPU;
            TiempoLlegada = tiempoLlegada;
            Estado = EstadoProceso.NEW;
        }
    }
}