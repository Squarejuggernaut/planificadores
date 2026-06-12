# Bitácora personal

## Me ayuda a ir documentando lo que voy haciendo en cada commit, el por qué y las posibles complicaciones que me puedan surgir.

### Versión inicial

- Creación de proyecto.
- Todo el código en Program.cs (class PCB y en el Main se crea lista de procesos hardcodeados, procedimiento FCFS y SJF muy básicos).
- Un primer acercamiento al comportamiento que deben tener FCFS y SJF.

### Problemas que detecto y que debo refactorizar

#### Organización del código

- ✅ **Resuelto en Refactor 1, 2 y 3:** Está todo en un solo archivo, no se aprovecha el paradigma de objetos.

#### Clase PCB

- ✅ **Resuelto en Refactor 1:** Los estados del proceso son pocos y siempre los mismos. Podría ponerlos en un `enum` para evitar errores (mayúsculas, "Ready", etc.).
- ✅ **Resuelto en Refactor 1:** En el Main cuando se crean los PCBs se agregan a una lista que a mi forma de ver simula ser los "NEW" tal vez conviene agregar al enum este estado para respetar aún más la teoría (tal vez no se va a ver nunca este estado pero está bueno remarcarlo que existe).
- ✅ **Resuelto en Refactor 2:** Los nombres de los procesos son P1, P2... El PID está hardcodeado. Buscar una forma de automatizar su creación.
- ✅ **Resuelto en Refactor 2:** Todos los campos tienen `get` y `set`. ¿Todos pueden ser cambiados una vez creado? PID y Nombre no deberían modificarse. Sacar el `set` de esos lugares o usar `private set`.

#### Interacción con el usuario

- ⬜ **Pendiente:** Lanzo los planificadores juntos (FCFS y SJF). Mejor sería dejar que el usuario elija qué tipo de planificador quiere ver en acción.
- ⬜ **Pendiente:** En el `Main` creo PCBs hardcodeados. Tal vez convenga que el usuario indique dinámicamente cuántos PCBs crear, con qué ráfaga cada uno, etc. O darle opción de usar datos de ejemplo o ingresar manualmente.

#### Algoritmos de planificación

- ✅ **Resuelto en Refactor 6:** **FCFS:** Solo ordena por tiempo de llegada y muestra el orden. No simula una cola FIFO real. Debería usar `Queue<PCB>` para que sea más fiel a la teoría.
- ⬜ **Pendiente:** **SJF:** Solo ordena por ráfaga de CPU y muestra el orden. No simula una cola de prioridad real. Podría usar `PriorityQueue<PCB>`.
- ⬜ **Pendiente:** Ambos procedimientos son primitivos: falta mostrar los estados que van teniendo los procesos, los tiempos de ejecución, qué va cambiando de cada PCB. No están respetando del todo la teoría (FCFS = cola simple, SJF = cola de prioridad).

#### Tiempos

- ✅ **Resuelto en Refactor 6:** Deberían pasar de Nuevo a la cola de preparados en su debido tiempo dinámicamente así aprovechamos que se vea en todo momento los procesos nuevos cómo se van encolando desencolando y ordenándose a medida que van llegando.

## Refactor 1: Reemplazar string Estado por enum EstadoProceso

- **Cambios realizados:**
  - Se creó el archivo `EstadoProceso.cs`.
  - Se definió el `enum` con los estados: `NEW`, `READY`, `RUNNING`, `TERMINATED`.
  - Se modificó la propiedad `Estado` en la clase `PCB` para que use el nuevo `enum`.
  - Se actualizó el `Main` para usar `EstadoProceso.NEW` con todos los procesos recién creados.
- **Problemas que resuelve:**
  - Estados como `string`: Ahora el compilador valida los valores, evitando errores por mayúsculas o typo.
- **Nuevos problemas detectados:**
  - ✅ **Resuelto en Refactor 6:** Los PCBs se crean con estado `NEW`, pero los planificadores (FCFS y SJF) aún no actualizan el estado a `READY`, `RUNNING` o `TERMINATED` durante la simulación.
- **Mejora adicional (no planeada originalmente):**
  - Se modificó la salida de FCFS y SJF para mostrar el estado actual de cada proceso, evidenciando que todos siguen en `NEW` (aún no se implementa el cambio de estado en los planificadores).

## Refactor 2: Separar clase PCB a su propio archivo y automatizar PID y Nombre

