using Spectre.Console;

namespace PlanificacionDeProcesos
{
    public static class LayoutTabla
    {
        private static readonly Color[] _coloresProcesos = new Color[]
        {
            Color.Blue, Color.Green, Color.Yellow, Color.Magenta, Color.Cyan,
            Color.Orange1, Color.Pink1, Color.Aqua, Color.Lime, Color.Purple
        };

        private static Table _tablaSimulacion;

        public static string ObtenerNombreConColor(PCB proceso)
        {
            int indice = (proceso.PID - 1) % _coloresProcesos.Length;
            Color color = _coloresProcesos[indice];
            return $"[{color.ToString().ToLower()}]{proceso.Nombre}[/]";
        }

        public static Table CrearTablaProcesosIniciales(List<PCB> procesos)
        {
            var tabla = new Table().Border(TableBorder.Rounded)
                .AddColumn("PID").AddColumn("Nombre").AddColumn("Rafaga CPU").AddColumn("Tiempo llegada");

            foreach (var p in procesos)
                tabla.AddRow(p.PID.ToString(), ObtenerNombreConColor(p), p.RafagaCPU.ToString(), p.TiempoLlegada.ToString());

            return tabla;
        }

        public static void InicializarTablaSimulacion()
        {
            _tablaSimulacion = new Table().Border(TableBorder.Rounded)
                .AddColumn(new TableColumn("Tiempo").Centered())
                .AddColumn(new TableColumn("Cola de READY").LeftAligned())
                .AddColumn(new TableColumn("CPU").Centered())
                .AddColumn(new TableColumn("Rafaga restante").Centered());
            _tablaSimulacion.BorderColor(Color.Grey);
        }

        public static Table ObtenerTablaSimulacion()
        {
            if (_tablaSimulacion == null)
                InicializarTablaSimulacion();
            return _tablaSimulacion;
        }

        // Para FCFS (Queue)
        public static void AgregarFilaFCFS(int tiempoActual, Queue<PCB> colaReady, PCB procesoActual)
        {
            string colaStr = colaReady.Count == 0 ? "vacía" : string.Join(", ", colaReady.Select(p => ObtenerNombreConColor(p)));
            string cpuStr = procesoActual != null ? ObtenerNombreConColor(procesoActual) : "---";
            string rafagaStr = procesoActual != null
                ? (procesoActual.RafagaRestante == 1 ? "[red]1 (fin)[/]" : procesoActual.RafagaRestante.ToString())
                : "--";

            ObtenerTablaSimulacion().AddRow(tiempoActual.ToString(), colaStr, cpuStr, rafagaStr);
        }

        // Para SJF (PriorityQueue con tupla)
        public static void AgregarFilaSJF(int tiempoActual, PriorityQueue<PCB, (int rafaga, int pid)> colaReady, PCB procesoActual)
        {
            string colaStr = "vacía";
            if (colaReady != null && colaReady.Count > 0)
            {
                var items = new List<(PCB proceso, (int rafaga, int pid) prioridad)>();
                foreach (var item in colaReady.UnorderedItems)
                {
                    items.Add((item.Element, item.Priority));
                }
                
                // Ordenar por ráfaga y luego por PID
                items = [.. items.OrderBy(i => i.prioridad.rafaga).ThenBy(i => i.prioridad.pid)];
                colaStr = string.Join(", ", items.Select(i => ObtenerNombreConColor(i.proceso)));
            }

            string cpuStr = procesoActual != null ? ObtenerNombreConColor(procesoActual) : "---";
            string rafagaStr = procesoActual != null
                ? (procesoActual.RafagaRestante == 1 ? "[red]1 (fin)[/]" : procesoActual.RafagaRestante.ToString())
                : "--";

            ObtenerTablaSimulacion().AddRow(tiempoActual.ToString(), colaStr, cpuStr, rafagaStr);
        }

        public static void MostrarResultados(int tiempoActual, List<string> ordenFinalizacion)
        {
            AnsiConsole.Write(new Rule("🏁 Resultados finales").RuleStyle("green"));
            AnsiConsole.Write(ObtenerTablaSimulacion());
            AnsiConsole.MarkupLine($"\n[green]✅ Tiempo total de simulación: {tiempoActual}[/]");
            AnsiConsole.MarkupLine($"[blue]📋 Procesos terminados (en orden): {string.Join(", ", ordenFinalizacion)}[/]");
        }
    }
}