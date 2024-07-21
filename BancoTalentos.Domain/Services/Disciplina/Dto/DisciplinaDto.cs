using BancoTalentos.Domain.Entity;

namespace BancoTalentos.Domain.Services.Disciplina.Dto;
public sealed record DisciplinaDto
{
    public int Id { get; set; }
    public required string Nome { get; set; }
    public required int CargaHoraria { get; set; }
    public string? Descricao { get; set; }

    public static DisciplinaDto Create(DISCIPLINAS disciplinas)
    {
        return new DisciplinaDto
        {
            Id = disciplinas.ID,
            Nome = disciplinas.NOME,
            CargaHoraria = disciplinas.CARGA_HORARIA,
            Descricao = disciplinas.DESCRICAO
        };
    }
}
