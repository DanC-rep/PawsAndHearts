using FluentValidation;
using PawsAndHearts.Core.Dtos;
using PawsAndHearts.Core.Validation;
using PawsAndHearts.SharedKernel;
using Constants = PawsAndHearts.SharedKernel.Constants;

namespace PawsAndHearts.PetManagement.Application.UseCases.AddPhotosToPet;

public class UploadFileDtoValidator : AbstractValidator<UploadFileDto>
{
    public UploadFileDtoValidator()
    {
        RuleFor(f => f.Content).Must(c => c.Length < Constants.MAX_FILE_SIZE)
            .WithError(Errors.Files.InvalidSize());
    }
}