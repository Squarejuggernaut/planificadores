# Bitácora personal

## Me ayuda a ir documentando lo que voy haciendo en cada commit, el por qué y las posibles complicaciones que me puedan surgir.

### Versión inicial

- Creación de proyecto.
- Todo el código en Program.cs (class PCB y en el Main se crea lista de procesos hardcodeados, procedimiento FCFS y SJF muy básicos).
- Un primer acercamiento al comportamiento que deben tener FCFS y SJF.

### Problemas que detecto y que debo refactorizar

#### Organización del código

- ⬜ **Pendiente:** Está todo en un solo archivo, no se aprovecha el paradigma de objetos.

#### Clase PCB

- ✅ **Resuelto en Refactor 1:** Los estados del proceso son pocos y siempre los mismos. Podría ponerlos en un `enum` para evitar errores (mayúsculas, "Ready", etc.).
- ✅ **Resuelto en Refactor 1:** En el Main cuando se crean los PCBs se agregan a una lista que a mi forma de ver simula ser los "NEW" tal vez conviene agregar al enum este estado para respetar aún más la teoría (tal vez no se va a ver nunca este estado pero está bueno remarcarlo que existe).
- ⬜ **Pendiente:** Los nombres de los procesos son P1, P2... El PID está hardcodeado. Buscar una forma de automatizar su creación.
- ⬜ **Pendiente:** Todos los campos tienen `get` y `set`. ¿Todos pueden ser cambiados una vez creado? PID y Nombre no deberían modificarse. Sacar el `set` de esos lugares o usar `private set`.

#### Interacción con el usuario

- ⬜ **Pendiente:** Lanzo los planificadores juntos (FCFS y SJF). Mejor sería dejar que el usuario elija qué tipo de planificador quiere ver en acción.
- ⬜ **Pendiente:** En el `Main` creo PCBs hardcodeados. Tal vez convenga que el usuario indique dinámicamente cuántos PCBs crear, con qué ráfaga cada uno, etc. O darle opción de usar datos de ejemplo o ingresar manualmente.

#### Algoritmos de planificación

- ⬜ **Pendiente:** **FCFS:** Solo ordena por tiempo de llegada y muestra el orden. No simula una cola FIFO real. Debería usar `Queue<PCB>` para que sea más fiel a la teoría.
- ⬜ **Pendiente:** **SJF:** Solo ordena por ráfaga de CPU y muestra el orden. No simula una cola de prioridad real. Podría usar `PriorityQueue<PCB>`.
- ⬜ **Pendiente:** Ambos procedimientos son primitivos: falta mostrar los estados que van teniendo los procesos, los tiempos de ejecución, qué va cambiando de cada PCB. No están respetando del todo la teoría (FCFS = cola simple, SJF = cola de prioridad).

#### Tiempos

- ⬜ **Pendiente:** Deberían pasar de Nuevo a la cola de preparados en su debido tiempo dinámicamente así aprovechamos que se vea en todo momento los procesos nuevos cómo se van encolando desencolando y ordenándose a medida que van llegando.

## Refactor 1: Reemplazar string Estado por enum EstadoProceso

- **Cambios realizados:**
  - Se creó el archivo `EstadoProceso.cs`.
  - Se definió el `enum` con los estados: `NEW`, `READY`, `RUNNING`, `TERMINATED`.
  - Se modificó la propiedad `Estado` en la clase `PCB` para que use el nuevo `enum`.
  - Se actualizó el `Main` para usar `EstadoProceso.NEW` con todos los procesos recién creados.
- **Problemas que resuelve:**
  - Estados como `string`: Ahora el compilador valida los valores, evitando errores por mayúsculas o typo.
- **Nuevos problemas detectados:**
  - ⬜ **Pendiente:** Los PCBs se crean con estado `NEW`, pero los planificadores (FCFS y SJF) aún no actualizan el estado a `READY`, `RUNNING` o `TERMINATED` durante la simulación.
- **Mejora adicional (no planeada originalmente):**
  - Se modificó la salida de FCFS y SJF para mostrar el estado actual de cada proceso, evidenciando que todos siguen en `NEW` (aún no se implementa el cambio de estado en los planificadores).
