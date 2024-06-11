using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace SistemaGestionEventos.Models
{
    public class Evento
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdEvento { get; set; }
        public string Descripcion { get; set; }

        [ForeignKey("Lugar")]
        [Display(Name = "Nombre lugar")]
        public int IdLugar { get; set; }
        public virtual Lugar Lugar { get; set; }

        public string Equipamiento { get; set; }
        public int Presupuesto { get; set; }
        [Display(Name = "Fecha inicio")]
        public DateTime FechaInicio { get; set; }

        [Display(Name = "Fecha fin")]
        public DateTime FechaFin { get; set; }

        

        [ForeignKey("Cliente")]
        public int IdCliente { get; set; }
        public virtual Cliente Cliente { get; set; }

        /*[ForeignKey("EventoPersonal")]
        public int IdEventoPersonal { get; set; }
        public virtual EventoPersonal EventoPersonal { get; set; }*/ //esto no va aca, verdad?


        [EnumDataType(typeof(EstadoEvento))]
        public EstadoEvento Estado { get; set; }
    }
}
