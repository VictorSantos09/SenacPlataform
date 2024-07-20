using FluentResults;

namespace BancoTalentos.Domain.Services.TipoContato.Interface;
public interface ITipoContatoDeletarService
{
    Task<Result> DeletarAsync(int id, CancellationToken cancellationToken);
}