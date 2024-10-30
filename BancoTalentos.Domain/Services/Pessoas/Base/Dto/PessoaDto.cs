using BancoTalentos.Domain.Entity.Enums;
using BancoTalentos.Domain.Services.Contato.Dto;
using Microsoft.AspNetCore.Http;

namespace BancoTalentos.Domain.Services.Pessoas.Base.Dto;

public record PessoaDto
{
    public required string Nome { get; set; }
    public IFormFile Foto { get; set; }
    public virtual CARGO Cargo { get; init; }
    public required int CargaHorariaSemanal { get; set; }
    public IEnumerable<ContatoDto> Contatos { get; set; }
    public int Id { get; set; }
    public IEnumerable<int> IdsDisciplinas { get; set; }

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
            Nome = Nome
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
            Nome = Nome
        };
    }
}
