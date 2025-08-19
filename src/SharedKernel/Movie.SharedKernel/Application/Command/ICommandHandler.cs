﻿using MediatR;
using Movie.SharedKernel.ErrorHandling;

namespace Movie.SharedKernel.Application.Command;

public interface ICommandHandler<TCommand> : IRequestHandler<TCommand, Result>
    where TCommand : ICommand
{
}

public interface ICommandHandler<TCommand, TResponse> : IRequestHandler<TCommand, Result<TResponse>>
    where TCommand : ICommand<TResponse>
{

}
