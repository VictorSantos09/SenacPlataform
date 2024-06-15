using BancoTalentos.Domain.Entity;
using FluentValidation;

namespace BancoTalentos.Domain.Validators;

public class ProfessorValidator : AbstractValidator<PESSOAS>
{
    public ProfessorValidator()
    {
        RuleFor(x => x.CARGA_HORARIA)
            .NotEmpty()
            .WithMessage("Carga horária deve ser informada");
        RuleFor(x => x.NOME)
            .NotEmpty()
            .WithMessage("Nome deve ser informado");
        RuleFor(x => x.CARGO)
            .NotEmpty()
            .WithMessage("Cargo deve ser informado");
    }
}
