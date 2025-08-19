namespace Movie.Api.Controllers;

public sealed record MovieRequest(
    string Title,
    string Description,
    string Category);
