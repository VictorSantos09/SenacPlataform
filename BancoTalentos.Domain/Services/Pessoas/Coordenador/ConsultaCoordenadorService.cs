using BancoTalentos.Domain.Entity;
using BancoTalentos.Domain.Entity.Enums;
using BancoTalentos.Domain.Repositories.Contracts.Interfaces;
using BancoTalentos.Domain.Services.Foto;
using BancoTalentos.Domain.Services.Pessoas.Base;
using BancoTalentos.Domain.Services.Pessoas.Coordenador.Interfaces;
using FluentResults;
using SixLabors.ImageSharp;

namespace BancoTalentos.Domain.Services.Pessoas.Coordenador;

internal class ConsultaCoordenadorService(IPESSOAS_REPOSITORY pessoas_repository, IImagemService imagemService) : IConsultaCoordenadorService
{
    public async Task<Result<IEnumerable<PESSOAS>>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return Result.Ok(await pessoas_repository.GetAllByCargoAsync(CARGO.COORDENADOR, cancellationToken));
    }

    public async Task<Result<PESSOAS>> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var result = await pessoas_repository.GetByIdAsync(id, cancellationToken);

        return result is not null
            ? Result.Ok(result)
            : Result.Fail(PessoaMessages.NAO_ENCONTRADO);
    }
    
    public async Task<Result<MemoryStream>> GetFotoPerfilAsync(int id, CancellationToken cancellationToken = default)
    {
        var pessoaEncontrada = await pessoas_repository.GetByIdAsync(id, cancellationToken);

        if(pessoaEncontrada is null)
        {
            return Result.Fail($"Não foi encontrado o coordenador com o código {id}");
        }
        return Result.Ok(await imagemService.GetImagemOnDisk(pessoaEncontrada.FOTO, cancellationToken));
    }
}
