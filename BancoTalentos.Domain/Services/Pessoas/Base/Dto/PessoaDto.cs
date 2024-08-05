using BancoTalentos.Domain.Entity.Enums;
using BancoTalentos.Domain.Services.Contato.Dto;
using Microsoft.AspNetCore.Http;

namespace BancoTalentos.Domain.Services.Pessoas.Base.Dto;

public abstract record PessoaDto
{
    public required string Nome { get; set; }
    public IFormFile Foto { get; set; }
    internal  abstract CARGO Cargo { get; init; }
    public required int CargaHorariaSemanal { get; set; }
    public IEnumerable<ContatoDto> Contatos { get; set; }
    public int Id { get; set; }
    public IEnumerable<int> IdsDisciplinas { get; set; }
}
