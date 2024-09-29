using Azure.Storage.Files.Shares;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
namespace WebAppCLDV6212Part2.Controllers
{
    public class FileController : Controller
    {
        private readonly ShareServiceClient _shareServiceClient;
        public FileController(IConfiguration configuration)
        {
            _shareServiceClient = new
           ShareServiceClient(configuration.GetConnectionString("StorageConnectionString"));
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return Content("File not selected");
            var shareClient = _shareServiceClient.GetShareClient("contractsandlogs");
            await shareClient.CreateIfNotExistsAsync();
            var directoryClient = shareClient.GetRootDirectoryClient();
            var fileClient = directoryClient.GetFileClient(file.FileName);
            using (var stream = file.OpenReadStream())
            {
                await fileClient.CreateAsync(file.Length);
                await fileClient.UploadAsync(stream);
            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> DownloadFile(string fileName)
        {
            var shareClient = _shareServiceClient.GetShareClient("contractsandlogs");
            var directoryClient = shareClient.GetRootDirectoryClient();
            var fileClient = directoryClient.GetFileClient(fileName);
            var download = await fileClient.DownloadAsync();
            return File(download.Value.Content, "application/octet-stream", fileName);
        }
    }
}
