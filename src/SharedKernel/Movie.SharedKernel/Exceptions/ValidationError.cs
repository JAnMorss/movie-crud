namespace Movie.SharedKernel.Exceptions;

public sealed record ValidationError(string PropertyName, string ErrorMassage);
