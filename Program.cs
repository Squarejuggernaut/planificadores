using PlanificacionDeProcesos;
class Program
{
    static void Main(string[] args)
    {
        List<PCB> procesos = new List<PCB>
{
    new PCB(5, 0), new PCB(3, 0), new PCB(3, 0), new PCB(1, 0)
};
        //PlanificadorFCFS.Ejecutar(procesos);
        PlanificadorSJF.Ejecutar(procesos);
    }
}