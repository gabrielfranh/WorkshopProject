using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AgendamentoAPI.Model.Base
{
    public class BaseModel
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
    }
}
