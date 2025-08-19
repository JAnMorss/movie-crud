using MediatR;
using Movie.SharedKernel.ErrorHandling;

namespace Movie.SharedKernel.Application.Query;

public interface IQuery<TResponse> : IRequest<Result<TResponse>> { }
