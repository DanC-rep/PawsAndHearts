using CSharpFunctionalExtensions;

namespace PawsAndHearts.Domain.Shared.ValueObjects;

public class FilePath
{
    private FilePath(string path)
    {
        Path = path;
    }
    
    public string Path { get; }

    public static Result<FilePath, Error> Create(Guid path, string extension)
    {
        if (!Constants.PERMITTED_FILE_EXTENSIONS.Contains(extension))
            return Errors.General.ValueIsInvalid("file extension");
        
        var fullPath = path + extension;
        
        return new FilePath(fullPath);
    }

    public static Result<FilePath, Error> Create(string fullPath)
    {
        var extension = System.IO.Path.GetExtension(fullPath);

        if (!Constants.PERMITTED_FILE_EXTENSIONS.Contains(extension))
            return Errors.Files.InvalidExtension();
        
        return new FilePath(fullPath);
    }
}