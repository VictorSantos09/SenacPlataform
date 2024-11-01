using BancoTalentos.Domain.Services.Imagem.Dto;
using BancoTalentos.Domain.Services.Pessoas.Base.Dto;
using FluentResults;
using Microsoft.AspNetCore.Http;

namespace BancoTalentos.Domain.Services.Pessoas.Coordenador.Interfaces;

public interface IAtualizarCoordenadorService
{
    Task<Result> AtualizarFotoPerfilAsync(ImagemBase64DTO fotoPerfil, int id, CancellationToken cancellationToken = default);
    Task<Result> AtualizarCoordenadorAsync(CoordenadorDto dto, CancellationToken cancellationToken = default);
}