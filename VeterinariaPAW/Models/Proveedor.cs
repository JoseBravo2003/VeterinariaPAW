namespace VeterinariaPAW.Models
{
    public class Proveedor
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Telefono { get; set; }
        public string Correo { get; set; }
        public string Direccion { get; set; }

        // Relación 1:N con Producto
        public ICollection<Producto> Productos { get; set; }
    }

}
