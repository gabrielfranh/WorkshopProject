using Utils;

namespace AgendamentoAPI.DTO
{
    public class AgendamentoDTO
    {
        public int Id { get; set; }

        public DateTime Data { get; set; }

        public int OficinaId { get; set; }

        public string TipoServico { get; set; }

        public int? UnidadeTrabalhoServico { get; set; }
    }
}
