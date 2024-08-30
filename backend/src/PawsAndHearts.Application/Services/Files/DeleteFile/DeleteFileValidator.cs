using FluentValidation;
using PawsAndHearts.Application.Validators;
using PawsAndHearts.Domain.Shared;

namespace PawsAndHearts.Application.Services.Files.DeleteFile;

public class DeleteFileValidator : AbstractValidator<DeleteFileRequest>
{
    public DeleteFileValidator()
    {
        RuleFor(d => d.BucketName).NotEmpty()
            .WithError(Errors.General.ValueIsRequired("bucket name"));

        RuleFor(d => d.FileName).NotEmpty()
            .WithError(Errors.General.ValueIsRequired("file name"));
    }
}