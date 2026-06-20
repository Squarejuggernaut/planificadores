namespace PlanificacionDeProcesos
{
    /// <summary>
    /// Implementación del algoritmo de planificación SJF (Shortest Job First).
    /// Hereda de PlanificadorBase y usa una cola de prioridad (PriorityQueue).
    /// 
    /// Características:
    /// - No apropiativo: un proceso no se interrumpe hasta que termina.
    /// - Prioriza los procesos con ráfaga de CPU más corta.
    /// - En caso de empate, se respeta el orden de llegada (por PID).
    /// - La cola de READY es una PriorityQueue con prioridad (ráfaga, PID).
    /// </summary>
    public class PlanificadorSJF : PlanificadorBase
    {
        /// <summary> Cola de procesos listos para ejecutar (ordenada por prioridad). </summary>
        private PriorityQueue<PCB, (int rafaga, int pid)> _colaReady;

        /// <summary> Nombre del planificador (se muestra en la UI). </summary>
        protected override string NombrePlanificador => "SJF";

        /// <summary> Inicializa la cola de READY como una PriorityQueue vacía. </summary>
        protected override void InicializarCola()
        {
            _colaReady = new PriorityQueue<PCB, (int rafaga, int pid)>();
        }

        /// <summary>
        /// Agrega un proceso a la cola con prioridad (ráfaga, PID).
        /// La cola ordena automáticamente: primero por ráfaga (menor) y luego por PID (en caso de empate).
        /// </summary>
        protected override void AgregarProcesoALaColaDeReady(PCB proceso)
        {
            _colaReady.Enqueue(proceso, (proceso.RafagaCPU, proceso.PID));
        }

        /// <summary>
        /// Asigna la CPU al proceso de mayor prioridad (menor ráfaga).
        /// Solo se ejecuta si la CPU está libre y hay procesos en READY.
        /// </summary>
        protected override void AsignarCPU()
        {
            if (_procesoEnCPU == null && _colaReady.Count > 0)
            {
                _procesoEnCPU = _colaReady.Dequeue();
                _procesoEnCPU.Estado = EstadoProceso.RUNNING;
            }
        }

        /// <summary>
        /// Agrega una fila a la tabla de simulación usando el formateo específico de SJF.
        /// </summary>
        protected override void AgregarFilaATabla()
        {
            LayoutTabla.AgregarFilaSJF(_tiempoActual, _colaReady, _procesoEnCPU);
        }
    }
}