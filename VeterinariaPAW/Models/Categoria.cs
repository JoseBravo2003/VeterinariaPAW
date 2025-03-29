namespace VeterinariaPAW.Models
{
    public class Categoria
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }

        // Relación 1:N con Producto
        public ICollection<Producto>? Productos { get; set; }
    }

}
