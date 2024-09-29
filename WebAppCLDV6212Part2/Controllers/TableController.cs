using Azure.Data.Tables;
using WebAppCLDV6212Part2.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
namespace WebAppCLDV6212Part2.Controllers
{
    public class TableController : Controller
    {
        private readonly TableServiceClient _tableServiceClient;
        public TableController(IConfiguration configuration)
        {
            _tableServiceClient = new
           TableServiceClient(configuration.GetConnectionString("StorageConnectionString"));
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Insert(CustomerEntity customer)
        {
            var tableClient = _tableServiceClient.GetTableClient("customersTable");
            await tableClient.CreateIfNotExistsAsync();
            await tableClient.AddEntityAsync(customer);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Get(string partitionKey, string rowKey)
        {
            var tableClient = _tableServiceClient.GetTableClient("customersTable");
            var entity = await tableClient.GetEntityAsync<CustomerEntity>(partitionKey,
           rowKey);
            return View(entity);
        }
    }
}
