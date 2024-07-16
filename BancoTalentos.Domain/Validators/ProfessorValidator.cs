using BancoTalentos.Domain.Entity;
using BancoTalentos.Domain.Services.Professores;
using FluentValidation;

namespace BancoTalentos.Domain.Validators;

public class ProfessorValidator : AbstractValidator<PESSOAS>
{
    public ProfessorValidator()
    {
        RuleFor(x => x.CARGA_HORARIA)
            .NotEmpty()
            .WithMessage(ProfessorMessages.CARGA_HORARIA_DEVE_SER_INFORMADA);
        RuleFor(x => x.NOME)
            .NotEmpty()
            .WithMessage(ProfessorMessages.NOME_DEVE_SER_INFORMADO);
        RuleFor(x => x.CARGO)
            .NotNull()
            .WithMessage(ProfessorMessages.CARGO_DEVE_SER_INFORMADO)
            .IsInEnum()
            .WithMessage(ProfessorMessages.CARGO_NAO_E_VALIDO);
    }
}
