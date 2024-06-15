using BancoTalentos.Domain.Services.Contato.Dto;

namespace BancoTalentos.Domain.Services.Professores.Dto;

public sealed record ProfessorDto
{
    public string Nome { get; set; }
    public byte[]? Foto { get; set; }
    public CARGO Cargo { get; set; }
    public int CargaHoraria { get; set; }
    public IEnumerable<int> IdsDisciplinas { get; set; }
    public IEnumerable<ContatoDto> Contatos { get; set; }

    public ProfessorDto(string nome,
                        CARGO cargo,
                        int cargaHoraria,
                        IEnumerable<int> idsDisciplinas,
                        IEnumerable<ContatoDto> contatos,
                        byte[]? foto = null)
    {
        Nome = nome;
        Foto = foto;
        Cargo = cargo;
        CargaHoraria = cargaHoraria;
        IdsDisciplinas = idsDisciplinas;
        Contatos = contatos;
    }
}
