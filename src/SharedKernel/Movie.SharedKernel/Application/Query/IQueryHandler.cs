using MediatR;
using Movie.SharedKernel.ErrorHandling;

namespace Movie.SharedKernel.Application.Query;

public interface IQueryHandler<TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>>
    where TQuery : IQuery<TResponse>
{
}
