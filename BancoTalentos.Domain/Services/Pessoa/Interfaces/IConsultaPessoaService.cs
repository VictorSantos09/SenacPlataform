using BancoTalentos.Domain.Services.Imagem.Dto;
using BancoTalentos.Domain.Services.Pessoas.Base.Dto;
using FluentResults;

namespace BancoTalentos.Domain.Services.Pessoa.Interfaces
{
    public interface IConsultaPessoaService
    {
        Task<PessoaDto> GetById_Detalhado(int id);
        Task<Result<ImagemDTO>> GetFotoPerfilAsync(int id, string mensagemNaoEncontrado = "Não foi encontrado a pessoa.", string mensagemSemFoto = "A pessoa não tem foto de perfil.", CancellationToken cancellationToken = default);
    }
}