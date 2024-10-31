using BancoTalentos.Domain.Entity;
using BancoTalentos.Domain.Repositories.Contracts.Interfaces;
using BancoTalentos.Domain.Services.Imagem;
using BancoTalentos.Domain.Services.Imagem.Dto;
using BancoTalentos.Domain.Services.Pessoas.Base.Dto;
using FluentResults;
using Microsoft.AspNetCore.Http;
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
                var resultadoFoto = await AtualizarFotoPerfilAsync(dto.Foto, pessoaEncontrada, cancellationToken);
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

        //_ = await ArmazenarFotoPerfilOnDiskAsync(fotoPerfil, cancellationToken: cancellationToken);

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
                return Result.Fail($"Não foi possível encontrar o contato com código {c.Id}.");
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

    private async Task AtualizarDisciplinasAsync(PessoaDto dto, int idProfessor, CancellationToken cancellationToken)
    {
        PESSOAS_HABILIDADES_DISCIPLINAS? pessoaHabilidadeEncontrada;
        PESSOAS_HABILIDADES_DISCIPLINAS novaHabilidade;

        foreach (var i in dto.IdsDisciplinas)
        {
            pessoaHabilidadeEncontrada = await pessoas_habilidades_disciplinas_repository.GetBy_IDX_PESSOAS_HABILIDADES_DISCIPLINAS_002(idProfessor, i, cancellationToken);

            if (pessoaHabilidadeEncontrada is not null)
            {
                await pessoas_habilidades_disciplinas_repository.DeleteAsync(pessoaHabilidadeEncontrada, cancellationToken);
            }
            else
            {
                novaHabilidade = new PESSOAS_HABILIDADES_DISCIPLINAS { ID_PESSOA = idProfessor, ID_DISCIPLINA = i, DATA_CADASTRO = DateTime.Now };

                await pessoas_habilidades_disciplinas_repository.InsertAsync(novaHabilidade, cancellationToken);
            }
        }
    }

    private static void AtualizarDadosRestantesProfessor(PessoaDto dto, PESSOAS professorEncontrado)
    {
        professorEncontrado.CARGA_HORARIA = dto.CargaHorariaSemanal;
        professorEncontrado.CARGO = dto.Cargo;
        professorEncontrado.NOME = dto.Nome;
    }
    #endregion

    private async Task<Result<string>> AtualizarFotoPerfilAsync(ImagemBase64DTO novaFoto, PESSOAS pessoa, CancellationToken cancellationToken = default)
    {
        if (pessoa.FOTO is null)
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
