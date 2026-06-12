using Spectre.Console;

namespace PlanificacionDeProcesos
{
    public static class PlanificadorFCFS
    {
        private static int _tiempoActual;
        private static Queue<PCB> _colaReady;
        private static PCB _procesoActual;
        private static List<PCB> _procesos;
        private static List<string> _ordenFinalizacion;
        private static int _procesosTerminados;

        public static void Ejecutar(List<PCB> procesosOriginales)
        {
            Console.WriteLine("=== SIMULADOR DE PLANIFICACIÓN FCFS ===\n");
            if (!ValidarProcesos(procesosOriginales)) return;

            Inicializar(procesosOriginales);
            LayoutTabla.InicializarTablaSimulacion();
            MostrarTablaProcesosIniciales();

            while (_procesosTerminados < _procesos.Count)
            {
                AgregarProcesosQueLlegan();
                AsignarCPU();
                LayoutTabla.AgregarFilaFCFS(_tiempoActual, _colaReady, _procesoActual);
                EjecutarUnidadDeTiempo();
                _tiempoActual++;
            }

            LayoutTabla.MostrarResultados(_tiempoActual, _ordenFinalizacion);
        }

        private static bool ValidarProcesos(List<PCB> procesos)
        {
            if (procesos.Count > 10)
            {
                AnsiConsole.MarkupLine("[red]Error: Máximo 10 procesos permitidos.[/]");
                return false;
            }
            return true;
        }

        private static void Inicializar(List<PCB> procesosOriginales)
        {
            _tiempoActual = 0;
            _colaReady = new Queue<PCB>();
            _procesoActual = null;
            _procesos = [.. procesosOriginales.OrderBy(p => p.TiempoLlegada)];
            _ordenFinalizacion = [];
            _procesosTerminados = 0;
        }

        private static void MostrarTablaProcesosIniciales()
        {
            AnsiConsole.Write(new Rule("📋 Procesos creados (estado NEW)").RuleStyle("yellow"));
            var tablaProcesos = LayoutTabla.CrearTablaProcesosIniciales(_procesos);
            AnsiConsole.Write(tablaProcesos);
            AnsiConsole.WriteLine();
        }

        private static void AgregarProcesosQueLlegan()
        {
            foreach (var p in _procesos)
            {
                if (p.EsNuevo && p.TiempoLlegada == _tiempoActual)
                {
                    if (_procesoActual == null)
                    {
                        p.Estado = EstadoProceso.RUNNING;
                        _procesoActual = p;
                    }
                    else
                    {
                        p.Estado = EstadoProceso.READY;
                        _colaReady.Enqueue(p);
                    }
                }
            }
        }

        private static void AsignarCPU()
        {
            if (_procesoActual == null && _colaReady.Count > 0)
            {
                _procesoActual = _colaReady.Dequeue();
                _procesoActual.Estado = EstadoProceso.RUNNING;
            }
        }

        private static void EjecutarUnidadDeTiempo()
        {
            if (_procesoActual != null)
            {
                _procesoActual.RafagaRestante--;

                if (_procesoActual.RafagaRestante == 0)
                {
                    _procesoActual.Estado = EstadoProceso.TERMINATED;
                    _ordenFinalizacion.Add(LayoutTabla.ObtenerNombreConColor(_procesoActual));
                    _procesoActual = null;
                    _procesosTerminados++;
                }
            }
        }
    }
}