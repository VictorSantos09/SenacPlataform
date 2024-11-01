using BancoTalentos.Domain.Services.Imagem.Dto;
using BancoTalentos.Domain.Services.Pessoas.Base.Dto;
using FluentResults;
using Microsoft.AspNetCore.Http;

namespace BancoTalentos.Domain.Services.Pessoas.Professores.Interfaces;

public interface IAtualizarProfessorService
{
    Task<Result> AtualizarAsync(ProfessorDto dto, CancellationToken cancellationToken = default);
    Task<Result> AtualizarAsync(ImagemBase64DTO fotoPerfil, int id, CancellationToken cancellationToken = default);
}