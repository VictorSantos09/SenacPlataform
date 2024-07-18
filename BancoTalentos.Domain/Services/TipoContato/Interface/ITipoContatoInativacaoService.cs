using FluentResults;

namespace BancoTalentos.Domain.Services.TipoContato.Interface;
public interface ITipoContatoInativacaoService
{
    Task<Result> AlterarAtivacaoAsync(int id, bool ativar, CancellationToken cancellationToken);
}