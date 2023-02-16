using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SunttelTradePointB.Server.Controllers
{

    /// <summary>
    /// Controller intended to manage files uploading
    /// </summary>
    [Tags("API Controller to upload Files")]
    [Route("api/[controller]")]
    [ApiController]
    public class UploadFilesController : ControllerBase
    {

        /// <summary>
        /// File Type to upload
        /// </summary>
        public enum UploadingFileType
        {
            /// <summary>
            /// Image file of a Transactional Item
            /// </summary>
            TransactionalItemImage,

            /// <summary>
            /// Image file of an Entity
            /// </summary>
            EntityImage
        }

        /// <summary>
        /// Uploads a photo
        /// </summary>
        /// <param name="photo"></param>
        /// <param name="uploadingFileType"></param>
        /// <param name="photoName"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> UploadPhoto(IFormFile photo, UploadingFileType uploadingFileType, string photoName)
        {
            if (photo == null || photo.Length == 0)
            {
                return BadRequest("Invalid file");
            }

            string uploadFolder = "";

            switch (uploadingFileType)
            {
                case UploadingFileType.TransactionalItemImage:
                    uploadFolder = "transactionalItemsImages";
                    break;
                case UploadingFileType.EntityImage:
                    uploadFolder = "entityImages";
                    break;

            }
            var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), uploadFolder);

            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }

            var fileName = photoName == "" ?Guid.NewGuid().ToString(): photoName + Path.GetExtension(photo.FileName);


            var filePath = Path.Combine(uploadPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await photo.CopyToAsync(stream);
            }

            return Ok(new { filePath });
        }


    }
}
