using MedicalBlog.Application.Common.Interfaces.Services;
using ErrorOr;

namespace MedicalBlog.Infrastructure.Services;

public class FileHandler : IFileHandler
{
    public ErrorOr<string> SaveFile(byte[] file)
    {
        throw new NotImplementedException();
    }
}