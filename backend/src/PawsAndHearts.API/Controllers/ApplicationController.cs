using Microsoft.AspNetCore.Mvc;
using PawsAndHearts.API.Response;

namespace PawsAndHearts.API.Controllers;

[ApiController]
[Route("[controller]")]
public abstract class ApplicationController : ControllerBase
{
    public override OkObjectResult Ok(object? value)
    {
        var envelope = Envelope.Ok(value);

        return new OkObjectResult(envelope);
    }
}