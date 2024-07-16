using BancoTalentos.Domain.Services.TipoContato.Dto;
using FluentResults;

namespace BancoTalentos.Domain.Services.TipoContato.Interface;
public interface ITipoContatoAdicionarService
{
    Task<Result> AdicionarAsync(TipoContatoDto dto, CancellationToken cancellationToken);
}