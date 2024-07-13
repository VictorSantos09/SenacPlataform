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
            .WithMessage(ProfessorMessages.CargaHorariaDeveSerInformada);
        RuleFor(x => x.NOME)
            .NotEmpty()
            .WithMessage(ProfessorMessages.NomeDeveSerInformado);
        RuleFor(x => x.CARGO)
            .NotNull()
            .WithMessage(ProfessorMessages.CargoDeveSerInformado)
            .IsInEnum()
            .WithMessage(ProfessorMessages.CargoNaoEValido);
    }
}
