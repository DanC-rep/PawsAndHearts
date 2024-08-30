using Microsoft.AspNetCore.Mvc;
using Minio;
using PawsAndHearts.API.Extensions;
using PawsAndHearts.Application.Services.Files.DeleteFile;
using PawsAndHearts.Application.Services.Files.GetFile;
using PawsAndHearts.Application.Services.Files.UploadFile;

namespace PawsAndHearts.API.Controllers;

public class FileController : ApplicationController
{
    [HttpGet]
    public async Task<ActionResult<string>> GetFile(
        [FromQuery] GetFileRequest request,
        [FromServices] GetFileHandler handler,
        CancellationToken cancellationToken = default)
    {
        var result = await handler.Handle(request, cancellationToken);

        return result.ToResponse();
    }

    [HttpPost]
    public async Task<ActionResult<string>> CreateFile(
        IFormFile file, 
        [FromServices] UploadFileHandler handler,
        CancellationToken cancellationToken = default)
    {
        await using var stream = file.OpenReadStream();

        var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
        
        var request = new UploadFileRequest(stream, "photos", fileName);

        var result = await handler.Handle(request, cancellationToken);

        return result.ToResponse();
    }

    [HttpDelete]
    public async Task<ActionResult<string>> DeleteFile(
        [FromBody] DeleteFileRequest request,
        [FromServices] DeleteFileHandler handler,
        CancellationToken cancellationToken = default)
    {
        var result = await handler.Handle(request, cancellationToken);

        return result.ToResponse();
    }
}