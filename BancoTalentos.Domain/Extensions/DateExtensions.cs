using BancoTalentos.Domain.Enums;

namespace BancoTalentos.Domain.Extensions;

public static class DateExtensions
{
    public static StatusCadastro DataInativacaoToStatus(this DateTime? dataInativacao)
    {
        return dataInativacao is not null || dataInativacao == DateTime.MinValue ? StatusCadastro.INATIVO : StatusCadastro.ATIVO;
    }
}
