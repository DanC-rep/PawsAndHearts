using System.Collections;
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
        var fullPath = path + extension;
        
        return new FilePath(fullPath);
    }

    public static Result<FilePath, Error> Create(string fullPath)
    {
        return new FilePath(fullPath);
    }
}

public class FilePathList : IEnumerable<FilePath>
{
    private readonly List<FilePath> _paths;

    private FilePathList(IEnumerable<FilePath> paths)
    {
        _paths = paths.ToList();
    }

    public IEnumerator<FilePath> GetEnumerator()
    {
        return _paths.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public static implicit operator FilePathList(List<FilePath> filePaths)
        => new(filePaths);

    public static implicit operator FilePathList(FilePath filePath)
        => new([filePath]);
}