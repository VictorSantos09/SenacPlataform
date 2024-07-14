using BancoTalentos.Domain.Entity;
using BancoTalentos.Domain.Repositories.Contracts.Interfaces;
using BancoTalentos.Domain.Services.Professores.Dto;
using BancoTalentos.Domain.Services.Professores.Interfaces;
using FluentResults;

namespace BancoTalentos.Domain.Services.Professores;

internal class AtualizarProfessorService : IAtualizarProfessorService
{
    private readonly IPESSOAS_REPOSITORY _pessoas_repository;
    private readonly IPESSOAS_CONTATOS_REPOSITORY _pessoas_contatos_repository;
    private readonly IPESSOAS_HABILIDADES_DISCIPLINAS_REPOSITORY _pessoas_habilidades_disciplinas_repository;

    public AtualizarProfessorService(IPESSOAS_REPOSITORY pessoas_repository,
                                     IPESSOAS_CONTATOS_REPOSITORY pessoas_contatos_repository,
                                     IPESSOAS_HABILIDADES_DISCIPLINAS_REPOSITORY pessoas_habilidades_disciplinas_repository)
    {
        _pessoas_repository = pessoas_repository;
        _pessoas_contatos_repository = pessoas_contatos_repository;
        _pessoas_habilidades_disciplinas_repository = pessoas_habilidades_disciplinas_repository;
    }

    public async Task<Result> AtualizarAsync(ProfessorDto dto, CancellationToken cancellationToken = default)
    {
        if (dto.Id == 0)
        {
            return Result.Fail("O código do professor não foi informado.");
        }

        var professorEncontrado = await _pessoas_repository.GetByIdAsync(dto.Id, cancellationToken);

        if (professorEncontrado is null)
        {
            return Result.Fail($"O professor com o código {dto.Id} não foi encontrado.");
        }

        try
        {
            _pessoas_repository.BeginTransaction();

            int resultadoPessoa = await AtualizarProfessorAsync(dto, professorEncontrado, cancellationToken);

            if (resultadoPessoa == 0)
            {
                _pessoas_repository.Rollback();
                return Result.Fail("Não foi possível atualizar o professor.");
            }

            var resultadoContato = await AtualizarContatosAsync(dto, professorEncontrado.ID, cancellationToken);

            if (resultadoContato.IsFailed)
            {
                _pessoas_repository.Rollback();
            }

            await AtualizarDisciplinasAsync(dto, professorEncontrado.ID, cancellationToken);

            _pessoas_repository.Commit();

            return Result.Ok();
        }
        catch (Exception)
        {
            _pessoas_repository.Rollback();
            throw;
        }
    }

    private async Task<Result> AtualizarContatosAsync(ProfessorDto dto, int idProfessor, CancellationToken cancellationToken)
    {
        PESSOAS_CONTATOS? contatoEncontrado;
        int resultadoContato;

        foreach (var c in dto.Contatos)
        {
            contatoEncontrado = await _pessoas_contatos_repository.GetByIdAsync(c.Id, cancellationToken);

            if (contatoEncontrado is null)
            {
                return Result.Fail($"Não foi possível encontrar o contato com código {c.Id}.");
            }

            contatoEncontrado.CONTATO = c.Contato;
            contatoEncontrado.ID_PESSOA = idProfessor;
            contatoEncontrado.ID_TIPO_CONTATO = c.IdTipo;

            resultadoContato = await _pessoas_contatos_repository.UpdateAsync(contatoEncontrado, cancellationToken);

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
            pessoaHabilidadeEncontrada = await _pessoas_habilidades_disciplinas_repository.GetBy_IDX_PESSOAS_HABILIDADES_DISCIPLINAS_002(idProfessor, i, cancellationToken);

            if (pessoaHabilidadeEncontrada is not null)
            {
                await _pessoas_habilidades_disciplinas_repository.DeleteAsync(pessoaHabilidadeEncontrada, cancellationToken);
            }
            else
            {
                novaHabilidade = new PESSOAS_HABILIDADES_DISCIPLINAS { ID_PESSOA = idProfessor, ID_DISCIPLINA = i, DATA_CADASTRO = DateTime.Now };

                await _pessoas_habilidades_disciplinas_repository.InsertAsync(novaHabilidade, cancellationToken);
            }
        }
    }

    private async Task<int> AtualizarProfessorAsync(ProfessorDto dto, PESSOAS professorEncontrado, CancellationToken cancellationToken)
    {
        professorEncontrado.CARGA_HORARIA = dto.CargaHorariaSemanal;
        professorEncontrado.FOTO = dto.Foto;
        professorEncontrado.CARGO = dto.Cargo;
        professorEncontrado.NOME = dto.Nome;

        var resultadoPessoa = await _pessoas_repository.UpdateAsync(professorEncontrado, cancellationToken);
        return resultadoPessoa;
    }
}
