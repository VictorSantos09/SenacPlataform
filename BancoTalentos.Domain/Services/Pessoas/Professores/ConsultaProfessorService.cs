using BancoTalentos.Domain.Entity;
using BancoTalentos.Domain.Entity.Enums;
using BancoTalentos.Domain.Repositories.Contracts.Interfaces;
using BancoTalentos.Domain.Services.Imagem.Dto;
using BancoTalentos.Domain.Services.Pessoa.Interfaces;
using BancoTalentos.Domain.Services.Pessoas.Base;
using BancoTalentos.Domain.Services.Pessoas.Professores.Interfaces;
using FluentResults;

namespace BancoTalentos.Domain.Services.Pessoas.Professores;

public class ConsultaProfessorService(IPESSOAS_REPOSITORY pessoas_repository, IConsultaPessoaService consultaPessoaService) : IConsultaProfessorService
{
    public async Task<Result<IEnumerable<PESSOAS>>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var result = await pessoas_repository.GetAllByCargoAsync(CARGO.PROFESSOR, cancellationToken);

        return result.Any()
            ? Result.Ok(result)
            : Result.Fail(PessoaMessages.NENHUM_ENCONTRADO);
    }

    public async Task<Result<PESSOAS>> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var result = await pessoas_repository.GetByIdAsync(id, cancellationToken);

        return result is not null
            ? Result.Ok(result)
            : Result.Fail(PessoaMessages.NAO_ENCONTRADO);
    }

    public async Task<Result<ImagemDTO>> GetFotoPerfilAsync(int id, CancellationToken cancellationToken = default)
    {
        return await consultaPessoaService.GetFotoPerfilAsync(id, "Não foi encontrado o professor.", "O professor não tem foto de perfil.", cancellationToken);
    }
}
