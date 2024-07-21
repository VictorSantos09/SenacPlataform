using BancoTalentos.Domain.Entity;
using BancoTalentos.Domain.Repositories.Contracts.Interfaces;
using BancoTalentos.Domain.Services.Pessoas.Professores;
using BancoTalentos.Domain.Services.Pessoas.Professores.Dto;
using FluentResults;
using FluentValidation;
using SenacPlataform.Shared.Extensions;

namespace BancoTalentos.Domain.Services.Pessoas.Base;

public abstract class CadastrarPessoaServiceBase
{
    public const int CARGA_HORARIA_SEMANA_MAXIMA_CLT = 44;
    private readonly IDISCIPLINAS_REPOSITORY _disciplinas_repository;
    private readonly IPESSOAS_CONTATOS_REPOSITORY _pessoas_contatos_repository;
    private readonly IPESSOAS_HABILIDADES_DISCIPLINAS_REPOSITORY _pessoas_habilidades_disciplinas_repository;
    private readonly IPESSOAS_REPOSITORY _pessoas_repository;
    private readonly ITIPOS_CONTATOS_REPOSITORY _tipos_contatos_repository;
    private readonly IValidator<PESSOAS> _validator;

    public CadastrarPessoaServiceBase(IDISCIPLINAS_REPOSITORY disciplinas_repository,
                                      IPESSOAS_CONTATOS_REPOSITORY pessoas_contatos_repository,
                                      IPESSOAS_HABILIDADES_DISCIPLINAS_REPOSITORY pessoas_habilidades_disciplinas_repository,
                                      IPESSOAS_REPOSITORY pessoas_repository,
                                      ITIPOS_CONTATOS_REPOSITORY tipos_contatos_repository,
                                      IValidator<PESSOAS> validator)
    {
        _disciplinas_repository = disciplinas_repository;
        _pessoas_contatos_repository = pessoas_contatos_repository;
        _pessoas_habilidades_disciplinas_repository = pessoas_habilidades_disciplinas_repository;
        _pessoas_repository = pessoas_repository;
        _tipos_contatos_repository = tipos_contatos_repository;
        _validator = validator;
    }

    public async Task<Result> CadastrarPessoaAsync(PessoaDto dto, CancellationToken cancellationToken)
    {
        try
        {
            if (dto.CargaHorariaSemanal > CARGA_HORARIA_SEMANA_MAXIMA_CLT)
            {
                return Result.Fail(PessoaMessages.CARGA_HORARIA_EXCEDE_LIMITE);
            }

            PESSOAS entity = new()
            {
                CARGA_HORARIA = dto.CargaHorariaSemanal,
                CARGO = dto.Cargo,
                FOTO = dto.Foto,
                NOME = dto.Nome,
            };

            var validationResult = await _validator.ValidateAsync(entity, cancellationToken);

            if (!validationResult.IsValid)
                return validationResult.ToErrorResult();

            _pessoas_repository.BeginTransaction();

            var result = await CadastrarPessoaAsync(entity, dto, cancellationToken);

            if (result.IsFailed) _pessoas_repository.Rollback();
            else _pessoas_repository.Commit();

            return result;
        }
        catch (Exception)
        {
            _pessoas_repository.Rollback();
            throw;
        }
    }

    #region Cadastrar Relacionamentos

    private async Task<Result> CadastrarContatosAsync(PessoaDto dto, int idProfessor, CancellationToken cancellationToken)
    {
        PESSOAS_CONTATOS entity = new()
        {
            ID_PESSOA = idProfessor
        };

        int rowsAffected;

        foreach (var c in dto.Contatos)
        {
            if (!await _tipos_contatos_repository.ExistsAsync("TIPOS_CONTATOS", c.IdTipo, cancellationToken))
            {
                return Result.Fail($"Não existe o tipo de contato informado.");
            }

            else if (await _pessoas_contatos_repository.HasContatoCadadastradoAsync(c.Contato, idProfessor, cancellationToken))
            {
                return Result.Fail($"Já existe o contato {c.Contato} registrado.");
            }

            entity.CONTATO = c.Contato;
            entity.ID_TIPO_CONTATO = c.IdTipo;
            rowsAffected = await _pessoas_contatos_repository.InsertAsync(entity, cancellationToken);

            if (rowsAffected == 0)
            {
                return Result.Fail($"Não foi possível cadastrar o contato {c.Contato}.");
            }
        }

        return Result.Ok();
    }

    private async Task<Result> CadastrarPessoaAsync(PESSOAS entity, PessoaDto dto, CancellationToken cancellationToken)
    {
        var affectedRows = await _pessoas_repository.InsertAsync(entity, cancellationToken);

        if (affectedRows == 0)
        {
            return Result.Fail(PessoaMessages.NAO_FOI_POSSIVEL_CADASTRAR);
        }

        var idProfessor = await _pessoas_repository.GetMaxIdAsync();

        var resultContato = await CadastrarContatosAsync(dto, idProfessor, cancellationToken);

        if (resultContato.IsFailed)
        {
            return resultContato;
        }

        var resultHabilidades = await CadastrarHabilidadesAsync(dto, idProfessor, cancellationToken);

        if (resultHabilidades.IsFailed)
        {
            return resultHabilidades;
        }

        return Result.Ok();
    }

    private async Task<Result> CadastrarHabilidadesAsync(PessoaDto dto, int idProfessor, CancellationToken cancellationToken)
    {
        PESSOAS_HABILIDADES_DISCIPLINAS entity = new()
        {
            DATA_CADASTRO = DateTime.Now,
            ID_PESSOA = idProfessor,
        };

        foreach (var i in dto.IdsDisciplinas)
        {
            if (!await _disciplinas_repository.ExistsAsync("DISCIPLINAS", i, cancellationToken))
            {
                return Result.Fail("Não existe a disciplina informada.");
            }

            if (await _pessoas_habilidades_disciplinas_repository.HasHabilidadeCadastrada(i, idProfessor, cancellationToken))
            {
                return Result.Fail(PessoaMessages.JA_TEM_HABILIDADE);
            }

            entity.ID_DISCIPLINA = i;
            var result = await _pessoas_habilidades_disciplinas_repository.InsertAsync(entity, cancellationToken);

            if (result == 0)
            {
                return Result.Fail(PessoaMessages.NAO_FOI_POSSIVEL_CADASTRAR_HABILIDADE);
            }
        }

        return Result.Ok();
    }

    #endregion
}