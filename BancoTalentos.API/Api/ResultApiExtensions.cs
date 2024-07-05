using FluentResults;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace BancoTalentos.API.Api;

public static class ResultApiExtensions
{
    public static ActionResult ToResponse(this Result result)
    {
        if (result.IsSuccess)
        {
            return new OkResult();
        }

        return new BadRequestResult(result)
    }
}
