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
        private static Table _tablaSimulacion;

        private static readonly Color[] _coloresProcesos = new Color[]
        {
            Color.Blue,
            Color.Green,
            Color.Yellow,
            Color.Magenta,
            Color.Cyan,
            Color.Orange1,
            Color.Pink1,
            Color.Aqua,
            Color.Lime,
            Color.Purple
        };

        public static void Ejecutar(List<PCB> procesosOriginales)
        {
            if (procesosOriginales.Count > 10)
            {
                AnsiConsole.MarkupLine("[red]Error: Máximo 10 procesos permitidos.[/]");
                return;
            }

            Inicializar(procesosOriginales);
            MostrarTablaProcesosIniciales();
            ConfigurarTablaSimulacion();

            while (_procesosTerminados < _procesos.Count)
            {
                AgregarProcesosQueLlegan();
                AsignarCPU();
                AgregarFilaATablaSimulacion();
                EjecutarUnidadDeTiempo();
                _tiempoActual++;
            }

            MostrarResultadosFinales();
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

        private static string ObtenerNombreConColor(PCB proceso)
        {
            int indice = (proceso.PID - 1) % _coloresProcesos.Length;
            Color color = _coloresProcesos[indice];
            return $"[{color.ToString().ToLower()}]{proceso.Nombre}[/]";
        }

        private static void MostrarTablaProcesosIniciales()
        {
            AnsiConsole.Write(new Rule("📋 Procesos creados (estado NEW)").RuleStyle("yellow"));

            var tabla = new Table()
                .Border(TableBorder.Rounded)
                .AddColumn("PID")
                .AddColumn("Nombre")
                .AddColumn("Rafaga CPU")
                .AddColumn("Tiempo llegada");

            foreach (var p in _procesos)
            {
                tabla.AddRow(p.PID.ToString(), ObtenerNombreConColor(p), p.RafagaCPU.ToString(), p.TiempoLlegada.ToString());
            }

            AnsiConsole.Write(tabla);
            AnsiConsole.WriteLine();
        }

        private static void ConfigurarTablaSimulacion()
        {
            _tablaSimulacion = new Table()
                .Border(TableBorder.Rounded)
                .AddColumn(new TableColumn("Tiempo").Centered())
                .AddColumn(new TableColumn("Cola de READY").LeftAligned())
                .AddColumn(new TableColumn("CPU").Centered())
                .AddColumn(new TableColumn("Rafaga restante").Centered());

            _tablaSimulacion.BorderColor(Color.Grey);
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

        private static void AgregarFilaATablaSimulacion()
        {
            string colaStr = _colaReady.Count > 0 
                ? string.Join(", ", _colaReady.Select(p => ObtenerNombreConColor(p))) 
                : "vacía";
            
            string cpuStr = _procesoActual != null 
                ? ObtenerNombreConColor(_procesoActual) 
                : "---";
            
            string rafagaStr = _procesoActual != null
                ? (_procesoActual.RafagaRestante == 1 
                    ? "[red]1 (fin)[/]" 
                    : _procesoActual.RafagaRestante.ToString())
                : "--";

            _tablaSimulacion.AddRow(_tiempoActual.ToString(), colaStr, cpuStr, rafagaStr);
        }

        private static void EjecutarUnidadDeTiempo()
        {
            if (_procesoActual != null)
            {
                _procesoActual.RafagaRestante--;

                if (_procesoActual.RafagaRestante == 0)
                {
                    _procesoActual.Estado = EstadoProceso.TERMINATED;
                    _ordenFinalizacion.Add(ObtenerNombreConColor(_procesoActual));
                    _procesoActual = null;
                    _procesosTerminados++;
                }
            }
        }

        private static void MostrarResultadosFinales()
        {
            AnsiConsole.Write(new Rule("🏁 Resultados finales").RuleStyle("green"));
            AnsiConsole.Write(_tablaSimulacion);

            AnsiConsole.MarkupLine($"\n[green]✅ Tiempo total de simulación: {_tiempoActual}[/]");
            AnsiConsole.MarkupLine($"[blue]📋 Procesos terminados (en orden): {string.Join(", ", _ordenFinalizacion)}[/]");
        }
    }
}