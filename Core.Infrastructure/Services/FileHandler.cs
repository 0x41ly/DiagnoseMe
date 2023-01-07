using Core.Application.Common.Interfaces.Services;
using ErrorOr;

namespace Core.Infrastructure.Services;

public class FileHandler : IFileHandler
{
    public ErrorOr<bool> SaveFile(byte[] file)
    {
        throw new NotImplementedException();
    }
}