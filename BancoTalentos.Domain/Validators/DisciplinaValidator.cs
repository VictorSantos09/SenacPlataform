using BancoTalentos.Domain.Entity;
using FluentValidation;

namespace BancoTalentos.Domain.Validators;
internal class DisciplinaValidator : AbstractValidator<DISCIPLINAS>
{
    public DisciplinaValidator()
    {
        RuleFor(x => x.NOME)
            .NotEmpty()
            .WithMessage("Nome da disciplina deve ser informado.");
        RuleFor(x => x.CARGA_HORARIA)
            .NotEmpty()
            .WithMessage("Carga horária da disciplina deve ser informada.")
            .GreaterThan(0)
            .WithMessage("Carga horária da disciplina deve ser maior que zero.")
            .LessThan(250)
            .WithMessage("Carga horária da disciplina deve ser menor que 250.");
    }
}
