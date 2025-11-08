using doctors.DTO;

namespace doctors.services.interfaces
{
    public interface Icloudinarycs
    {
        Task<UploadImageResult> UploadImageAsync(IFormFile file);
        Task<bool> DeleteImageAsync(string publicId);
    }
}
