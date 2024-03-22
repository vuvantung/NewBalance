using NewBalance.Application.Requests;

namespace NewBalance.Application.Interfaces.Services
{
    public interface IUploadService
    {
        string UploadAsync(UploadRequest request);
    }
}