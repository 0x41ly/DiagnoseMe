using Core.Application.Common.Interfaces.Services;
using ErrorOr;

namespace Core.Infrastructure.Services;

public class FileHandler : IFileHandler
{
    public ErrorOr<string> SaveFile(byte[] file)
    {
        throw new NotImplementedException();
    }
}