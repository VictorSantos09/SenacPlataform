using FluentResults;

namespace BancoTalentos.Domain.Services.TipoContato.Interface;
public interface ITipoContatoAtualizarService
{
    Task<Result> AtualizarAsync(int id, string tipo, CancellationToken cancellationToken);
}