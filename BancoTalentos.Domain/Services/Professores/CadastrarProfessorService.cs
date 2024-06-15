using BancoTalentos.Domain.Entity;
using BancoTalentos.Domain.Repositories.Contracts.Interfaces;
using BancoTalentos.Domain.Services.Professores.Dto;
using BancoTalentos.Domain.Services.Professores.Interfaces;
using FluentValidation;
using LanguageExt.Common;

namespace BancoTalentos.Domain.Services.Professores;

public class CadastrarProfessorService : ICadastrarProfessorService
{
    private readonly IPESSOAS_REPOSITORY _pessoas_repository;
    private readonly IPESSOAS_CONTATOS_REPOSITORY _pessoas_contatos_repository;
    private readonly ITIPOS_CONTATOS_REPOSITORY _tipos_contatos_repository;
    private readonly IValidator<PESSOAS> _validator;
    private readonly IPESSOAS_HABILIDADES_DISCIPLINAS_REPOSITORY _pessoas_habilidades_disciplinas_repository;
    private readonly IDISCIPLINAS_REPOSITORY _disciplinas_repository;

    public CadastrarProfessorService(IPESSOAS_REPOSITORY pessoas_repository,
                                     IPESSOAS_CONTATOS_REPOSITORY pessoas_contatos_repository,
                                     ITIPOS_CONTATOS_REPOSITORY tipos_contatos_repository,
                                     IValidator<PESSOAS> validator,
                                     IPESSOAS_HABILIDADES_DISCIPLINAS_REPOSITORY pessoas_habilidades_disciplinas_repository,
                                     IDISCIPLINAS_REPOSITORY disciplinas_repository)
    {
        _pessoas_repository = pessoas_repository;
        _pessoas_contatos_repository = pessoas_contatos_repository;
        _tipos_contatos_repository = tipos_contatos_repository;
        _validator = validator;
        _pessoas_habilidades_disciplinas_repository = pessoas_habilidades_disciplinas_repository;
        _disciplinas_repository = disciplinas_repository;
    }

    public async Task<Result<bool>> CadastrarAsync(ProfessorDto dto, CancellationToken cancellationToken)
    {
        PESSOAS entity = new()
        {
            CARGA_HORARIA = dto.CargaHoraria,
            CARGO = dto.Cargo,
            FOTO = dto.Foto,
            NOME = dto.Nome,
        };

        var validationResult = await _validator.ValidateAsync(entity, cancellationToken);

        if (validationResult.IsValid)
        {
            try
            {
                var result = await CadastrarProfessorAsync(entity, dto, cancellationToken);
                return result;
            }
            catch (Exception)
            {
                _pessoas_repository.Rollback();
                throw;
            }
        }

        return new Result<bool>(false);
    }

    private async Task<Result<bool>> CadastrarProfessorAsync(PESSOAS entity, ProfessorDto dto, CancellationToken cancellationToken)
    {
        _pessoas_repository.BeginTransaction();

        var affectedRows = await _pessoas_repository.InsertAsync(entity, cancellationToken);

        if (affectedRows == 0)
        {
            _pessoas_repository.Rollback();
            return new Result<bool>(false);
        }

        var idProfessor = await _pessoas_repository.GetMaxIdAsync();

        var resultContato = await CadastrarContatosAsync(dto, idProfessor, cancellationToken);
        var resultHabilidades = await CadastrarProfessorHabilidades(dto, idProfessor, cancellationToken);


        if (resultContato.IsSuccess && resultHabilidades.IsSuccess)
        {
            _pessoas_repository.Commit();
            return new Result<bool>(true);
        }

        return new Result<bool>(false);
    }

    private async Task<Result<bool>> CadastrarContatosAsync(ProfessorDto dto, int idProfessor, CancellationToken cancellationToken)
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
                _pessoas_repository.Rollback();
                return new Result<bool>(false);
            }

            entity.CONTATO = c.Contato;
            entity.ID_TIPO_CONTATO = c.IdTipo;
            rowsAffected = await _pessoas_contatos_repository.InsertAsync(entity, cancellationToken);

            if (rowsAffected == 0)
            {
                _pessoas_repository.Rollback();
                return new Result<bool>(false);
            }
        }

        return new Result<bool>(true);
    }

    private async Task<Result<bool>> CadastrarProfessorHabilidades(ProfessorDto dto, int idProfessor, CancellationToken cancellationToken)
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
                _pessoas_repository.Rollback();
                return new Result<bool>(false);
            }

            entity.ID_DISCIPLINA = i;
            var result = await _pessoas_habilidades_disciplinas_repository.InsertAsync(entity, cancellationToken);

            if (result == 0)
            {
                _pessoas_repository.Rollback();
                return new Result<bool>(false);
            }
        }

        return new Result<bool>(true);
    }
}
