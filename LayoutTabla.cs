using Spectre.Console;

namespace PlanificacionDeProcesos
{
    /// <summary>
    /// Clase auxiliar para toda la interfaz de usuario (UI).
    /// Centraliza la creación de tablas, colores por proceso, formato de salida
    /// y mensajes de la interfaz.
    /// </summary>
    public static class LayoutTabla
    {
        private static readonly Color[] _coloresProcesos =
        [
            Color.Blue, Color.Green, Color.Yellow, Color.Magenta, Color.Cyan,
            Color.Orange1, Color.Pink1, Color.Aqua, Color.Lime, Color.Purple
        ];

        private static Table _tablaSimulacion;

        /// <summary>
        /// Devuelve el nombre del proceso con el color asignado según su PID.
        /// El color se asigna de forma cíclica (PID 1 → Blue, PID 2 → Green, etc.).
        /// </summary>
        public static string ObtenerNombreConColor(PCB proceso)
        {
            int indice = (proceso.PID - 1) % _coloresProcesos.Length;
            Color color = _coloresProcesos[indice];
            return $"[{color.ToString().ToLower()}]{proceso.Nombre}[/]";
        }

        /// <summary>
        /// Muestra la tabla de procesos en estado NEW (recién creados).
        /// </summary>
        public static void MostrarTablaProcesosIniciales(List<PCB> procesos)
        {
            AnsiConsole.Write(new Rule(" Procesos creados (estado NEW)").RuleStyle("yellow"));
            var tabla = CrearTablaProcesosIniciales(procesos);
            AnsiConsole.Write(tabla);
            AnsiConsole.WriteLine();
        }

        /// <summary>
        /// Crea la tabla de procesos en estado NEW.
        /// Muestra PID, Nombre, Ráfaga CPU y Tiempo de llegada.
        /// </summary>
        private static Table CrearTablaProcesosIniciales(List<PCB> procesos)
        {
            var tabla = new Table().Border(TableBorder.Rounded)
                .AddColumn("PID")
                .AddColumn("Nombre")
                .AddColumn("Rafaga CPU")
                .AddColumn("Tiempo llegada");

            foreach (var proceso in procesos)
                tabla.AddRow(
                    proceso.PID.ToString(),
                    ObtenerNombreConColor(proceso),
                    proceso.RafagaCPU.ToString(),
                    proceso.TiempoLlegada.ToString()
                );

            return tabla;
        }

        /// <summary>
        /// Prepara la tabla de simulación (tiempo, cola READY, CPU, ráfaga restante).
        /// Debe llamarse antes de comenzar a agregar filas.
        /// </summary>
        public static void PrepararTablaSimulacion()
        {
            _tablaSimulacion = new Table().Border(TableBorder.Rounded)
                .AddColumn(new TableColumn("Tiempo").Centered())
                .AddColumn(new TableColumn("Cola de READY").LeftAligned())
                .AddColumn(new TableColumn("CPU").Centered())
                .AddColumn(new TableColumn("Rafaga restante").Centered());
            _tablaSimulacion.BorderColor(Color.Grey);
        }

        /// <summary>
        /// Obtiene la tabla de simulación actual (la crea si no existe).
        /// </summary>
        private static Table ObtenerTablaSimulacion()
        {
            if (_tablaSimulacion == null)
                PrepararTablaSimulacion();
            return _tablaSimulacion;
        }

        /// <summary>
        /// Agrega una fila a la tabla de simulación para FCFS.
        /// </summary>
        public static void AgregarFilaFCFS(int tiempoActual, Queue<PCB> colaReady, PCB procesoEnCPU)
        {
            AgregarFila(tiempoActual, FormatearColaFCFS(colaReady), procesoEnCPU);
        }

        /// <summary>
        /// Agrega una fila a la tabla de simulación para SJF.
        /// </summary>
        public static void AgregarFilaSJF(int tiempoActual, PriorityQueue<PCB, (int rafaga, int pid)> colaReady, PCB procesoEnCPU)
        {
            AgregarFila(tiempoActual, FormatearColaSJF(colaReady), procesoEnCPU);
        }

