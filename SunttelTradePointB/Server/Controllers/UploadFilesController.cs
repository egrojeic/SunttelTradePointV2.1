using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
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

        private readonly IWebHostEnvironment _env;

        /// <summary>
        /// Constructor to get the hosting environment
        /// </summary>
        /// <param name="env"></param>
        public UploadFilesController(IWebHostEnvironment env)
        {
            _env = env;
        }



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
            string rootpath = AppDomain.CurrentDomain.BaseDirectory;

            var strHostFolder = rootpath.Replace("\\Server\\bin\\Debug\\net7.0\\", "\\Client\\wwwroot\\uploads\\");

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
                case "2":
                    uploadFolder = "qualityImages";
                    break;

            }
            //var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), uploadFolder);

            var uploadPath = Path.Combine(strHostFolder, uploadFolder);


            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }

            var fileName = photoName == "" ?Guid.NewGuid().ToString(): photoName;


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
