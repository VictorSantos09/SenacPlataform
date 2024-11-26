using BancoTalentos.Domain.Services.Pessoas.Base.Dto;
using FluentResults;

namespace BancoTalentos.Domain.Services.Pessoas;

public interface IPessoaMediatorService
{
    Task<Result> AtualizarAsync(PessoaDto dto, CancellationToken cancellationToken = default);
    Task<Result> CadastrarAsync(PessoaDto dto, CancellationToken cancellationToken = default);
}