using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using doctors.data;
using doctors.DTO;
using doctors.services.interfaces;
using Microsoft.Extensions.Options;

public class CloudinaryService : Icloudinarycs
{
    private readonly Cloudinary _cloudinary;

    public CloudinaryService(IOptions<CloudinarySettings> config)
    {
        var acc = new Account(
            config.Value.CloudName,
            config.Value.ApiKey,
            config.Value.ApiSecret
        );
        _cloudinary = new Cloudinary(acc);
    }

    public async Task<UploadImageResult> UploadImageAsync(IFormFile file)
    {
        if (file == null || file.Length == 0)
            return null;
        var allowedTypes = new[] { "image/jpeg", "image/png", "image/gif", "image/webp" };
        if (!allowedTypes.Contains(file.ContentType.ToLower()))
            throw new Exception("Only image files are allowed (jpeg, png, gif, webp).");

        var uploadResult = new ImageUploadResult();

        using var stream = file.OpenReadStream();
        var uploadParams = new ImageUploadParams
        {
            File = new FileDescription(file.FileName, stream),
            Folder = "doctors"
        };

        uploadResult = await _cloudinary.UploadAsync(uploadParams);

        // تحقق من النتيجة
        if (uploadResult.StatusCode == System.Net.HttpStatusCode.OK && uploadResult.SecureUrl != null)
        {
            return new UploadImageResult
            {
                Url = uploadResult.SecureUrl.ToString(),
                PublicId = uploadResult.PublicId
            };
        }
        else
        {
            throw new Exception($"Cloudinary upload failed: {uploadResult.Error?.Message}");
        }
    }

    public async Task<bool> DeleteImageAsync(string publicId)
    {
        var deleteParams = new DeletionParams(publicId);
        var result = await _cloudinary.DestroyAsync(deleteParams);
        return result.Result == "ok";
    }
}
