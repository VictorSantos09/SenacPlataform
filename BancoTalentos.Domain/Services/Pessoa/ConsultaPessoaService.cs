using BancoTalentos.Domain.Repositories.Contracts.Interfaces;
using BancoTalentos.Domain.Services.Contato.Dto;
using BancoTalentos.Domain.Services.Imagem;
using BancoTalentos.Domain.Services.Imagem.Dto;
using BancoTalentos.Domain.Services.Pessoa.Interfaces;
using BancoTalentos.Domain.Services.Pessoas.Base.Dto;
using FluentResults;

namespace BancoTalentos.Domain.Services.Pessoa;

internal class ConsultaPessoaService(IPESSOAS_REPOSITORY pessoas_repository,
                                     IImagemService imagemService,
                                     IPESSOAS_CONTATOS_REPOSITORY pessoas_contatos_repository,
                                     IPESSOAS_HABILIDADES_DISCIPLINAS_REPOSITORY pessoas_habilidades_disciplinas_repository) : IConsultaPessoaService
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

    public async Task<PessoaDto> GetById_Detalhado(int id)
    {
        var pessoa = await pessoas_repository.GetByIdAsync(id);

        var pessoaHabilidades = await pessoas_habilidades_disciplinas_repository.GetByIdPessoaAsync(id);

        var pessoaContatos = await pessoas_contatos_repository.GetByIdPessoaAsync(id);

        var foto = await GetFotoPerfilAsync(id);

        return new PessoaDto()
        {
            CargaHorariaSemanal = pessoa.CARGA_HORARIA,
            Cargo = pessoa.CARGO,
            Foto = foto.IsSuccess ? new ImagemBase64DTO() { Image = foto.Value?.Imagem } : null,
            Id = pessoa.ID,
            Nome = pessoa.NOME,
            IdsDisciplinas = pessoaHabilidades.Select(x => x.ID_DISCIPLINA).ToList(),
            Contatos = pessoaContatos.Select(x => new ContatoDto()
            {
                Id = x.ID,
                Contato = x.CONTATO,
                IdTipo = x.ID_TIPO_CONTATO
            }).ToList(),
        };
    }
}
