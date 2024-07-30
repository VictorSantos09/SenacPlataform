using BancoTalentos.Domain.Services.Pessoas.Base.Dto;
using FluentResults;
using Microsoft.AspNetCore.Http;

namespace BancoTalentos.Domain.Services.Pessoas.Coordenador.Interfaces;

public interface IAtualizarCoordenadorService
{
    Task<Result> AtualizarAsync(IFormFile fotoPerfil, int id, CancellationToken cancellationToken = default);
    Task<Result> AtualizarAsync(CoordenadorDto dto, CancellationToken cancellationToken = default);
}