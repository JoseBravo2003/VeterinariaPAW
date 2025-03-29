namespace VeterinariaPAW.Models
{
    public class Producto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaCaducidad { get; set; }
        public string SKU { get; set; }
        public int IdCategoria { get; set; }
        public string Precio { get; set; }
        public int Stock { get; set; }
        public int IdProveedor { get; set; }
        public string Tipo { get; set; }
        public bool? Estado { get; set; }
        public bool? Clinico { get; set; }

        // Relación N:1 con Categoria
        public Categoria? Categoria { get; set; }

        // Relación N:1 con Proveedor
        public Proveedor? Proveedor { get; set; }
    }

}
