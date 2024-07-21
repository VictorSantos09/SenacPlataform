using BancoTalentos.Domain.Repositories.Contracts.Interfaces;
using BancoTalentos.Domain.Services.Pessoas.Professores.Interfaces;
using FluentResults;

namespace BancoTalentos.Domain.Services.Pessoas.Professores;

internal class DeletarProfessorService : IDeletarProfessorService
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
            return Result.Fail($"Não foi encontrado o professor com código {id}.");
        }

        var professorContatosEncontrados = await _pessoas_contatos_repository.GetByIdPessoaAsync(id, cancellationToken);
        var professorHabilidadesDisciplinasEncontradas = await _pessoas_habilidades_disciplinas_repository.GetByIdPessoaAsync(id, cancellationToken);

        try
        {
            _pessoas_repository.BeginTransaction();

            int resultadoPessoaContato;
            foreach (var contato in professorContatosEncontrados)
            {
                resultadoPessoaContato = await _pessoas_contatos_repository.DeleteAsync(contato, cancellationToken);

                if (resultadoPessoaContato == 0)
                {
                    _pessoas_repository.Rollback();
                    return Result.Fail($"Erro ao deletar o contato {contato.CONTATO} do professor {professorEncontrado.NOME}.");
                }
            }

            int resultadoPessoaHabilidadeDisciplina;
            foreach (var habilidadeDisciplina in professorHabilidadesDisciplinasEncontradas)
            {
                resultadoPessoaHabilidadeDisciplina = await _pessoas_habilidades_disciplinas_repository.DeleteAsync(habilidadeDisciplina, cancellationToken);

                if (resultadoPessoaHabilidadeDisciplina == 0)
                {
                    _pessoas_repository.Rollback();
                    return Result.Fail($"Erro ao deletar a habilidade {habilidadeDisciplina.ID_DISCIPLINA} do professor {professorEncontrado.NOME}.");
                }
            }

            var resultadoPessoa = await _pessoas_repository.DeleteAsync(professorEncontrado, cancellationToken);

            if (resultadoPessoa == 0)
            {
                _pessoas_repository.Rollback();
                return Result.Fail($"Erro ao deletar o professor {professorEncontrado.NOME}.");
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
}
