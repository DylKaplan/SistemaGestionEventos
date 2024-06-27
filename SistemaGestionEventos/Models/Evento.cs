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

        [Required(ErrorMessage = "La descripción es obligatoria.")]
        public string Descripcion { get; set; }

        [ForeignKey("Lugar")]
        [Display(Name = "Nombre lugar")]
        /*[Required(ErrorMessage = "El lugar es obligatorio.")]*/
        public int IdLugar { get; set; }
        public virtual Lugar Lugar { get; set; }

        public string Equipamiento { get; set; }
        [Display(Name = "Presupuesto ($)")]
        [Range(0, double.MaxValue, ErrorMessage = "El presupuesto debe ser un valor positivo.")]
        public int Presupuesto { get; set; }
        [Display(Name = "Fecha inicio")]
        public DateTime FechaInicio { get; set; }

        [Display(Name = "Fecha fin")]
        public DateTime FechaFin { get; set; }

        /*fecha fin tiene que ser mayor a fecha inicio. y ambas tienen que ser mayor a hoy. sino sale mensaje error
         (esto se hace en vista create de Evento, y si anda ok en la vista edit*/

        [ForeignKey("Cliente")]
        [Display(Name = "Nombre cliente")]
        public int IdCliente { get; set; }
        public virtual Cliente Cliente { get; set; }

        /*[ForeignKey("EventoPersonal")]
        public int IdEventoPersonal { get; set; }
        public virtual EventoPersonal EventoPersonal { get; set; }*/ //esto no va aca, verdad?


        [EnumDataType(typeof(EstadoEvento))]
        public EstadoEvento Estado { get; set; }
    }
}
