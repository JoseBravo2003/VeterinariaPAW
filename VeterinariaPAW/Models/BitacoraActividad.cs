using System;

namespace VeterinariaPAW.Models
{
    public class BitacoraActividad
    {
        public int Id { get; set; }
        public string Usuario { get; set; }
        public string Accion { get; set; }
        public string Controlador { get; set; }
        public string Vista { get; set; }
        public DateTime FechaHora { get; set; }
    }
}
