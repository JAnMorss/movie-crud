using MediatR;
using Movie.SharedKernel.ErrorHandling;

namespace Movie.SharedKernel.Application.Command;

public interface ICommand : IRequest<Result>, IBaseCommand { }

public interface ICommand<TResponse> : IRequest<Result<TResponse>>, IBaseCommand { }

public interface IBaseCommand { }
