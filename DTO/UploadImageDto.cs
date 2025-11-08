namespace doctors.DTO
{
    public class UploadImageDto
    {
        public IFormFile File { get; set; }
    }
    public class UploadImageResult
    {
        public string Url { get; set; }
        public string PublicId { get; set; }
    }

}
