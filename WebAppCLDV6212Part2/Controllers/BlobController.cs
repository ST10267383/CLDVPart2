using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
namespace WebAppCLDV6212Part2.Controllers
{
    public class BlobController : Controller //(IIE, 2024)
    {
        private readonly BlobServiceClient _blobServiceClient;
        public BlobController(IConfiguration configuration)
        {
            _blobServiceClient = new
           BlobServiceClient(configuration.GetConnectionString("StorageConnectionString")); //(Pritel, 2018)
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> UploadBlob(IFormFile file) //(IIE, 2024)
        {
            if (file == null || file.Length == 0)
                return Content("File not selected");
            var containerClient =
           _blobServiceClient.GetBlobContainerClient("productimages");
            await containerClient.CreateIfNotExistsAsync();
            var blobClient = containerClient.GetBlobClient(file.FileName);
            using (var stream = file.OpenReadStream())
            {
                await blobClient.UploadAsync(stream, true);
            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> DownloadBlob(string blobName) //(Pritel, 2018)
        {
            var containerClient =
           _blobServiceClient.GetBlobContainerClient("productimages");
            var blobClient = containerClient.GetBlobClient(blobName);
            var download = await blobClient.DownloadAsync();
            return File(download.Value.Content, "application/octet-stream", blobName);
        }
    }
}
