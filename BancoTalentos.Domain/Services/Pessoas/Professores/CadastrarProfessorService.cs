using BancoTalentos.Domain.Entity;
using BancoTalentos.Domain.Repositories.Contracts.Interfaces;
using BancoTalentos.Domain.Services.Pessoas.Base;
using BancoTalentos.Domain.Services.Pessoas.Professores.Dto;
using BancoTalentos.Domain.Services.Pessoas.Professores.Interfaces;
using FluentResults;
using FluentValidation;

namespace BancoTalentos.Domain.Services.Pessoas.Professores;

public class CadastrarProfessorService : CadastrarPessoaServiceBase, ICadastrarProfessorService
{
    public CadastrarProfessorService(IDISCIPLINAS_REPOSITORY disciplinas_repository,
                                     IPESSOAS_CONTATOS_REPOSITORY pessoas_contatos_repository,
                                     IPESSOAS_HABILIDADES_DISCIPLINAS_REPOSITORY pessoas_habilidades_disciplinas_repository,
                                     IPESSOAS_REPOSITORY pessoas_repository,
                                     ITIPOS_CONTATOS_REPOSITORY tipos_contatos_repository,
                                     IValidator<PESSOAS> validator) : base(
        disciplinas_repository, pessoas_contatos_repository, pessoas_habilidades_disciplinas_repository,
        pessoas_repository, tipos_contatos_repository, validator)
    {
    }

    public async Task<Result> CadastrarAsync(ProfessorDto dto, CancellationToken cancellationToken)
    {
        return await CadastrarPessoaAsync(dto, cancellationToken);
    }
}
