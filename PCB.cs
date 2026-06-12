namespace PlanificacionDeProcesos
{
    public class PCB
    {
        private static int _ultimoPID = 0;

        public int PID { get; private set; }
        public string Nombre { get; private set; }
        public int RafagaCPU { get; set; }
        public int RafagaRestante { get; set; }
        public int TiempoLlegada { get; private set; }
        public EstadoProceso Estado { get; set; }

        public bool EsNuevo => Estado == EstadoProceso.NEW;
        public bool EstaReady => Estado == EstadoProceso.READY;
        public bool EstaRunning => Estado == EstadoProceso.RUNNING;
        public bool EstaTerminado => Estado == EstadoProceso.TERMINATED;

        public PCB(int rafagaCPU, int tiempoLlegada)
        {
            _ultimoPID++;
            PID = _ultimoPID;
            Nombre = $"P{PID}";
            RafagaCPU = rafagaCPU;
            RafagaRestante = rafagaCPU;
            TiempoLlegada = tiempoLlegada;
            Estado = EstadoProceso.NEW;
        }
    }
}