- **Cambios realizados:**
  - Se creó el archivo `PCB.cs` con la clase `PCB`.
  - Se agregó un constructor que recibe `ráfagaCPU` y `tiempoLlegada`.
  - Se automatizó la generación de `PID` (autoincremental) y `Nombre` ($"P{PID}"`).
  - Se cambiaron `PID`, `Nombre` y `TiempoLlegada` a `private set` (solo lectura).
  - Se simplificó la creación de procesos en `Main` (ya no se asignan manualmente esos campos).
- **Problemas que resuelve:**
  - PID y Nombre ya no están hardcodeados.
  - Se respeta el encapsulamiento (campos inmutables después de creados).

## Refactor 3: Separar FCFS y SJF a sus propios archivos

- **Cambios realizados:**
  - Se creó `PlanificadorFCFS.cs` con la clase estática `PlanificadorFCFS`.
  - Se creó `PlanificadorSJF.cs` con la clase estática `PlanificadorSJF`.
  - Se simplificó `Program.cs` (solo crea procesos y llama a los planificadores).
- **Problemas que resuelve:**
  - Organización del código: ya no está todo en un solo archivo.
- **Nuevos problemas detectados:**
  - ⬜ **Pendiente:** Estoy viendo que vamos a tener muchas cosas que se van a repetir entre todos los tipos de planificadores quizás con una clase abstracta que actúe de padre o base. Donde se pueda poner todo lo que se compartirá entre todos y luego las hijas hagan que cada uno maneje lo específico.

## Punto crítico: Análisis y planificación profunda de cómo voy a encarar la simulación FCFS paso a paso

### Objetivo

- ✅ **Resuelto en Refactor 6:** Implementar una simulación **FCFS paso a paso (unidad por unidad de tiempo)** que muestre:
  - Tabla de procesos iniciales (estado NEW)
  - Evolución de la simulación (tiempo, cola READY, CPU, ráfaga restante)
  - Resultados finales (tiempo total, orden de finalización)

### Cambios planificados en `PCB.cs`

- ✅ **Resuelto en Refactor 4:** Agregar propiedad `RafagaRestante` (con `get; set;`) para decrementar durante la simulación sin perder la ráfaga original.
- ✅ **Resuelto en Refactor 4:** Agregar propiedades calculadas para mejorar legibilidad:
  - `bool EsNuevo => Estado == EstadoProceso.NEW;`
  - `bool EstaReady => Estado == EstadoProceso.READY;`
  - `bool EstaRunning => Estado == EstadoProceso.RUNNING;`
  - `bool EstaTerminado => Estado == EstadoProceso.TERMINATED;`

### Variables de clase (PlanificadorFCFS.cs)

- `private static int _tiempoActual;` → tiempo actual de la simulación
- `private static Queue<PCB> _colaReady;` → cola FIFO de procesos listos
- `private static PCB _procesoActual;` → proceso en CPU (o `null`)
- `private static List<PCB> _procesos;` → copia ordenada de los procesos originales
- `private static List<string> _ordenFinalizacion;` → guarda el orden en que terminan
- `private static int _procesosTerminados;` → contador de procesos finalizados
- `private static Table _tablaSimulacion;` → tabla de Spectre.Console

### Métodos a implementar (PlanificadorFCFS.cs)

- `public static void Ejecutar(List<PCB> procesosOriginales)` → coordina toda la simulación
- `private static void Inicializar(List<PCB> procesosOriginales)` → prepara variables y ordena procesos por llegada
- `private static void MostrarTablaProcesosIniciales()` → muestra tabla de procesos en estado NEW
- `private static void ConfigurarTablaSimulacion()` → crea y configura la tabla de Spectre.Console
- `private static void AgregarProcesosQueLlegan()` → pasa procesos de `NEW` a `READY` o `RUNNING` según `TiempoLlegada`
- `private static void AsignarCPU()` → desencola el próximo proceso de `_colaReady` si CPU libre (FIFO)
- `private static void AgregarFilaATablaSimulacion()` → agrega una fila con el estado actual
- `private static void EjecutarUnidadDeTiempo()` → decrementa `RáfagaRestante` y maneja finalización
- `private static void MostrarResultadosFinales()` → muestra la tabla completa y el resumen

### Reglas de negocio que se implementarán

- **Llegada de procesos:** Cuando `_tiempoActual == TiempoLlegada`, el proceso pasa de `NEW` a `READY` (si CPU ocupada) o directamente a `RUNNING` (si CPU libre)
- **Asignación de CPU:** Si CPU libre y hay procesos en `_colaReady`, se toma el primero (FIFO)
- **Ejecución de unidad:** Se decrementa `RáfagaRestante` en **1** por cada unidad de tiempo
- **Finalización:** Cuando `RáfagaRestante == 0`, el proceso pasa a `TERMINATED`, se guarda en `_ordenFinalizacion` y se libera la CPU
- **Visualización:** Cuando `RáfagaRestante == 1`, se muestra `"1 ⏳ (termina)"` para indicar que en esa unidad terminará

### Dependencias externas

- ✅ **Resuelto en Refactor 5:** Agregar paquete `Spectre.Console` al proyecto (`dotnet add package Spectre.Console`)

### Pendiente (para próximos refactors)

- ⬜ **Pendiente:** Una vez que FCFS funcione, repetir la estructura para **SJF**
- ⬜ **Pendiente:** Migrar métodos comunes a **`PlanificadorBase`** (clase abstracta)
- ⬜ **Pendiente:** Agregar **interacción con el usuario** (elegir planificador, cargar datos dinámicamente)

## Refactor 4: Agregar RafagaRestante y propiedades calculadas a PCB

- **Cambios realizados:**
  - Se agregó la propiedad `RafagaRestante` (con `get; set;`) para decrementar durante la simulación sin perder la ráfaga original.
  - Se agregaron propiedades calculadas para mejorar la legibilidad:
    - `EsNuevo`, `EstaReady`, `EstaRunning`, `EstaTerminado`.
- **Problemas que resuelve:**
  - La simulación puede modificar la ráfaga restante sin afectar la ráfaga original.
  - El código es más legible (ya no hay que comparar `Estado == EstadoProceso.XXX` en todas partes).
- **Nuevos problemas detectados:**
  - ✅ **Resuelto en Refactor 6:** Los planificadores aún no usan `RafagaRestante` ni las nuevas propiedades.

## Refactor 5: Agregar paquete Spectre.Console

- **Cambios realizados:**
  - Se agregó el paquete Spectre.Console.
- **Problemas que resuelve:**
  - Me simplifica la creación de tablas para mostrar resultados por consola por ejemplo cuando haya que mostrar la ejecución de un planificador.

## Refactor 6: Implementación completa de FCFS paso a paso con mejoras visuales y límite de procesos

### Cambios realizados

#### 1. Simulación paso a paso (unidad por unidad de tiempo)
- Se implementó el bucle principal que avanza `_tiempoActual` de 1 en 1.
- En cada unidad de tiempo:
  - Se agregan a la cola READY los procesos cuyo `TiempoLlegada == _tiempoActual`.
  - Si la CPU está libre, se asigna el próximo proceso de la cola (FIFO).
  - Se decrementa `RafagaRestante` del proceso en CPU.
  - Si `RafagaRestante == 0`, el proceso TERMINA y se registra en `_ordenFinalizacion`.

#### 2. Límite máximo de procesos
- Se agregó validación al inicio de `Ejecutar()`: máximo 10 procesos.
- Si se supera, se muestra un error en rojo y se aborta la simulación.

#### 3. Colores automáticos para procesos (hasta 10)
- Se definió un array con 10 colores distintos (Blue, Green, Yellow, Magenta, Cyan, Orange1, Pink1, Aqua, Lime, Purple).
- Función `ObtenerNombreConColor(PCB)` que asigna color según PID (cíclico).
- Los nombres con color se muestran en:
  - Tabla de procesos iniciales.
  - Cola de READY (cada proceso con su color).
  - CPU (proceso en ejecución).
  - Orden de finalización (cada nombre con su color).

#### 4. Mejoras visuales en la tabla de simulación
- Se cambió el título de columna `"Ráfaga restante"` a `"Rafaga restante"` (sin acento).
- Cuando `RafagaRestante == 1`, se muestra `"1 (fin)"` en color rojo para destacar la finalización.
- La cola READY vacía se muestra como `"vacía"`.
- CPU sin proceso se muestra como `"---"`.

#### 5. Integración con Spectre.Console
- Se utilizó `Table` para mostrar resultados de forma profesional.
- Se usó `Rule` para separar secciones (procesos iniciales, resultados finales).
- Se usó `MarkupLine` para mostrar texto con colores.

### Problemas que resuelve

- ✅ FCFS ahora respeta fielmente la teoría (cola FIFO, llegadas dinámicas, cambios de estado).
- ✅ La simulación es educativa (se ve el estado en cada unidad de tiempo).
- ✅ El código es más profesional y presentable para el TP final.
- ✅ Se evitan errores visuales con el límite de 10 procesos.

### Nuevos problemas detectados

- ✅ **Resuelto en Refactor 7:** La lógica de UI (colores, tablas, formato) está acoplada dentro de `PlanificadorFCFS`. Conviene extraerla a un archivo aparte (`LayoutTabla.cs`) para reutilizarla en SJF y futuros planificadores.
- ⬜ **Pendiente:** Probar el comportamiento cuando dos procesos llegan en el mismo tiempo (ej. `TiempoLlegada = 2` para P2 y P3).

## Refactor 7: Extraer lógica de UI a LayoutTabla.cs

- **Cambios realizados:**
  - Se creó `LayoutTabla.cs` con métodos estáticos para crear tablas, formatear colas, CPU y ráfagas.
  - Se eliminó la lógica de `Spectre.Console` de `PlanificadorFCFS.cs`, delegando todo a `LayoutTabla`.
  - `PlanificadorFCFS.cs` quedó más limpio, solo con la lógica de simulación.
- **Problemas que resuelve:**
  - Separación clara entre lógica de negocio (planificación) y presentación (UI).
  - El código es más mantenible y reutilizable para futuros planificadores (SJF, RR).
- **Nuevos problemas detectados:**
  - ⬜ **Pendiente:** Implementar SJF reutilizando `LayoutTabla`.