        /// <summary>
        /// Agrega una fila a la tabla de simulación (lógica común).
        /// </summary>
        private static void AgregarFila(int tiempoActual, string colaFormateada, PCB procesoEnCPU)
        {
            ObtenerTablaSimulacion().AddRow(
                tiempoActual.ToString(),
                colaFormateada,
                FormatearCPU(procesoEnCPU),
                FormatearRafaga(procesoEnCPU)
            );
        }

        /// <summary>
        /// Formatea la cola de READY para FCFS (Queue).
        /// </summary>
        private static string FormatearColaFCFS(Queue<PCB> colaReady)
        {
            if (colaReady.Count == 0)
                return "vacía";

            return string.Join(", ", colaReady.Select(ObtenerNombreConColor));
        }

        /// <summary>
        /// Formatea la cola de READY para SJF (PriorityQueue).
        /// </summary>
        private static string FormatearColaSJF(PriorityQueue<PCB, (int rafaga, int pid)> colaReady)
        {
            if (colaReady == null || colaReady.Count == 0)
                return "vacía";

            // Extrae los elementos de la PriorityQueue (sin órden garantizado) y los guarda en una lista con su prioridad (desordenadamente).
            var items = new List<(PCB proceso, (int rafaga, int pid) prioridad)>();
            foreach (var (Element, Priority) in colaReady.UnorderedItems)
                items.Add((Element, Priority));

            // ecorro la lista y ordeno manualmente los procesos por prioridad primero el que menor rafaga y luego por PID (en caso de empate de ráfaga).
            items = [.. items.OrderBy(i => i.prioridad.rafaga).ThenBy(i => i.prioridad.pid)];

            return string.Join(", ", items.Select(item => ObtenerNombreConColor(item.proceso)));
        }

        /// <summary>
        /// Formatea el nombre del proceso en CPU (o "---" si no hay).
        /// </summary>
        private static string FormatearCPU(PCB procesoEnCPU)
        {
            return procesoEnCPU != null ? ObtenerNombreConColor(procesoEnCPU) : "---";
        }

        /// <summary>
        /// Formatea la ráfaga restante del proceso en CPU.
        /// </summary>
        private static string FormatearRafaga(PCB procesoEnCPU)
        {
            if (procesoEnCPU == null)
                return "--";

            return procesoEnCPU.RafagaRestante == 1
                ? "[red]1 (fin)[/]"
                : procesoEnCPU.RafagaRestante.ToString();
        }

        /// <summary>
        /// Muestra el título según el planificador utilizado en la consola.
        /// </summary>
        public static void MostrarTituloPlanificador(string nombre)
        {
            Console.WriteLine($"=== SIMULADOR DE PLANIFICACIÓN {nombre} ===\n");
        }

        /// <summary>
        /// Muestra un mensaje de error cuando se supera el límite de 10 procesos.
        /// </summary>
        public static void MostrarErrorLimiteProcesos()
        {
            AnsiConsole.MarkupLine("[red]Error: Máximo 10 procesos permitidos.[/]");
        }

        /// <summary>
        /// Muestra el resumen final de la simulación:
        /// - Tabla completa de simulación
        /// - Tiempo total de simulación
        /// - Orden de finalización de los procesos
        /// </summary>
        public static void MostrarResultados(int tiempoActual, List<string> ordenFinalizacion)
        {
            AnsiConsole.Write(new Rule(" Resultados finales").RuleStyle("green"));
            AnsiConsole.Write(ObtenerTablaSimulacion());
            AnsiConsole.MarkupLine($"\n[green] Tiempo total de simulación: {tiempoActual}[/]");
            AnsiConsole.MarkupLine($"[blue] Procesos terminados (en orden): {string.Join(", ", ordenFinalizacion)}[/]");
        }
    }
}