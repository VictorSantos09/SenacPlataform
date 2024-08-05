using BancoTalentos.Domain.Repositories.Contracts.Interfaces;
using BancoTalentos.Domain.Services.Foto;
using BancoTalentos.Domain.Services.Imagem.Dto;
using BancoTalentos.Domain.Services.Pessoa.Interfaces;
using FluentResults;

namespace BancoTalentos.Domain.Services.Pessoa;

internal class ConsultaPessoaService(IPESSOAS_REPOSITORY pessoas_repository, IImagemService imagemService) : IConsultaPessoaService
{
    private const string CNT_NAO_ENCONTRADO = "Não foi encontrado a pessoa.";
    private const string CNT_SEM_FOTO = "A pessoa não tem foto de perfil.";

    public async Task<Result<ImagemDTO>> GetFotoPerfilAsync(int id,
                                                            string mensagemNaoEncontrado = CNT_NAO_ENCONTRADO,
                                                            string mensagemSemFoto = CNT_SEM_FOTO,
                                                            CancellationToken cancellationToken = default)
    {
        var pessoaEncontrada = await pessoas_repository.GetByIdAsync(id, cancellationToken);

        if (pessoaEncontrada is null)
        {
            return Result.Fail(mensagemNaoEncontrado);
        }

        if (pessoaEncontrada.FOTO is null)
        {
            return Result.Fail(mensagemSemFoto);
        }

        return Result.Ok(await imagemService.GetImagemOnDisk(pessoaEncontrada.FOTO, cancellationToken));
    }
}
