using BancoTalentos.Domain.Entity;
using BancoTalentos.Domain.Services.Pessoas.Professores;
using FluentValidation;

namespace BancoTalentos.Domain.Validators;

internal class ProfessorValidator : AbstractValidator<PESSOAS>
{
    public ProfessorValidator()
    {
        RuleFor(x => x.CARGA_HORARIA)
            .NotEmpty()
            .WithMessage(PessoaMessages.CARGA_HORARIA_DEVE_SER_INFORMADA);
        RuleFor(x => x.NOME)
            .NotEmpty()
            .WithMessage(PessoaMessages.NOME_DEVE_SER_INFORMADO);
        RuleFor(x => x.CARGO)
            .NotNull()
            .WithMessage(PessoaMessages.CARGO_DEVE_SER_INFORMADO)
            .IsInEnum()
            .WithMessage(PessoaMessages.CARGO_NAO_E_VALIDO);
    }
}
