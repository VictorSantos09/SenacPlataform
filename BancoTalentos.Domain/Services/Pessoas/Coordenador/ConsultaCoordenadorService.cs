using BancoTalentos.Domain.Entity;
using BancoTalentos.Domain.Entity.Enums;
using BancoTalentos.Domain.Repositories.Contracts.Interfaces;
using BancoTalentos.Domain.Services.Imagem;
using BancoTalentos.Domain.Services.Imagem.Dto;
using BancoTalentos.Domain.Services.Pessoa.Interfaces;
using BancoTalentos.Domain.Services.Pessoas.Base;
using BancoTalentos.Domain.Services.Pessoas.Coordenador.Interfaces;
using FluentResults;

namespace BancoTalentos.Domain.Services.Pessoas.Coordenador;

internal class ConsultaCoordenadorService(IPESSOAS_REPOSITORY pessoas_repository,
                                          IImagemService imagemService,
                                          IConsultaPessoaService consultaPessoaService) : IConsultaCoordenadorService
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

    public async Task<Result<ImagemDTO>> GetFotoPerfilAsync(int id, CancellationToken cancellationToken = default)
    {
        return await consultaPessoaService.GetFotoPerfilAsync(id, "Não foi encontrado o coordenador", "O coordenador não tem foto de perfil", cancellationToken);
    }
}
