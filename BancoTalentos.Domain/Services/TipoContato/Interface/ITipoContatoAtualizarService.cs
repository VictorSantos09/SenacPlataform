using BancoTalentos.Domain.Services.TipoContato.Dto;
using FluentResults;

namespace BancoTalentos.Domain.Services.TipoContato.Interface;
public interface ITipoContatoAtualizarService
{
    Task<Result> AtualizarAsync(TipoContatoDto dto, CancellationToken cancellationToken);
}