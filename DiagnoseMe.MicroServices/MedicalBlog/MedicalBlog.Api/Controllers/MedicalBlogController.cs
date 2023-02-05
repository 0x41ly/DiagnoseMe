using MapsterMapper;
using MediatR;
using MedicalBlog.Application.MedicalBlog.Commands.AddAnswer;
using MedicalBlog.Application.MedicalBlog.Commands.AddComment;
using MedicalBlog.Application.MedicalBlog.Commands.AgreeComment;
using MedicalBlog.Application.MedicalBlog.Commands.Ask;
using MedicalBlog.Application.MedicalBlog.Commands.CreatePost;
using MedicalBlog.Application.MedicalBlog.Commands.DeleteAnswer;
using MedicalBlog.Application.MedicalBlog.Commands.DeleteComment;
using MedicalBlog.Application.MedicalBlog.Commands.DeleteCommentAgreement;
using MedicalBlog.Application.MedicalBlog.Commands.DeletePost;
using MedicalBlog.Application.MedicalBlog.Commands.DeleteQuestion;
using MedicalBlog.Application.MedicalBlog.Commands.EditAnswer;
using MedicalBlog.Application.MedicalBlog.Commands.EditComment;
using MedicalBlog.Application.MedicalBlog.Commands.EditPost;
using MedicalBlog.Application.MedicalBlog.Commands.EditQuestion;
using MedicalBlog.Application.MedicalBlog.Commands.RatePost;
using MedicalBlog.Application.MedicalBlog.Commands.ReplyComment;
using MedicalBlog.Application.MedicalBlog.Commands.SubscribeDoctor;
using MedicalBlog.Application.MedicalBlog.Commands.UnsubscribeDoctor;
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
using MedicalBlog.Domain.Common.Roles;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MedicalBlog.Api.Controllers;

[Route("api")]
public class MedicalBlogController : ApiController
{
    private readonly ISender _mediator;
    private readonly IMapper _mapper;
    public MedicalBlogController(
        ISender mediator, 
        IMapper mapper,
        Serilog.ILogger logger) : base(logger)
    {

        _mediator = mediator;
        _mapper = mapper;
    }
    
    [Authorize]
    [HttpGet("posts/page-number/{pageNumber}")]
    public async Task<IActionResult> GetPosts(int pageNumber)
    {
        var query = new GetPostsQuery(
            pageNumber,
            GetUserIdFromToken());
        var result = await _mediator.Send(query);
        return result.Match(
        result => Ok(result),
        errors => Problem(errors));
    }

    [Authorize]
    [HttpGet("posts/post-id/{postId}")]
    public async Task<IActionResult> GetPostById(string postId)
    {
        var query = new GetPostByIdQuery(
            postId,
            GetUserIdFromToken());
        var result = await _mediator.Send(query);
        return result.Match(
        result => Ok(result),
        errors => Problem(errors));
    }

    [Authorize]
    [HttpPost("posts/post-id/{postId}/comments/page-number/{pageNumber}")]
    public async Task<IActionResult> GetCommentsByPostId(string postId, int pageNumber)
    {
        var query = new GetCommentsByPostIdQuery(postId, pageNumber);
        var result = await _mediator.Send(query);
        return result.Match(
        result => Ok(result),
        errors => Problem(errors));
    }

    [Authorize]
    [HttpGet("posts/doctor-id/{DoctorId}/page-number/{pageNumber}")]
    public async Task<IActionResult> GetPostsByDoctorId(string DoctorId, int pageNumber)
    {
        var query = new GetPostsByDoctorIdQuery(
            DoctorId, 
            pageNumber,
            GetUserIdFromToken());
        var result = await _mediator.Send(query);
        return result.Match(
        result => Ok(result),
        errors => Problem(errors));
    }
    [Authorize]
    [HttpPost("posts/tags")]
    public async Task<IActionResult> GetPostsByTags(GetPostsByTagsRequest request)
    {
        var query = new GetPostsByTagsQuery(
            request.PageNumber,
            request.Tags,
            GetUserIdFromToken());
        var result = await _mediator.Send(query);
        return result.Match(
        result => Ok(result),
        errors => Problem(errors));
    }
    [Authorize]
    [HttpGet("posts/post-id/{postId}/comments/parent-id/{parentId}/page-number/{pageNumber}")]
    public async Task<IActionResult> GetCommentsByParentId(string postId, string parentId, int pageNumber)
    {
        var query = new GetCommentsByParentIdQuery(parentId, pageNumber);
        var result = await _mediator.Send(query);
        return result.Match(
        result => Ok(result),
        errors => Problem(errors));
    }

