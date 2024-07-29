using BancoTalentos.Domain.Entity;
using BancoTalentos.Domain.Services.Pessoas.Professores.Dto;
using BancoTalentos.Domain.Services.Pessoas.Professores;
using Microsoft.AspNetCore.Http;
using BancoTalentos.Domain.Repositories.Contracts.Interfaces;
using FluentResults;

namespace BancoTalentos.Domain.Services.Pessoas.Base;

internal abstract class AtualizarPessoaServiceBase(IPESSOAS_REPOSITORY pessoas_repository,
                                 IPESSOAS_CONTATOS_REPOSITORY pessoas_contatos_repository,
                                 IPESSOAS_HABILIDADES_DISCIPLINAS_REPOSITORY pessoas_habilidades_disciplinas_repository)
{
    public async Task<Result> AtualizarPessoaAsync(ProfessorDto dto, CancellationToken cancellationToken = default)
    {
        if (dto.Id == 0)
        {
            return Result.Fail("O código do professor não foi informado.");
        }

        var professorEncontrado = await pessoas_repository.GetByIdAsync(dto.Id, cancellationToken);

        if (professorEncontrado is null)
        {
            return Result.Fail($"O professor com o código {dto.Id} não foi encontrado.");
        }

        try
        {
            pessoas_repository.BeginTransaction();

            int resultadoPessoa = await AtualizarProfessorAsync(dto, professorEncontrado, cancellationToken);

            if (resultadoPessoa == 0)
            {
                pessoas_repository.Rollback();
                return Result.Fail(PessoaMessages.NAO_FOI_POSSIVEL_ATUALIZAR);
            }

            var resultadoContato = await AtualizarContatosAsync(dto, professorEncontrado.ID, cancellationToken);

            if (resultadoContato.IsFailed)
            {
                pessoas_repository.Rollback();
                return Result.Fail(PessoaMessages.NAO_FOI_POSSIVEL_ATUALIZAR);
            }

            await AtualizarDisciplinasAsync(dto, professorEncontrado.ID, cancellationToken);

            pessoas_repository.Commit();

            return Result.Ok();
        }
        catch (Exception)
        {
            pessoas_repository.Rollback();
            throw;
        }
    }

    public async Task<Result> AtualizarFotoPerfilAsync(IFormFile fotoPerfil, int id, CancellationToken cancellationToken = default)
    {
        var professorExiste = await pessoas_repository.ExistsAsync("PESSOAS", id, cancellationToken);

        if (!professorExiste)
        {
            return Result.Fail($"O professor com o código {id} não foi encontrado.");
        }

        //_ = await ArmazenarFotoPerfilOnDiskAsync(fotoPerfil, cancellationToken: cancellationToken);

        return Result.Ok();
    }

    private async Task<Result> AtualizarContatosAsync(ProfessorDto dto, int idProfessor, CancellationToken cancellationToken)
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

    private async Task AtualizarDisciplinasAsync(ProfessorDto dto, int idProfessor, CancellationToken cancellationToken)
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

    private async Task<int> AtualizarProfessorAsync(ProfessorDto dto, PESSOAS professorEncontrado, CancellationToken cancellationToken)
    {
        professorEncontrado.CARGA_HORARIA = dto.CargaHorariaSemanal;
        professorEncontrado.FOTO = dto.Foto;
        professorEncontrado.CARGO = dto.Cargo;
        professorEncontrado.NOME = dto.Nome;

        var resultadoPessoa = await pessoas_repository.UpdateAsync(professorEncontrado, cancellationToken);
        return resultadoPessoa;
    }
}
