using FluentResults;

namespace BancoTalentos.Domain.Services.TipoContato.Interface;
internal interface ITipoContatoDeletarService
{
    Task<Result> DeletarAsync(int id, CancellationToken cancellationToken);
}