using AzureStorageSolution.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AzureStorageSolution.Controllers
{
    public class OrderController : Controller
    {
        private readonly QueueService _queueService;

        public OrderController(QueueService queueService)
        {
            _queueService = queueService;
        }

        // Send Order to Queue
        [HttpPost]
        public async Task<IActionResult> ProcessOrder(string orderDetails)
        {
            await _queueService.SendMessageAsync("orderqueue", orderDetails);
            ViewBag.Message = "Order processed and added to the queue.";
            return View();
        }

        // Receive and Process Order from Queue
        [HttpGet]
        public async Task<IActionResult> GetProcessedOrder()
        {
            string orderDetails = await _queueService.ReceiveMessageAsync("orderqueue");

            if (!string.IsNullOrEmpty(orderDetails))
            {
                ViewBag.Message = "Order retrieved from the queue: " + orderDetails;
            }
            else
            {
                ViewBag.Message = "No orders in the queue.";
            }

            return View();
        }
    }
}