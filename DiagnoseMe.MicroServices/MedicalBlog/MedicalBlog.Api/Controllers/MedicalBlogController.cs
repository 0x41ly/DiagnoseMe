using MapsterMapper;
using MediatR;
using MedicalBlog.Application.MedicalBlog.Queries.GetAnswersByQuestionId;
using MedicalBlog.Application.MedicalBlog.Queries.GetCommentsByPostId;
using MedicalBlog.Application.MedicalBlog.Queries.GetCommentsyParentId;
using MedicalBlog.Application.MedicalBlog.Queries.GetPost;
using MedicalBlog.Application.MedicalBlog.Queries.GetPosts;
using MedicalBlog.Application.MedicalBlog.Queries.GetPostsByDoctorId;
using MedicalBlog.Application.MedicalBlog.Queries.GetPostsByTags;
using MedicalBlog.Application.MedicalBlog.Queries.GetQuestion;
using MedicalBlog.Application.MedicalBlog.Queries.GetQuestions;
using MedicalBlog.Application.MedicalBlog.Queries.GetQuestionsByAskingUserId;
using MedicalBlog.Contracts.MedicalBlog;
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
    [HttpGet("posts")]
    public async Task<IActionResult> GetPosts(int pageNumber)
    {
        var query = new GetPostsQuery(pageNumber);
        var result = await _mediator.Send(query);
        return result.Match(
        result => Ok(result),
        errors => Problem(errors));
    }

    [Authorize]
    [HttpGet("posts/{postId}")]
    public async Task<IActionResult> GetPostById(string postId)
    {
        var query = new GetPostByIdQuery(postId);
        var result = await _mediator.Send(query);
        return result.Match(
        result => Ok(result),
        errors => Problem(errors));
    }

    [Authorize]
    [HttpPost("posts/{postId}/comments")]
    public async Task<IActionResult> GetCommentsByPostId(string postId, int pageNumber)
    {
        var query = new GetCommentsByPostIdQuery(postId, pageNumber);
        var result = await _mediator.Send(query);
        return result.Match(
        result => Ok(result),
        errors => Problem(errors));
    }

    [Authorize]
    [HttpGet("posts/{DoctorId}")]
    public async Task<IActionResult> GetPostsByDoctorId(string DoctorId, int pageNumber)
    {
        var query = new GetPostsByDoctorIdQuery(DoctorId, pageNumber);
        var result = await _mediator.Send(query);
        return result.Match(
        result => Ok(result),
        errors => Problem(errors));
    }
    [Authorize]
    [HttpPost("posts/tags")]
    public async Task<IActionResult> GetPostsByTags(GetPostsByTagsRequest request)
    {
        var query = _mapper.Map<GetPostsByTagsQuery>(request);
        var result = await _mediator.Send(query);
        return result.Match(
        result => Ok(result),
        errors => Problem(errors));
    }
    [Authorize]
    [HttpGet("posts/{postId}/comments/{parentId}")]
    public async Task<IActionResult> GetCommentsByParentId(string postId, string parentId, int pageNumber)
    {
        var query = new GetCommentsByParentIdQuery(parentId, pageNumber);
        var result = await _mediator.Send(query);
        return result.Match(
        result => Ok(result),
        errors => Problem(errors));
    }

    [Authorize]
    [HttpGet("questions")]
    public async Task<IActionResult> GetQuestions(int pageNumber)
    {
        var query = new GetQuestionsQuery(pageNumber);
        var result = await _mediator.Send(query);
        return result.Match(
        result => Ok(result),
        errors => Problem(errors));
    }
    [Authorize]
    [HttpGet("questions/{questionId}")]
    public async Task<IActionResult> GetQuestionById(string questionId)
    {
        var query = new GetQuestionByIdQuery(questionId);
        var result = await _mediator.Send(query);
        return result.Match(
        result => Ok(result),
        errors => Problem(errors));
    }

    [Authorize]
    [HttpGet("questions/{askingUserId}")]
    public async Task<IActionResult> GetQuestionsByAskingUserId(string askingUserId, int pageNumber)
    {
        var query = new GetQuestionsByAskingUserIdQuery(pageNumber, askingUserId);
        var result = await _mediator.Send(query);
        return result.Match(
        result => Ok(result),
        errors => Problem(errors));
    }

    [Authorize]
    [HttpGet("questions/{questionId}/answers}")]
    public async Task<IActionResult> GetAnswersByQuestionId(string questionId, int pageNumber)
    {
        var query = new GetAnswersByQuestionIdQuery(questionId, pageNumber);
        var result = await _mediator.Send(query);
        return result.Match(
        result => Ok(result),
        errors => Problem(errors));
    } 
    
}