    [Authorize]
    [HttpGet("questions/page-number/{pageNumber}")]
    public async Task<IActionResult> GetQuestions(int pageNumber)
    {
        var query = new GetQuestionsQuery(pageNumber);
        var result = await _mediator.Send(query);
        return result.Match(
        result => Ok(result),
        errors => Problem(errors));
    }
    [Authorize]
    [HttpGet("questions/question-id/{questionId}")]
    public async Task<IActionResult> GetQuestionById(string questionId)
    {
        var query = new GetQuestionByIdQuery(questionId);
        var result = await _mediator.Send(query);
        return result.Match(
        result => Ok(result),
        errors => Problem(errors));
    }

    [Authorize]
    [HttpGet("questions/asking-user-id/{askingUserId}/page-number/{pageNumber}")]
    public async Task<IActionResult> GetQuestionsByAskingUserId(string askingUserId, int pageNumber)
    {
        var query = new GetQuestionsByAskingUserIdQuery(pageNumber, askingUserId);
        var result = await _mediator.Send(query);
        return result.Match(
        result => Ok(result),
        errors => Problem(errors));
    }

    [Authorize]
    [HttpGet("questions/question-id/{questionId}/answers/page-number/{pageNumber}")]
    public async Task<IActionResult> GetAnswersByQuestionId(string questionId, int pageNumber)
    {
        var query = new GetAnswersByQuestionIdQuery(questionId, pageNumber);
        var result = await _mediator.Send(query);
        return result.Match(
        result => Ok(result),
        errors => Problem(errors));
    } 

    [Authorize(Roles = Roles.Doctor)]
    [HttpPost("posts/create")]
    public async Task<IActionResult> CreatePost(CreatePostRequest request)
    {
        var command = new CreatePostCommand(
            GetUserIdFromToken(),
            request.Title,
            request.Content,
            request.Tags);
        var result = await _mediator.Send(command);
        return result.Match(
        result => Ok(result),
        errors => Problem(errors));
    }
    [Authorize(Roles = Roles.Doctor)]
    [HttpPost("posts/post-id/{postId}/edit")]
    public async Task<IActionResult> EditPost(EditPostRequest request, string postId)
    {
        var command = new EditPostCommand(
            postId,
            request.Title,
            request.Content,
            request.Tags,
            GetUserIdFromToken());
        var result = await _mediator.Send(command);
        return result.Match(
        result => Ok(result),
        errors => Problem(errors));
    }
    [Authorize(Roles = Roles.Doctor + "," + Roles.Admin)]
    [HttpDelete("posts/post-id/{postId}/delete")]
    public async Task<IActionResult> DeletePost(string postId)
    {
        var command = new DeletePostCommand(
            postId,
            GetUserIdFromToken(),
            GetUserRolesFromToken());
        var result = await _mediator.Send(command);
        return result.Match(
        result => Ok(result),
        errors => Problem(errors));
    }

    [Authorize(Roles = Roles.Doctor)]
    [HttpPost("posts/post-id/{postId}/comments/Add")]
    public async Task<IActionResult> AddComment(AddCommentRequest request, string postId)
    {
        var command = new AddCommentCommand(
            request.Content,
            GetUserIdFromToken(),
            postId);
        var result = await _mediator.Send(command);
        return result.Match(
        result => Ok(result),
        errors => Problem(errors));
    }
    [Authorize(Roles = Roles.Doctor)]
    [HttpPost("posts/comments/comment-id/{commentId}/edit")]
    public async Task<IActionResult> EditComment(EditCommentRequest request, string commentId)
    {
        var command = new EditCommentCommand(
            commentId,
            request.Content,
            GetUserIdFromToken());
        var result = await _mediator.Send(command);
        return result.Match(
        result => Ok(result),
        errors => Problem(errors));
    }
    [Authorize(Roles = Roles.Doctor + "," + Roles.Admin)]
    [HttpDelete("posts/comments/comment-id/{commentId}/delete")]
    public async Task<IActionResult> DeleteComment(string commentId)
    {
        var command = new DeleteCommentCommand(
            commentId,
            GetUserIdFromToken(),
            GetUserRolesFromToken());
        var result = await _mediator.Send(command);
        return result.Match(
        result => Ok(result),
        errors => Problem(errors));
    }
    [Authorize(Roles = Roles.Doctor)]
    [HttpDelete("posts/post-id/{postId}/comments/comment-id/{commentId}/reply")]
    public async Task<IActionResult> ReplyToComment(ReplyToCommentRequest request, string commentId, string postId)
    {
        var command = new ReplyToCommentCommand(
            postId,
            commentId,
            GetUserIdFromToken(),
            request.Content);
        var result = await _mediator.Send(command);
        return result.Match(
        result => Ok(result),
        errors => Problem(errors));
    }

