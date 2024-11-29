using BancoTalentos.Domain.Entity;
using BancoTalentos.Domain.Repositories.Contracts.Interfaces;
using BancoTalentos.Domain.Services.Imagem;
using BancoTalentos.Domain.Services.Imagem.Dto;
using BancoTalentos.Domain.Services.Pessoas.Base.Dto;
using FluentResults;
using SenacPlataform.Shared.Exceptions.ImagemConfig;

namespace BancoTalentos.Domain.Services.Pessoas.Base;

internal abstract class AtualizarPessoaServiceBase(IPESSOAS_REPOSITORY pessoas_repository,
                                 IPESSOAS_CONTATOS_REPOSITORY pessoas_contatos_repository,
                                 IPESSOAS_HABILIDADES_DISCIPLINAS_REPOSITORY pessoas_habilidades_disciplinas_repository,
                                 IImagemService imagemService)
{
    public async Task<Result> AtualizarPessoaAsync(PessoaDto dto, CancellationToken cancellationToken = default)
    {
        if (dto.Id == 0)
        {
            return Result.Fail("O código da pessoa não foi informado.");
        }

        var pessoaEncontrada = await pessoas_repository.GetByIdAsync(dto.Id, cancellationToken);

        if (pessoaEncontrada is null)
        {
            return Result.Fail($"A pessoa com o código {dto.Id} não foi encontrado.");
        }

        try
        {
            pessoas_repository.BeginTransaction();

            var resultadoContato = await AtualizarContatosAsync(dto, pessoaEncontrada.ID, cancellationToken);

            if (resultadoContato.IsFailed)
            {
                pessoas_repository.Rollback();
                return resultadoContato;
            }

            await AtualizarDisciplinasAsync(dto, pessoaEncontrada.ID, cancellationToken);

            bool fotoAtualizada = false;
            var nomeFotoAntiga = pessoaEncontrada.FOTO;

            if (dto.Foto is not null)
            {
                var resultadoFoto = await AtualizarFotoPerfilAsync(dto.Foto, cancellationToken);
                if (resultadoFoto.IsSuccess)
                {
                    pessoaEncontrada.FOTO = resultadoFoto.Value;
                    fotoAtualizada = true;
                }
            }

            AtualizarDadosRestantesProfessor(dto, pessoaEncontrada);
            var resultadoPessoa = await pessoas_repository.UpdateAsync(pessoaEncontrada, cancellationToken);

            if (resultadoPessoa == 0)
            {
                pessoas_repository.Rollback();

                if (pessoaEncontrada.FOTO is null)
                {
                    throw new ImagemErroAtualizarException("Ocorreu um erro ao reverter para a foto de perfil anterior ao atualizar a pessoa.",
                                                                                nomeFotoAntiga,
                                                                                pessoaEncontrada.FOTO);
                }

                imagemService.DeletarImagemOnDisk(pessoaEncontrada.FOTO);
                return Result.Fail(PessoaMessages.NAO_FOI_POSSIVEL_ATUALIZAR);
            }

            if (fotoAtualizada && nomeFotoAntiga is not null)
            {
                imagemService.DeletarImagemOnDisk(nomeFotoAntiga);
            }

            pessoas_repository.Commit();

            return Result.Ok();
        }
        catch (Exception)
        {
            pessoas_repository.Rollback();
            throw;
        }
    }
    #region ATUALIZAR RELACIONAMENTOS

    public async Task<Result> AtualizarPessoaFotoPerfilAsync(ImagemBase64DTO fotoPerfil, int id, CancellationToken cancellationToken = default)
    {
        var professorExiste = await pessoas_repository.ExistsAsync("PESSOAS", id, cancellationToken);

        if (!professorExiste)
        {
            return Result.Fail($"O professor com o código {id} não foi encontrado.");
        }

        await imagemService.ArmazenarFotoPerfilOnDiskAsync(fotoPerfil, cancellationToken);

        return Result.Ok();
    }

    private async Task<Result> AtualizarContatosAsync(PessoaDto dto, int idProfessor, CancellationToken cancellationToken)
    {
        PESSOAS_CONTATOS? contatoEncontrado;
        int resultadoContato;

        foreach (var c in dto.Contatos)
        {
            contatoEncontrado = await pessoas_contatos_repository.GetByIdAsync(c.Id, cancellationToken);

            if (contatoEncontrado is null)
            {
                await pessoas_contatos_repository.InsertAsync(new PESSOAS_CONTATOS()
                {
                    CONTATO = c.Contato,
                    ID_PESSOA = idProfessor,
                    ID_TIPO_CONTATO = c.IdTipo
                });

                break;
            }

            contatoEncontrado.CONTATO = c.Contato;
            contatoEncontrado.ID_PESSOA = idProfessor;
            contatoEncontrado.ID_TIPO_CONTATO = c.IdTipo;

            resultadoContato = await pessoas_contatos_repository.UpdateAsync(contatoEncontrado, cancellationToken);

            if (resultadoContato == 0)
            {
                return Result.Fail($"Não foi possível atualizar o contato {c.Contato}.");
            }
        }

        return Result.Ok();
    }

    private async Task AtualizarDisciplinasAsync(PessoaDto dto, int idPessoa, CancellationToken cancellationToken)
    {
        if (dto.IdsDisciplinas is null || !dto.IdsDisciplinas.Any())
        {
            await pessoas_habilidades_disciplinas_repository.DeletarHabilidadesPessoa(idPessoa);
            return;
        }

        var habilidadesPessoa = await pessoas_habilidades_disciplinas_repository.GetByIdPessoaAsync(idPessoa, cancellationToken);
        var idsExistentes = habilidadesPessoa.Select(h => h.ID_DISCIPLINA).ToHashSet();

        var idsParaAdicionar = dto.IdsDisciplinas.Except(idsExistentes);
        var idsParaRemover = idsExistentes.Except(dto.IdsDisciplinas);

        foreach (var idDisciplina in idsParaAdicionar)
        {
            var novaHabilidade = new PESSOAS_HABILIDADES_DISCIPLINAS
            {
                ID_PESSOA = idPessoa,
                ID_DISCIPLINA = idDisciplina,
                DATA_CADASTRO = DateTime.Now
            };

            await pessoas_habilidades_disciplinas_repository.InsertAsync(novaHabilidade, cancellationToken);
        }

        foreach (var idDisciplina in idsParaRemover)
        {
            await pessoas_habilidades_disciplinas_repository.DeleteBy_IDX_PESSOAS_HABILIDADES_DISCIPLINAS_002(idPessoa, idDisciplina);
        }
    }

    private static void AtualizarDadosRestantesProfessor(PessoaDto dto, PESSOAS professorEncontrado)
    {
        professorEncontrado.CARGA_HORARIA = dto.CargaHorariaSemanal;
        professorEncontrado.CARGO = dto.Cargo;
        professorEncontrado.NOME = dto.Nome;
    }
    #endregion

    private async Task<Result<string>> AtualizarFotoPerfilAsync(ImagemBase64DTO novaFoto, CancellationToken cancellationToken = default)
    {
        if (novaFoto is null)
        {
            throw new ImagemNaoInformadaException("A foto de perfil para ser atualizada deve ser informada.");
        }

        var resultadoGravacaoNovaImagem = await imagemService.ArmazenarFotoPerfilOnDiskAsync(novaFoto, cancellationToken);

        if (resultadoGravacaoNovaImagem.IsSuccess)
        {
            return Result.Ok(resultadoGravacaoNovaImagem.Value);
        }

        return Result.Fail("Não foi possível armazenar a nova imagem. Portanto não será alterada.");
    }
}
