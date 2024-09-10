using System.Collections;

namespace PawsAndHearts.Domain.Shared.ValueObjects;

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
        => new (filePaths);

    public static implicit operator FilePathList(FilePath filePath)
        => new ([filePath]);
}