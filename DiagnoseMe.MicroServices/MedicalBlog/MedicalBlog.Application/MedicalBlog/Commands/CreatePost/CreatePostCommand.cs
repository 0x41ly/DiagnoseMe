using System.ComponentModel.Design;
using ErrorOr;
using MediatR;
using MedicalBlog.Application.MedicalBlog.Common;

namespace MedicalBlog.Application.MedicalBlog.Commands.CreatePost;
 public record CreatePostCommand(
    string AuthorId,
     string Title,
     string Content,
     List<string> Tags
 ) : IRequest<ErrorOr<CommandResponse>>;