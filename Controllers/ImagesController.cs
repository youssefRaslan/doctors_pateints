using doctors.DTO;
using doctors.services.interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace doctors.controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly Icloudinarycs _cloudinaryService;

        public ImagesController(Icloudinarycs cloudinaryService)
        {
            _cloudinaryService = cloudinaryService;
        }

    
        [HttpPost("uploadphoto")]
        [Consumes("multipart/form-data")] 
        public async Task<ActionResult<UploadImageResult>> Upload([FromForm] UploadImageDto dto) 
        { 
            if (dto.File == null || dto.File.Length == 0)
                
                return BadRequest("No file selected.");
            try 
            {
            
                var imageUrl = await _cloudinaryService.UploadImageAsync(dto.File);
                
                return Ok(new { Url = imageUrl }); } 
            
            catch (Exception ex)
            { 
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = ex.Message }); } }

       
        [HttpDelete("deletephoto")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Delete([FromQuery] string publicId)
        {
            if (string.IsNullOrEmpty(publicId))
                return BadRequest("Public ID is required.");

            try
            {
                var result = await _cloudinaryService.DeleteImageAsync(publicId);

                if (result)
                    return Ok(new { Message = "Image deleted successfully." });
                else
                    return BadRequest(new { Message = "Failed to delete image." });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = ex.Message });
            }
        }
    }
}
