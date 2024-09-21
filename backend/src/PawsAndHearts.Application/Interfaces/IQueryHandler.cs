namespace PawsAndHearts.Application.Interfaces;

public interface IQueryHandler<TResponse, in TQuery> where TQuery : IQuery
{
    public Task<TResponse> Handle(TQuery command, CancellationToken cancellationToken = default);
}