using BancoTalentos.Domain.Entity;
using FluentResults;

namespace BancoTalentos.Domain.Services.TipoContato.Interface;
public interface ITipoContatoConsultaService
{
    Task<Result<IEnumerable<TIPOS_CONTATOS>>> GetAllAsync(CancellationToken cancellationToken);
    Task<Result<TIPOS_CONTATOS>> GetByIdAsync(int id, CancellationToken cancellationToken);
}