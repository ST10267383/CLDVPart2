using Azure.Storage.Queues;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
namespace WebAppCLDV6212Part2.Controllers
{
    public class QueueController : Controller //(Bokaba, 2024)
    {
        private readonly QueueServiceClient _queueServiceClient;
        public QueueController(IConfiguration configuration)
        {
            _queueServiceClient = new
           QueueServiceClient(configuration.GetConnectionString("StorageConnectionString")) //(Pritel, 2018)
           ;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SendMessage(string message)
        {
            var queueClient = _queueServiceClient.GetQueueClient("orderprocessing"); //(IIE, 2024)
            await queueClient.CreateIfNotExistsAsync();
            await queueClient.SendMessageAsync(message);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> ReceiveMessage()
        {
            var queueClient = _queueServiceClient.GetQueueClient("orderprocessing");
            var receivedMessage = await queueClient.ReceiveMessageAsync();
            if (receivedMessage != null)
            {
                return Content(receivedMessage.Value.MessageText);
            }
            return Content("No message available");
        }
    }
}
