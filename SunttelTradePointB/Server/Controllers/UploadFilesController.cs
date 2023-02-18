using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO.Pipelines;

namespace SunttelTradePointB.Server.Controllers
{

    /// <summary>
    /// Controller intended to manage files uploading
    /// </summary>
    [Tags("API Controller to upload Files")]
    [Route("api/UploadFiles")]
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
        public async Task<IActionResult> UploadFiles()
        {
            var formCollection = await Request.ReadFormAsync();
            
            var photo = formCollection.Files["photo"];


            if (photo == null)
            {
                return BadRequest("Invalid file");
            }

            string uploadFolder = "";
            string uploadingFileType = "";
            string photoName = "";
            

            uploadingFileType = formCollection["uploadingFileType"].ToString();
            photoName = formCollection["photoName"].ToString();


            switch (uploadingFileType)
            {
                case "0":
                    uploadFolder = "transactionalItemsImages";
                    break;
                case "1":
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
            var file = formCollection.Files.First();

         

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await photo.CopyToAsync(stream);
            }

            return Ok(new { filePath });
        }


    }
}
