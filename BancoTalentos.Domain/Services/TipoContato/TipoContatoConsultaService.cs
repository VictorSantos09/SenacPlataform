using BancoTalentos.Domain.Entity;
using BancoTalentos.Domain.Repositories.Contracts.Interfaces;
using BancoTalentos.Domain.Services.TipoContato.Interface;
using FluentResults;

namespace BancoTalentos.Domain.Services.TipoContato;
internal class TipoContatoConsultaService : ITipoContatoConsultaService
{
    private readonly ITIPOS_CONTATOS_REPOSITORY _tipos_contatos_repository;

    public TipoContatoConsultaService(ITIPOS_CONTATOS_REPOSITORY tipos_contatos_repository)
    {
        _tipos_contatos_repository = tipos_contatos_repository;
    }

    public async Task<Result<IEnumerable<TIPOS_CONTATOS>>> GetAllAsync(CancellationToken cancellationToken)
    {
        var data = await _tipos_contatos_repository.GetAllAsync(cancellationToken);

        return !data.Any() ? Result.Fail("Nenhum tipo de contato encontrado.") : Result.Ok(data);
    }

    public async Task<Result<TIPOS_CONTATOS>> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        var data = await _tipos_contatos_repository.GetByIdAsync(id, cancellationToken);

        return data is null ? Result.Fail($"Tipo de contato com código {id} não encontrado.") : Result.Ok(data);
    }

    public async Task<Result<TIPOS_CONTATOS>> GetEmailAsync()
    {
        var data = await _tipos_contatos_repository.GetEmailAsync();

        return data is null ? Result.Fail($"Tipo de contato email não encontrado.") : Result.Ok(data);
    }

    public async Task<Result<TIPOS_CONTATOS>> GetTelefoneAsync()
    {
        var data = await _tipos_contatos_repository.GetTelefoneAsync();

        return data is null ? Result.Fail($"Tipo de contato telefone não encontrado.") : Result.Ok(data);
    }
}
