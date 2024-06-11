using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace SistemaGestionEventos.Models
{
    public class EventoPersonal
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int IdEventoPersonal { get; set; }

        [ForeignKey("Evento")]
        public int IdEvento { get; set; }
        public virtual Evento Evento { get; set; }

        [ForeignKey("Personal")]
        public int IdPersonal { get; set; }
        public virtual Personal Personal { get; set; }
    }

}
