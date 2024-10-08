using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PawsAndHearts.Core.Models;
using PawsAndHearts.SharedKernel;
using PawsAndHearts.SharedKernel.Enums;

namespace PawsAndHearts.Framework.Extensions;

public static class ResponseExtensions
{
    public static ActionResult<T> ToResponse<T>(this Result<T, ErrorList> result)
    {
        if (result.IsSuccess)
            return new OkObjectResult(Envelope.Ok(result.Value));

        var distinctErrorTypes = result.Error
            .Select(x => x.Type)
            .Distinct()
            .ToList();

        var statusCode = distinctErrorTypes.Count > 1
            ? StatusCodes.Status500InternalServerError
            : GetStatusCodeForErrorType(distinctErrorTypes.First());

        var envelope = Envelope.Error(result.Error);

        return new ObjectResult(envelope)
        {
            StatusCode = statusCode
        };
    }
    
    public static ActionResult ToResponse(this UnitResult<ErrorList> result)
    {
        if (result.IsSuccess)
            return new OkResult();

        var distinctErrorTypes = result.Error
            .Select(x => x.Type)
            .Distinct()
            .ToList();

        var statusCode = distinctErrorTypes.Count > 1
            ? StatusCodes.Status500InternalServerError
            : GetStatusCodeForErrorType(distinctErrorTypes.First());

        var envelope = Envelope.Error(result.Error);

        return new ObjectResult(envelope)
        {
            StatusCode = statusCode
        };
    }

    private static int GetStatusCodeForErrorType(ErrorType errorType) =>
        errorType switch
        {
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            ErrorType.Failure => StatusCodes.Status500InternalServerError,
            _ => StatusCodes.Status500InternalServerError
        };
}