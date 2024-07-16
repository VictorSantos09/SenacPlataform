using BancoTalentos.Domain.Entity;
using FluentValidation;

namespace BancoTalentos.Domain.Validators;
internal class TipoContatoValidator : AbstractValidator<TIPOS_CONTATOS>
{
    public TipoContatoValidator()
    {
        RuleFor(x => x.TIPO)
            .NotEmpty().WithMessage("Tipo de contato não informado.")
            .MaximumLength(45).WithMessage("Tipo de contato deve ter no máximo 50 caracteres.");
        RuleFor(x => x.DATA_CADASTRO)
            .NotEmpty().WithMessage("Data de cadastro não informada.");
    }
}
