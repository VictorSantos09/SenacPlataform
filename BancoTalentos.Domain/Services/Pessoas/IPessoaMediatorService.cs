using BancoTalentos.Domain.Services.Pessoas.Base.Dto;
using FluentResults;

namespace BancoTalentos.Domain.Services.Pessoas;

public interface IPessoaMediatorService
{
    Task<Result> CadastrarAsync(PessoaDto dto, CancellationToken cancellationToken = default);
    Task<PessoaDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<Result> AtualizarAsync(int id, PessoaDto dto, CancellationToken cancellationToken = default);
}