using AgendamentoAPI.Model.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AgendamentoAPI.Model
{
    [Table("agendamento")]
    public class Agendamento : BaseModel
    {
        [Column("data")]
        [Required]
        public DateTime Data { get; set; }

        public int OficinaId { get; set; }

        public virtual Oficina Oficina { get; set; }

        [Column("tipoServico")]
        [Required]
        public int TipoServico { get; set; }

    }
}
