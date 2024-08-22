using PawsAndHearts.Domain.Shared;

namespace PawsAndHearts.API.Response;

public record Envelope
{
    private Envelope(object? result, IEnumerable<ResponseError>? errors)
    {
        Result = result;
        ResponseErrors = errors?.ToList();
        TimeGenerated = DateTime.Now;
    }

    public object? Result { get; }
    
    public List<ResponseError>? ResponseErrors { get; }
    
    public DateTime TimeGenerated { get; }

    public static Envelope Ok(object? result) =>
        new Envelope(result, null);

    public static Envelope Error(IEnumerable<ResponseError> errors) =>
        new Envelope(null, errors);
}