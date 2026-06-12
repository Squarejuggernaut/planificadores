using Spectre.Console;

namespace PlanificacionDeProcesos
{
    public static class LayoutTabla
    {
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

        public static string ObtenerNombreConColor(PCB proceso)
        {
            int indice = (proceso.PID - 1) % _coloresProcesos.Length;
            Color color = _coloresProcesos[indice];
            return $"[{color.ToString().ToLower()}]{proceso.Nombre}[/]";
        }

        public static Table CrearTablaProcesosIniciales(List<PCB> procesos)
        {
            var tabla = new Table()
                .Border(TableBorder.Rounded)
                .AddColumn("PID")
                .AddColumn("Nombre")
                .AddColumn("Rafaga CPU")
                .AddColumn("Tiempo llegada");

            foreach (var p in procesos)
            {
                tabla.AddRow(p.PID.ToString(), ObtenerNombreConColor(p), p.RafagaCPU.ToString(), p.TiempoLlegada.ToString());
            }

            return tabla;
        }

        public static Table CrearTablaSimulacion()
        {
            var tabla = new Table()
                .Border(TableBorder.Rounded)
                .AddColumn(new TableColumn("Tiempo").Centered())
                .AddColumn(new TableColumn("Cola de READY").LeftAligned())
                .AddColumn(new TableColumn("CPU").Centered())
                .AddColumn(new TableColumn("Rafaga restante").Centered());

            tabla.BorderColor(Color.Grey);
            return tabla;
        }

        public static string FormatearRafagaRestante(int rafagaRestante)
        {
            return rafagaRestante == 1 ? "[red]1 (fin)[/]" : rafagaRestante.ToString();
        }

        public static string FormatearColaReady(Queue<PCB> colaReady)
        {
            if (colaReady.Count == 0) return "vacía";
            return string.Join(", ", colaReady.Select(p => ObtenerNombreConColor(p)));
        }

        public static string FormatearCPU(PCB procesoActual)
        {
            return procesoActual != null ? ObtenerNombreConColor(procesoActual) : "---";
        }

        public static void MostrarResultados(Table tablaSimulacion, int tiempoActual, List<string> ordenFinalizacion)
        {
            AnsiConsole.Write(new Rule("🏁 Resultados finales").RuleStyle("green"));
            AnsiConsole.Write(tablaSimulacion);

            AnsiConsole.MarkupLine($"\n[green]✅ Tiempo total de simulación: {tiempoActual}[/]");
            AnsiConsole.MarkupLine($"[blue]📋 Procesos terminados (en orden): {string.Join(", ", ordenFinalizacion)}[/]");
        }
    }
}