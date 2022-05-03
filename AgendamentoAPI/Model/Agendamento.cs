using AgendamentoAPI.Model.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Utils;

namespace AgendamentoAPI.Model
{
    [Table("agendamento")]
    public class Agendamento : BaseModel
    {
        [Column("data")]
        [Required]
        public DateTime Data { get; set; }

        [ForeignKey("Oficina")]
        public int OficinaId { get; set; }
        public Oficina Oficina { get; set; }

        [Column("tipoServico")]
        [Required]
        public TipoServicoEnum TipoServico { get; set; }

        [Column("unidadeTrabalhoServico")]
        [Required]
        public int? UnidadeTrabalhoServico { get; set; }

    }
}
