using BancoTalentos.Domain.Entity.Enums;
using BancoTalentos.Domain.Services.Pessoas.Base.Dto;
using BancoTalentos.Domain.Services.Pessoas.Coordenador.Interfaces;
using BancoTalentos.Domain.Services.Pessoas.Professores.Interfaces;
using BancoTalentos.Domain.Repositories.Contracts.Interfaces;
using FluentResults;
using BancoTalentos.Domain.Entity;
using BancoTalentos.Domain.Services.Imagem.Dto;

namespace BancoTalentos.Domain.Services.Pessoas
{
    internal class PessoaMediatorService : IPessoaMediatorService
    {
        private readonly ICadastrarCoordenadorService _cadastrarCoordenadorService;
        private readonly ICadastrarProfessorService _cadastrarProfessorService;
        private readonly IPESSOAS_REPOSITORY _pessoasRepository;

        public PessoaMediatorService(
            ICadastrarCoordenadorService cadastrarCoordenadorService,
            ICadastrarProfessorService cadastrarProfessorService,
            IPESSOAS_REPOSITORY pessoasRepository)
        {
            _cadastrarCoordenadorService = cadastrarCoordenadorService;
            _cadastrarProfessorService = cadastrarProfessorService;
            _pessoasRepository = pessoasRepository;
        }

        public async Task<Result> CadastrarAsync(PessoaDto dto, CancellationToken cancellationToken = default)
        {
            switch (dto.Cargo)
            {
                case CARGO.PROFESSOR:
                    return await _cadastrarProfessorService.CadastrarAsync(dto.ToProfessor(), cancellationToken);
                case CARGO.COORDENADOR:
                    return await _cadastrarCoordenadorService.CadastrarAsync(dto.ToCoodenador(), cancellationToken);
                default:
                    throw new ArgumentException("Cargo informado é inválido");
            }
        }

        public async Task<PessoaDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            var pessoa = await _pessoasRepository.GetByIdAsync(id, cancellationToken);
            if (pessoa == null) return null;

            return new PessoaDto
            {
                Id = pessoa.ID,
                Nome = pessoa.NOME,
                Foto = new ImagemBase64DTO { Image = pessoa.FOTO },
                Cargo = pessoa.CARGO,
                CargaHorariaSemanal = pessoa.CARGA_HORARIA
            };
        }

        public async Task<Result> AtualizarAsync(int id, PessoaDto dto, CancellationToken cancellationToken = default)
        {
            var pessoa = new PESSOAS
            {
                ID = id,
                NOME = dto.Nome,
                FOTO = dto.Foto?.Image,
                CARGO = dto.Cargo,
                CARGA_HORARIA = dto.CargaHorariaSemanal
            };

            var sucesso = await _pessoasRepository.UpdateAsync(pessoa, cancellationToken);
            if (sucesso > 0)
            {
                return Result.Ok();
            }

            return Result.Fail("Falha ao atualizar a pessoa");
        }
    }
}
