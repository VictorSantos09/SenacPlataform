using BancoTalentos.Domain.Services.Pessoas.Professores.Dto;
using FluentResults;

namespace BancoTalentos.Domain.Services.Pessoas.Coordenador.Interfaces
{
    public interface ICadastrarCoordenadorService
    {
        Task<Result> CadastrarAsync(CoordenadorDto dto, CancellationToken cancellationToken);
    }
}