    [Authorize]
    [HttpPost("questions/ask")]
    public async Task<IActionResult> AskQuestion(AskRequest request)
    {
        var command = new AskCommand(
            GetUserIdFromToken(),
            request.QuestionString);
        var result = await _mediator.Send(command);
        return result.Match(
        result => Ok(result),
        errors => Problem(errors));
    }
    [Authorize]
    [HttpPost("questions/question-id/{questionId}/edit")]
    public async Task<IActionResult> EditQuestion(EditQuestionRequest request, string questionId)
    {
        var command = new EditQuestionCommand(
            questionId,
            request.QuestionString,
            GetUserIdFromToken());
        var result = await _mediator.Send(command);
        return result.Match(
        result => Ok(result),
        errors => Problem(errors));
    }
    [Authorize]
    [HttpDelete("questions/question-id/{questionId}/delete")]
    public async Task<IActionResult> DeleteQuestion(string questionId)
    {
        var command = new DeleteQuestionCommand(
            questionId,
            GetUserIdFromToken(),
            GetUserRolesFromToken());
        var result = await _mediator.Send(command);
        return result.Match(
        result => Ok(result),
        errors => Problem(errors));
    }
    [Authorize(Roles = Roles.Doctor)]
    [HttpPost("questions/question-id/{questionId}/answer")]
    public async Task<IActionResult> AddAnswerQuestion(AddAnswerRequest request, string questionId)
    {
        var command = new AnswerCommand(
            questionId,
            request.AnswerString,
            GetUserIdFromToken());
        var result = await _mediator.Send(command);
        return result.Match(
        result => Ok(result),
        errors => Problem(errors));
    }
    [Authorize(Roles = Roles.Doctor)]
    [HttpPost("questions/answers/answer-id/{answerId}/edit")]
    public async Task<IActionResult> EditAnswerQuestion(EditAnswerRequest request, string answerId)
    {
        var command = new EditAnswerCommand(
            GetUserIdFromToken(),
            answerId,
            request.AnswerString);
        var result = await _mediator.Send(command);
        return result.Match(
        result => Ok(result),
        errors => Problem(errors));
    }
    [Authorize(Roles = Roles.Doctor + "," + Roles.Admin)]
    [HttpDelete("questions/answers/answer-id/{answerId}/delete")]
    public async Task<IActionResult> DeleteAnswerQuestion(string answerId)
    {
        var command = new DeleteAnswerCommand(
            GetUserIdFromToken(),
            answerId,
            GetUserRolesFromToken());
        var result = await _mediator.Send(command);
        return result.Match(
        result => Ok(result),
        errors => Problem(errors));
    }
    [Authorize(Roles = Roles.Doctor)]
    [HttpPost("posts/comments/comment-id/{commentId}/agreement")]
    public async Task<IActionResult> AgreeComment(string commentId)
    {
        var command = new AgreeCommentCommand(
            commentId,
            GetUserIdFromToken());
        var result = await _mediator.Send(command);
        return result.Match(
        result => Ok(result),
        errors => Problem(errors));
    }

    [Authorize(Roles = Roles.Doctor)]
    [HttpPost("posts/comments/comment-id/{commentId}/agreement/delete")]
    public async Task<IActionResult> DeleteAgreeComment(string commentId)
    {
        var command = new DeleteCommentAgreementCommand(
            commentId,
            GetUserIdFromToken());
        var result = await _mediator.Send(command);
        return result.Match(
        result => Ok(result),
        errors => Problem(errors));
    }

    [Authorize(Roles = Roles.Doctor)]
    [HttpPost("posts/post-id/{postId}/rating")]
    public async Task<IActionResult> RatePost(RatePostRequest request, string postId)
    {
        var command = new RatePostCommand(
            postId,
            request.Rating,
            GetUserIdFromToken());
        var result = await _mediator.Send(command);
        return result.Match(
        result => Ok(result),
        errors => Problem(errors));
    }
    [Authorize]
    [HttpPost("subscribe/Doctor-id/{doctorId}")]
    public async Task<IActionResult> Subscribe(string doctorId)
    {
        var command = new SubscribeDoctorCommand(
            doctorId,
            GetUserIdFromToken());
        var result = await _mediator.Send(command);
        return result.Match(
        result => Ok(result),
        errors => Problem(errors));
    }
    [Authorize]
    [HttpPost("unsubscribe/Doctor-id/{doctorId}")]
    public async Task<IActionResult> Unsubscribe(string doctorId)
    {
        var command = new UnsubscribeDoctorCommand(
            doctorId,
            GetUserIdFromToken());
        var result = await _mediator.Send(command);
        return result.Match(
        result => Ok(result),
        errors => Problem(errors));
    }
}