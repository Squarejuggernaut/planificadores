namespace PlanificacionDeProcesos
{
    /// <summary>
    /// Implementación del algoritmo de planificación FCFS (First Come, First Served).
    /// Hereda de PlanificadorBase y usa una cola FIFO (Queue).
    /// 
    /// Características:
    /// - No apropiativo: un proceso no se interrumpe hasta que termina.
    /// - El orden de ejecución es el orden de llegada.
    /// - La cola de READY es una Queue (FIFO).
    /// </summary>
    public class PlanificadorFCFS : PlanificadorBase
    {
        /// <summary> Cola de procesos listos para ejecutar (FIFO). </summary>
        private Queue<PCB> _colaReady;

        /// <summary> Nombre del planificador (se muestra en la UI). </summary>
        protected override string NombrePlanificador => "FCFS";

        /// <summary> Inicializa la cola de READY como una Queue vacía. </summary>
        protected override void InicializarCola()
        {
            _colaReady = new Queue<PCB>();
        }

        /// <summary> Agrega un proceso al final de la cola (FIFO). </summary>
        protected override void AgregarProcesoALaColaDeReady(PCB proceso)
        {
            _colaReady.Enqueue(proceso);
        }

        /// <summary>
        /// Asigna la CPU al próximo proceso de la cola (el primero en llegar).
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
        /// Agrega una fila a la tabla de simulación usando el formateo específico de FCFS.
        /// </summary>
        protected override void AgregarFilaATabla()
        {
            LayoutTabla.AgregarFilaFCFS(_tiempoActual, _colaReady, _procesoEnCPU);
        }
    }
}