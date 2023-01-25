using Auth.Application.Common.Interfaces.Services;
using ErrorOr;

namespace Auth.Infrastructure.Services;

public class FileHandler : IFileHandler
{
    public ErrorOr<string> SaveFile(byte[] file)
    {
        throw new NotImplementedException();
    }
}