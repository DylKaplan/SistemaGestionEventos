using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace SistemaGestionEventos.Models
{
    public class Lugar
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdLugar { get; set; }

        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public bool tieneEscaleras { get; set; }

       
    }
}
