using BancoTalentos.Domain.Entity;
using BancoTalentos.Domain.Repositories.Contracts.Interfaces;
using BancoTalentos.Domain.Services.Pessoas.Base;
using BancoTalentos.Domain.Services.Pessoas.Base.Dto;
using BancoTalentos.Domain.Services.Pessoas.Coordenador.Interfaces;
using FluentResults;
using FluentValidation;

namespace BancoTalentos.Domain.Services.Pessoas.Coordenador;

internal class CadastrarCoordenadorService(IDISCIPLINAS_REPOSITORY disciplinas_repository,
                              IPESSOAS_CONTATOS_REPOSITORY pessoas_contatos_repository,
                              IPESSOAS_HABILIDADES_DISCIPLINAS_REPOSITORY pessoas_habilidades_disciplinas_repository,
                              IPESSOAS_REPOSITORY pessoas_repository,
                              ITIPOS_CONTATOS_REPOSITORY tipos_contatos_repository,
                              IValidator<PESSOAS> validator) : CadastrarPessoaServiceBase(disciplinas_repository,
                                                                    pessoas_contatos_repository,
                                                                    pessoas_habilidades_disciplinas_repository,
                                                                    pessoas_repository,
                                                                    tipos_contatos_repository,
                                                                    validator), ICadastrarCoordenadorService
{
    public async Task<Result> CadastrarAsync(CoordenadorDto dto, CancellationToken cancellationToken)
    {
        return await CadastrarPessoaAsync(dto, cancellationToken);
    }
}
