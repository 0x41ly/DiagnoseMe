using MapsterMapper;
using MediatR;
using MedicalBlog.Application.MedicalBlog.Queries.GetCommentsByPostId;
using MedicalBlog.Domain.Common.Roles;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MedicalBlog.Api.Controllers;

[Route("medical-blog")]
public class MedicalBlogController : ApiController
{
    private readonly ISender _mediator;
    private readonly IMapper _mapper;
    public MedicalBlogController(
        ISender mediator, 
        IMapper mapper)
    {

        _mediator = mediator;
        _mapper = mapper;
    }
    [Authorize]
    [HttpGet("comments/{postId}")]
    public async Task<IActionResult> GetCommentsByPostId(string postId, int pageNumber)
    {
        var query = new GetCommentsByPostIdQuery(postId, pageNumber);
        var result = await _mediator.Send(query);
        return result.Match(
        result => Ok(result),
        errors => Problem(errors));
    }
    [Authorize(Roles = Roles.Admin)]
    [HttpGet("test")]
    public IActionResult Test()
    {
        return Ok("I'm Here");
    }
}