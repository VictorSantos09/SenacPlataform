using BancoTalentos.Domain.Services.Imagem.Dto;
using FluentResults;

namespace BancoTalentos.Domain.Services.Pessoa.Interfaces
{
    public interface IConsultaPessoaService
    {
        Task<Result<ImagemDTO>> GetFotoPerfilAsync(int id, string mensagemNaoEncontrado = "Não foi encontrado a pessoa.", string mensagemSemFoto = "A pessoa não tem foto de perfil.", CancellationToken cancellationToken = default);
    }
}