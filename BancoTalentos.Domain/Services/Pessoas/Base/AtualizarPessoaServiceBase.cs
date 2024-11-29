using BancoTalentos.Domain.Entity;
using BancoTalentos.Domain.Repositories.Contracts.Interfaces;
using BancoTalentos.Domain.Services.Imagem;
using BancoTalentos.Domain.Services.Imagem.Dto;
using BancoTalentos.Domain.Services.Pessoas.Base.Dto;
using FluentResults;
using SenacPlataform.Shared.Exceptions.ImagemConfig;

namespace BancoTalentos.Domain.Services.Pessoas.Base;

internal abstract class AtualizarPessoaServiceBase(IPESSOAS_REPOSITORY pessoas_repository,
                                 IPESSOAS_CONTATOS_REPOSITORY pessoas_contatos_repository,
                                 IPESSOAS_HABILIDADES_DISCIPLINAS_REPOSITORY pessoas_habilidades_disciplinas_repository,
                                 IImagemService imagemService)
{
    public async Task<Result> AtualizarPessoaAsync(PessoaDto dto, CancellationToken cancellationToken = default)
    {
        if (dto.Id == 0)
        {
            return Result.Fail("O código da pessoa não foi informado.");
        }

        var pessoaEncontrada = await pessoas_repository.GetByIdAsync(dto.Id, cancellationToken);

        if (pessoaEncontrada is null)
        {
            return Result.Fail($"A pessoa com o código {dto.Id} não foi encontrado.");
        }

        try
        {
            pessoas_repository.BeginTransaction();

            var resultadoContato = await AtualizarContatosAsync(dto, pessoaEncontrada.ID, cancellationToken);

            if (resultadoContato.IsFailed)
            {
                pessoas_repository.Rollback();
                return resultadoContato;
            }

            await AtualizarDisciplinasAsync(dto, pessoaEncontrada.ID, cancellationToken);

            bool fotoAtualizada = false;
            var nomeFotoAntiga = pessoaEncontrada.FOTO;

            if (dto.Foto is not null)
            {
                var resultadoFoto = await AtualizarFotoPerfilAsync(dto.Foto, cancellationToken);
                if (resultadoFoto.IsSuccess)
                {
                    pessoaEncontrada.FOTO = resultadoFoto.Value;
                    fotoAtualizada = true;
                }
            }

            AtualizarDadosRestantesProfessor(dto, pessoaEncontrada);
            var resultadoPessoa = await pessoas_repository.UpdateAsync(pessoaEncontrada, cancellationToken);

            if (resultadoPessoa == 0)
            {
                pessoas_repository.Rollback();

                if (pessoaEncontrada.FOTO is null)
                {
                    throw new ImagemErroAtualizarException("Ocorreu um erro ao reverter para a foto de perfil anterior ao atualizar a pessoa.",
                                                                                nomeFotoAntiga,
                                                                                pessoaEncontrada.FOTO);
                }

                imagemService.DeletarImagemOnDisk(pessoaEncontrada.FOTO);
                return Result.Fail(PessoaMessages.NAO_FOI_POSSIVEL_ATUALIZAR);
            }

            if (fotoAtualizada && nomeFotoAntiga is not null)
            {
                imagemService.DeletarImagemOnDisk(nomeFotoAntiga);
            }

            pessoas_repository.Commit();

            return Result.Ok();
        }
        catch (Exception)
        {
            pessoas_repository.Rollback();
            throw;
        }
    }
    #region ATUALIZAR RELACIONAMENTOS

    public async Task<Result> AtualizarPessoaFotoPerfilAsync(ImagemBase64DTO fotoPerfil, int id, CancellationToken cancellationToken = default)
    {
        var professorExiste = await pessoas_repository.ExistsAsync("PESSOAS", id, cancellationToken);

        if (!professorExiste)
        {
            return Result.Fail($"O professor com o código {id} não foi encontrado.");
        }

        await imagemService.ArmazenarFotoPerfilOnDiskAsync(fotoPerfil, cancellationToken);

        return Result.Ok();
    }

    private async Task<Result> AtualizarContatosAsync(PessoaDto dto, int idPessoa, CancellationToken cancellationToken)
    {
        if (dto.Contatos is null || !dto.Contatos.Any())
        {
            await pessoas_contatos_repository.DeleteByIdPessoa(idPessoa);
            return Result.Ok();
        }

        var contatosPessoa = await pessoas_contatos_repository.GetByIdPessoaAsync(idPessoa, cancellationToken);
        
        var idsExistentes = contatosPessoa.Select(c => c.ID).ToHashSet();
        var idsNoDto = ExtrairRegistroParaComparar(dto);
        var contatosParaAdicionar = ExtrairContatosParaAdicionar(dto, idsExistentes);
        var idsParaRemover = ExtrairContatosParaRemover(idsExistentes, idsNoDto);

        await GravarNovosContatos(idPessoa, contatosParaAdicionar, cancellationToken);
        await RemoverContatosAntigos(idsParaRemover);

        return Result.Ok();
    }

    private async Task RemoverContatosAntigos(List<int> idsParaRemover)
    {
        foreach (var idContato in idsParaRemover)
        {
            await pessoas_contatos_repository.DeleteById(idContato);
        }
    }

    private async Task GravarNovosContatos(int idPessoa, List<Contato.Dto.ContatoDto> contatosParaAdicionar, CancellationToken cancellationToken)
    {
        foreach (var contato in contatosParaAdicionar)
        {
            var novoContato = new PESSOAS_CONTATOS
            {
                ID_PESSOA = idPessoa,
                ID_TIPO_CONTATO = contato.IdTipo,
                CONTATO = contato.Contato,
            };

            await pessoas_contatos_repository.InsertAsync(novoContato, cancellationToken);
        }
    }

    private static HashSet<int> ExtrairRegistroParaComparar(PessoaDto dto)
    {
        return dto.Contatos.Select(c => c.Id).ToHashSet();
    }

    private static List<Contato.Dto.ContatoDto> ExtrairContatosParaAdicionar(PessoaDto dto, HashSet<int> idsExistentes)
    {

        // Contatos para adicionar
        return dto.Contatos
            .Where(c => !idsExistentes.Contains(c.Id))
            .ToList();
    }

    private static List<int> ExtrairContatosParaRemover(HashSet<int> idsExistentes, HashSet<int> idsNoDto)
    {
        var idsParaRemover = idsExistentes
                    .Where(id => !idsNoDto.Contains(id)) // Apenas IDs que não estão no DTO
                    .ToList();
        return idsParaRemover;
    }


    private async Task AtualizarDisciplinasAsync(PessoaDto dto, int idPessoa, CancellationToken cancellationToken)
    {
        if (dto.IdsDisciplinas is null || !dto.IdsDisciplinas.Any())
        {
            await pessoas_habilidades_disciplinas_repository.DeletarHabilidadesPessoa(idPessoa);
            return;
        }

        var habilidadesPessoa = await pessoas_habilidades_disciplinas_repository.GetByIdPessoaAsync(idPessoa, cancellationToken);
        var idsExistentes = habilidadesPessoa.Select(h => h.ID_DISCIPLINA).ToHashSet();

        var idsParaAdicionar = dto.IdsDisciplinas.Except(idsExistentes);
        var idsParaRemover = idsExistentes.Except(dto.IdsDisciplinas);

        foreach (var idDisciplina in idsParaAdicionar)
        {
            var novaHabilidade = new PESSOAS_HABILIDADES_DISCIPLINAS
            {
                ID_PESSOA = idPessoa,
                ID_DISCIPLINA = idDisciplina,
                DATA_CADASTRO = DateTime.Now
            };

            await pessoas_habilidades_disciplinas_repository.InsertAsync(novaHabilidade, cancellationToken);
        }

        foreach (var idDisciplina in idsParaRemover)
        {
            await pessoas_habilidades_disciplinas_repository.DeleteBy_IDX_PESSOAS_HABILIDADES_DISCIPLINAS_002(idPessoa, idDisciplina);
        }
    }

    private static void AtualizarDadosRestantesProfessor(PessoaDto dto, PESSOAS professorEncontrado)
    {
        professorEncontrado.CARGA_HORARIA = dto.CargaHorariaSemanal;
        professorEncontrado.CARGO = dto.Cargo;
        professorEncontrado.NOME = dto.Nome;
    }
    #endregion

    private async Task<Result<string>> AtualizarFotoPerfilAsync(ImagemBase64DTO novaFoto, CancellationToken cancellationToken = default)
    {
        if (novaFoto is null)
        {
            throw new ImagemNaoInformadaException("A foto de perfil para ser atualizada deve ser informada.");
        }

        var resultadoGravacaoNovaImagem = await imagemService.ArmazenarFotoPerfilOnDiskAsync(novaFoto, cancellationToken);

        if (resultadoGravacaoNovaImagem.IsSuccess)
        {
            return Result.Ok(resultadoGravacaoNovaImagem.Value);
        }

        return Result.Fail("Não foi possível armazenar a nova imagem. Portanto não será alterada.");
    }
}
