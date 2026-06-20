namespace PlanificacionDeProcesos
{
    /// <summary>
    /// Clase base abstracta para todos los planificadores.
    /// Contiene la lógica común a FCFS y SJF:
    /// - Avance del tiempo (unidad por unidad)
    /// - Llegada de procesos (NEW → READY)
    /// - Ejecución de unidad de tiempo (decremento de ráfaga)
    /// - Finalización de procesos (READY → TERMINATED)
    /// 
    /// Cada planificador concreto debe implementar:
    /// - La cola de READY (Queue, PriorityQueue, etc.)
    /// - La asignación de CPU (FIFO, prioridad, etc.)
    /// - La forma de agregar una fila a la tabla de simulación
    /// </summary>
    public abstract class PlanificadorBase
    {
        protected int _tiempoActual;
        protected PCB _procesoEnCPU;
        protected List<PCB> _procesosActivos;
        protected List<string> _ordenFinalizacion;

        /// <summary> Nombre del planificador (ej. "FCFS", "SJF"). </summary>
        protected abstract string NombrePlanificador { get; }

        /// <summary> Inicializa la estructura de datos de la cola de READY. </summary>
        protected abstract void InicializarCola();

        /// <summary> Agrega un proceso a la cola de READY según el criterio del planificador. </summary>
        protected abstract void AgregarProcesoALaColaDeReady(PCB proceso);

        /// <summary> Asigna el próximo proceso de la cola a la CPU según el criterio del planificador. </summary>
        protected abstract void AsignarCPU();

        /// <summary> Agrega una fila a la tabla de simulación con el estado actual. </summary>
        protected abstract void AgregarFilaATabla();

        /// <summary>
        /// Punto de entrada del planificador. Orquesta toda la simulación.
        /// </summary>
        /// <param name="procesosOriginales">Lista de procesos a simular.</param>
        public void Ejecutar(List<PCB> procesosOriginales)
        {
            LayoutTabla.MostrarTituloPlanificador(NombrePlanificador);

            if (!ValidarCantidadProcesos(procesosOriginales)) return;

            Inicializar(procesosOriginales);
            InicializarCola();
            LayoutTabla.PrepararTablaSimulacion();
            LayoutTabla.MostrarTablaProcesosIniciales(_procesosActivos);

            while (HayProcesosActivos())
            {
                AgregarProcesosQueLlegan();
                AsignarCPU();
                AgregarFilaATabla();
                EjecutarUnidadDeTiempo();
                _tiempoActual++;
            }

            LayoutTabla.MostrarResultados(_tiempoActual, _ordenFinalizacion);
        }

        /// <summary> Valida que la cantidad de procesos no supere el límite de 10. </summary>
        private static bool ValidarCantidadProcesos(List<PCB> procesos)
        {
            if (procesos.Count > 10)
            {
                LayoutTabla.MostrarErrorLimiteProcesos();
                return false;
            }
            return true;
        }

        /// <summary> Inicializa las variables de estado y ordena los procesos por tiempo de llegada. </summary>
        private void Inicializar(List<PCB> procesosOriginales)
        {
            _tiempoActual = 0;
            _procesoEnCPU = null;
            _procesosActivos = [.. procesosOriginales.OrderBy(p => p.TiempoLlegada)];
            _ordenFinalizacion = [];
        }

        /// <summary> Indica si aún hay procesos sin terminar (activos). </summary>
        private bool HayProcesosActivos()
        {
            return _procesosActivos.Any(proceso => !proceso.EstaTerminado);
        }

        /// <summary> Agrega a la cola de READY los procesos cuyo tiempo de llegada coincide con el tiempo actual. </summary>
        private void AgregarProcesosQueLlegan()
        {
            foreach (var proceso in _procesosActivos)
            {
                if (ProcesoListoParaLlegar(proceso))
                {
                    proceso.Estado = EstadoProceso.READY;
                    AgregarProcesoALaColaDeReady(proceso);
                }
            }
        }

        /// <summary> Indica si un proceso está listo para ser encolado (está en NEW y su tiempo de llegada es el actual). </summary>
        private bool ProcesoListoParaLlegar(PCB proceso)
        {
            return proceso.EsNuevo && proceso.TiempoLlegada == _tiempoActual;
        }

        /// <summary> Simula una unidad de tiempo de ejecución del proceso en CPU. </summary>
        private void EjecutarUnidadDeTiempo()
        {
            if (_procesoEnCPU == null) return;

            _procesoEnCPU.RafagaRestante--;

            if (_procesoEnCPU.RafagaRestante == 0)
            {
                FinalizarProcesoActual();
            }
        }

        /// <summary> Finaliza el proceso actual: cambia estado a TERMINATED, guarda en orden y libera CPU. </summary>
        private void FinalizarProcesoActual()
        {
            _procesoEnCPU.Estado = EstadoProceso.TERMINATED;
            _ordenFinalizacion.Add(LayoutTabla.ObtenerNombreConColor(_procesoEnCPU));
            _procesoEnCPU = null;
        }
    }
}