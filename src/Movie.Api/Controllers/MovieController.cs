using MediatR;
using Microsoft.AspNetCore.Mvc;
using Movie.Application.Commands.Movies.CreateMovies;
using Movie.Application.Commands.Movies.DeleteMovies;
using Movie.Application.Commands.Movies.UpdateMovies;
using Movie.Application.Queries.Movies.GetMovies;
using Movie.Application.Queries.Movies.GetMoviesById;

namespace Movie.Api.Controllers;

[ApiController]
[Route("api/movie")]
public class MovieController : ControllerBase
{
    private readonly ISender _sender;

    public MovieController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet]
    public async Task<IActionResult> GetMovies(CancellationToken cancellationToken)
    {
        var query = new GetMoviesQuery();

        var result = await _sender.Send(query, cancellationToken);

        if (result.IsFailure)
            return NotFound(result.Error);

        return Ok(result.Value);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(
        [FromRoute] Guid id,
        CancellationToken cancellationToken)
    {
        var query = new GetMovieByIdQuery(id);

        var result = await _sender.Send(query, cancellationToken);

        return result.IsSuccess
            ? Ok(result.Value)
            : NotFound(result.Error);
    }

    [HttpPost]
    public async Task<IActionResult> CreateMovies(
        [FromBody] CreateMoviesCommand command,
        CancellationToken cancellationToken)
    {
        var result = await _sender.Send(command, cancellationToken);

        if (result.IsFailure)
            return BadRequest(result.Error);

        return CreatedAtAction(nameof(GetById), new { id = result.Value }, result.Value);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateMovies(
        [FromRoute] Guid id,
        [FromBody] MovieRequest request,
        CancellationToken cancellationToken)
    {
        var command = new UpdateMoviesCommand(id, request.Title, request.Description, request.Category);

        var result = await _sender.Send(command, cancellationToken);

        return result.IsSuccess
            ? Ok("Successfully updated")
            : NotFound(result.Error);
    }


    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteMovies(
        [FromRoute] Guid id,
        CancellationToken cancellationToken)
    {
        var command = new DeleteMoviesCommand(id);

        var result = await _sender.Send(command, cancellationToken);

        return result.IsSuccess
            ? Ok("Successfully deleted")
            : NotFound(result.Error);
    }
}
