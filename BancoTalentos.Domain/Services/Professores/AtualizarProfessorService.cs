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

    public AtualizarProfessorService(IPESSOAS_REPOSITORY pessoas_repository,
                                     IPESSOAS_CONTATOS_REPOSITORY pessoas_contatos_repository)
    {
        _pessoas_repository = pessoas_repository;
        _pessoas_contatos_repository = pessoas_contatos_repository;
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

            PESSOAS_CONTATOS? contatoEncontrado;
            int resultadoContato;
            foreach (var c in dto.Contatos)
            {
                contatoEncontrado = await _pessoas_contatos_repository.GetByIdAsync(c.Id, cancellationToken);

                if (contatoEncontrado is null)
                {
                    _pessoas_repository.Rollback();
                    return Result.Fail($"Não foi possível encontrar o contato com código {c.Id}.");
                }

                contatoEncontrado.CONTATO = c.Contato;
                contatoEncontrado.ID_PESSOA = professorEncontrado.ID;
                contatoEncontrado.ID_TIPO_CONTATO = c.IdTipo;

                resultadoContato = await _pessoas_contatos_repository.UpdateAsync(contatoEncontrado, cancellationToken);

                if (resultadoContato == 0)
                {
                    _pessoas_repository.Rollback();
                    return Result.Fail($"Não foi possível atualizar o contato {c.Contato}.");
                }
            }

            _pessoas_repository.Commit();
            return Result.Ok();
        }
        catch (Exception)
        {
            _pessoas_repository.Rollback();
            throw;
        }
    }

    private async Task<int> AtualizarProfessorAsync(ProfessorDto dto, PESSOAS professorEncontrado, CancellationToken cancellationToken)
    {
        professorEncontrado.CARGA_HORARIA = dto.CargaHoraria;
        professorEncontrado.FOTO = dto.Foto;
        professorEncontrado.CARGO = dto.Cargo;
        professorEncontrado.NOME = dto.Nome;

        var resultadoPessoa = await _pessoas_repository.UpdateAsync(professorEncontrado, cancellationToken);
        return resultadoPessoa;
    }
}
