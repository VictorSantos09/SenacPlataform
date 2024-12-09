using BancoTalentos.Domain.Entities;
using BancoTalentos.Domain.Entity.Enums;
using BancoTalentos.Domain.Services.Contato.Dto;
using BancoTalentos.Domain.Services.Imagem.Dto;

namespace BancoTalentos.Domain.Services.Pessoas.Base.Dto;

public record PessoaDto
{
    public required string Nome { get; set; }
    /// <summary>
    /// Foto de perfil em Base64
    /// </summary>
    public ImagemBase64DTO? Foto { get; set; }
    public virtual CARGO Cargo { get; init; }
    public required int CargaHorariaSemanal { get; set; }
    public IEnumerable<ContatoDto> Contatos { get; set; }
    public int Id { get; set; }
    public IEnumerable<int>? IdsDisciplinas { get; set; }
    public IEnumerable<PessoaFormacoesDto> Formacoes { get; set; }

    public PessoaDto()
    {

    }

    public ProfessorDto ToProfessor()
    {
        return new ProfessorDto()
        {
            CargaHorariaSemanal = CargaHorariaSemanal,
            Cargo = CARGO.PROFESSOR,
            Contatos = Contatos,
            Foto = Foto,
            Id = Id,
            IdsDisciplinas = IdsDisciplinas,
            Nome = Nome,
            Formacoes = Formacoes,
        };
    }

    public CoordenadorDto ToCoodenador()
    {
        return new CoordenadorDto()
        {
            CargaHorariaSemanal = CargaHorariaSemanal,
            Cargo = CARGO.COORDENADOR,
            Contatos = Contatos,
            Foto = Foto,
            Id = Id,
            IdsDisciplinas = IdsDisciplinas,
            Nome = Nome,
            Formacoes = Formacoes,
        };
    }
}
