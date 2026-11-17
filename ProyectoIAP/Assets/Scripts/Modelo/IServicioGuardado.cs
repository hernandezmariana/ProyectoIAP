namespace ExplosionesIncrementales.Modelo
{
    public interface IServicioGuardado
    {
        bool HayGuardado { get; }
        void Guardar(DatosGuardado datos);
        DatosGuardado Cargar();   // null si no hay o si los datos son inválidos
        void Borrar();
    }
}