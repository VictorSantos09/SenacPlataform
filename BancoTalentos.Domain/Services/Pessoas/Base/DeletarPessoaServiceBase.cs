using BancoTalentos.Domain.Repositories.Contracts.Interfaces;
using FluentResults;

namespace BancoTalentos.Domain.Services.Pessoas.Base;

internal abstract class DeletarPessoaServiceBase(IPESSOAS_REPOSITORY pessoas_repository,
                               IPESSOAS_CONTATOS_REPOSITORY pessoas_contatos_repository,
                               IPESSOAS_HABILIDADES_DISCIPLINAS_REPOSITORY pessoas_habilidades_disciplinas_repository)
{
    public virtual async Task<Result> DeletarAsync(int id, CancellationToken cancellationToken = default)
    {
        var pessoaEncontrada = await pessoas_repository.GetByIdAsync(id, cancellationToken);

        if (pessoaEncontrada is null)
        {
            return Result.Fail($"Não foi encontrado o registro com código {id}.");
        }

        var pessoaContatosEncontrados = await pessoas_contatos_repository.GetByIdPessoaAsync(id, cancellationToken);
        var pessoaHabilidadesDisciplinasEncontradas = await pessoas_habilidades_disciplinas_repository.GetByIdPessoaAsync(id, cancellationToken);

        try
        {
            pessoas_repository.BeginTransaction();

            int resultadoPessoaContato;
            foreach (var contato in pessoaContatosEncontrados)
            {
                resultadoPessoaContato = await pessoas_contatos_repository.DeleteAsync(contato, cancellationToken);

                if (resultadoPessoaContato == 0)
                {
                    pessoas_repository.Rollback();
                    return Result.Fail($"Erro ao deletar o contato {contato.CONTATO} da pessoa {pessoaEncontrada.NOME}.");
                }
            }

            int resultadoPessoaHabilidadeDisciplina;
            foreach (var habilidadeDisciplina in pessoaHabilidadesDisciplinasEncontradas)
            {
                resultadoPessoaHabilidadeDisciplina = await pessoas_habilidades_disciplinas_repository.DeleteAsync(habilidadeDisciplina, cancellationToken);

                if (resultadoPessoaHabilidadeDisciplina == 0)
                {
                    pessoas_repository.Rollback();
                    return Result.Fail($"Erro ao deletar a habilidade {habilidadeDisciplina.ID_DISCIPLINA} da pessoa {pessoaEncontrada.NOME}.");
                }
            }

            var resultadoPessoa = await pessoas_repository.DeleteAsync(pessoaEncontrada, cancellationToken);

            if (resultadoPessoa == 0)
            {
                pessoas_repository.Rollback();
                return Result.Fail($"Erro ao deletar o registro da pessoa {pessoaEncontrada.NOME}.");
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
}
