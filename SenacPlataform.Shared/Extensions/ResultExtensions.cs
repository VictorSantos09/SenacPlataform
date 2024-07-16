using FluentResults;
using FluentValidation.Results;

namespace SenacPlataform.Shared.Extensions;

public static class ResultExtensions
{
    public static Result FailValidation(string message = "Dados inválidos fornecidos")
    {
        return Result.Fail(message);
    }
}

public static class ValidationResultExtensions
{
    public static IEnumerable<string> ToErrors(this ValidationResult result)
    {
        return result.Errors.Select(x => $"{x.ErrorCode}: {x.ErrorMessage} - {x.Severity}");
    }

    public static Result ToErrorResult(this ValidationResult result)
    {
        return Result.Fail(ToErrors(result));
    }
}
