using ErrorOr;
namespace MedicalBlog.Application.Common.Interfaces.Services;


public interface IFileHandler
{
    ErrorOr<string> SaveFile(byte[] file);
}