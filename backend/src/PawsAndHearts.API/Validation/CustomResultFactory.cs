using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PawsAndHearts.API.Response;
using PawsAndHearts.Domain.Shared;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Results;

namespace PawsAndHearts.API.Validation;

public class CustomResultFactory : IFluentValidationAutoValidationResultFactory
{
    public IActionResult CreateActionResult(
        ActionExecutingContext context, 
        ValidationProblemDetails? validationProblemDetails)
    {
        if (validationProblemDetails is null)
            throw new InvalidOperationException("ValidationProblemDetails in null");

        List<ResponseError> errors = [];

        foreach (var (invalidField, validationErrors) in validationProblemDetails.Errors)
        {
            var responseErrors = 
                from errorMessage in validationErrors 
                let error = Error.Deserialize(errorMessage) 
                select new ResponseError(error.Code, error.Message, invalidField);
            
            errors.AddRange(responseErrors);
        } 
        
        var envelope = Envelope.Error(errors);

        return new ObjectResult(envelope)
        {
            StatusCode = StatusCodes.Status400BadRequest
        };
    }
}