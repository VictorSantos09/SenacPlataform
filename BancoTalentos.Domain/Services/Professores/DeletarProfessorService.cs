using BancoTalentos.Domain.Repositories.Contracts.Interfaces;
using FluentResults;

namespace BancoTalentos.Domain.Services.Professores;

internal class DeletarProfessorService
{
    private readonly IPESSOAS_REPOSITORY _pessoas_repository;
    private readonly IPESSOAS_CONTATOS_REPOSITORY _pessoas_contatos_repository;
    private readonly IPESSOAS_HABILIDADES_DISCIPLINAS_REPOSITORY _pessoas_habilidades_disciplinas_repository;

    public DeletarProfessorService(IPESSOAS_REPOSITORY pessoas_repository,
                                   IPESSOAS_CONTATOS_REPOSITORY pessoas_contatos_repository,
                                   IPESSOAS_HABILIDADES_DISCIPLINAS_REPOSITORY pessoas_habilidades_disciplinas_repository)
    {
        _pessoas_repository = pessoas_repository;
        _pessoas_contatos_repository = pessoas_contatos_repository;
        _pessoas_habilidades_disciplinas_repository = pessoas_habilidades_disciplinas_repository;
    }

    public async Task<Result> DeletarAsync(int id, CancellationToken cancellationToken = default)
    {
        var professorEncontrado = await _pessoas_repository.GetByIdAsync(id, cancellationToken);

        if (professorEncontrado is null)
        {
            return Result.Fail($"Não foi encontrado o professor com código {id}");
        }

        try
        {
            _pessoas_repository.BeginTransaction();


            foreach (var item in collection)
            {

            }

            _pessoas_repository.Commit();
        }
        catch (Exception)
        {
            _pessoas_repository.Rollback();
            throw;
        }
    }
}
