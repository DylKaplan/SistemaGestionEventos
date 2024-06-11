using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace SistemaGestionEventos.Models
{
    public class Personal
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] 
        public int IdPersonal { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Telefono { get; set; }

        public bool EsPersonalTecnico { get; set; }
        public bool EsPersonalFlete { get; set; }

        [EnumDataType(typeof(Especializacion))]
        public Especializacion Especializacion { get; set; }

        public string Patente { get; set; }

        [EnumDataType(typeof(TamanioFlete))]
        public TamanioFlete TamanioFlete { get; set; }



    }
}
