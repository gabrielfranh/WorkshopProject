using OficinasAPI.Model.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OficinasAPI.Model
{
    [Table("oficina")]
    public class Oficina : BaseModel
    {
        public Oficina()
        {
            Agendamentos = new HashSet<Agendamento>();
        }

        [Column("nome")]
        [Required]
        public string Nome { get; set; }

        [Column("cnpj")]
        [Required]
        public string Cnpj { get; set; }

        [Column("cargaTrabalhoDiaria")]
        [Required]
        public int CargaTrabalhoDiaria { get; set; }

        [Column("senha")]
        [Required]
        public string Senha { get; set; }

        public virtual ICollection<Agendamento> Agendamentos { get; set; }
    }
}
