﻿using FluentResults;

namespace BancoTalentos.Domain.Services.Professores.Interfaces;
public interface IDeletarProfessorService
{
    Task<Result> DeletarAsync(int id, CancellationToken cancellationToken = default);
}