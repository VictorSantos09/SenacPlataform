using BancoTalentos.Domain.Repositories.Contracts.Interfaces;
using BancoTalentos.Domain.Repositories.Dto;
using BancoTalentos.Domain.Services.Disciplina.Dto;
using BancoTalentos.Domain.Services.Disciplina.Interfaces;
using BancoTalentos.Domain.Services.Imagem;
using FluentResults;

namespace BancoTalentos.Domain.Services.Disciplina;
internal class ConsultarDisciplinaService(IDISCIPLINAS_REPOSITORY disciplinas_repository, IImagemService imagemService) : IConsultarDisciplinaService
{
    private const string MESSAGE_NAO_ENCONTRADO = "Disciplina não encontrada.";

    public async Task<Result<IEnumerable<DisciplinaDto>>> GetAllAsync(CancellationToken cancellationToken)
    {
        var result = await disciplinas_repository.GetAllAsync(cancellationToken);

        if (result is null)
        {
            return Result.Fail("Não foi possível consultar as disciplinas.");
        }

        var disciplinas = result.Select(DisciplinaDto.Create);

        return Result.Ok(disciplinas);
    }

    public async Task<Result<DisciplinaDto>> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        var result = await disciplinas_repository.GetByIdAsync(id, cancellationToken);

        if (result is null)
        {
            return Result.Fail(MESSAGE_NAO_ENCONTRADO);
        }

        return Result.Ok(DisciplinaDto.Create(result));
    }

    public async Task<Result<DisciplinaDto>> GetByNameAsync(string name, CancellationToken cancellationToken)
    {
        var result = await disciplinas_repository.GetByNameAsync(name, cancellationToken);

        if (result is null)
        {
            return Result.Fail(MESSAGE_NAO_ENCONTRADO);
        }

        return Result.Ok(DisciplinaDto.Create(result));
    }

    public async Task<IEnumerable<DisciplinaDetalhesDTO>> GetDetalhesPessoasHabilitadasAsync(int id)
    {
        var detalhes = await disciplinas_repository.GetDetalhesPessoasHabilitadas(id);

        foreach (var detalhe in detalhes)
        {
            try
            {
                if (detalhe.CAMINHO_FOTO_PESSOA is not null)
                {
                    detalhe.CAMINHO_FOTO_PESSOA = imagemService.GetImagemOnDisk(detalhe.CAMINHO_FOTO_PESSOA).Result?.Imagem;
                }
            }
            catch (AggregateException ex)
            {
                foreach (var innerException in ex.InnerExceptions)
                {
                    if (innerException is ImageNotFoundException)
                    {
                        detalhe.CAMINHO_FOTO_PESSOA = null;
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        return detalhes;
    }
}
