using BancoTalentos.Domain.Entity.Enums;
using BancoTalentos.Domain.Services.Contato.Dto;
using BancoTalentos.Domain.Services.Imagem.Dto;
using BancoTalentos.Domain.Services.Pessoas.Base.Dto;

namespace BancoTalentos.UI.Components.Pages.Pessoas.dto;

public record PessoaCadastroDto
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public ImagemBase64DTO? Foto { get; set; }
    public CARGO Cargo { get; set; }
    public int CargaHoraria { get; set; }
    public IList<ContatoDto> Contatos { get; set; } = [];
    public IEnumerable<int> HabilidadesDisciplinas { get; set; } = [];
    public IList<PessoaFormacoesDto> Formacoes { get; set; } = [];
}