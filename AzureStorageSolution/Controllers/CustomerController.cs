using Microsoft.AspNetCore.Mvc;
using static AzureStorageSolution.Models.EntityClass;

namespace AzureStorageSolution.Controllers
{
    public class CustomerController : Controller
    {
        private readonly TableService _tableService;

        public CustomerController(TableService tableService)
        {
            _tableService = tableService;
        }

        // Create
        [HttpPost]
        public async Task<IActionResult> CreateCustomer(CustomerProfile customerProfile)
        {
            await _tableService.AddCustomerProfileAsync(customerProfile);
            return RedirectToAction("Index");
        }

        // Read
        [HttpGet]
        public async Task<IActionResult> GetCustomer(string partitionKey, string rowKey)
        {
            var customer = await _tableService.GetCustomerProfileAsync(partitionKey, rowKey);
            return View(customer);
        }

        // Update
        [HttpPost]
        public async Task<IActionResult> UpdateCustomer(CustomerProfile customerProfile)
        {
            await _tableService.UpdateCustomerProfileAsync(customerProfile);
            return RedirectToAction("Index");
        }

        // Delete
        [HttpPost]
        public async Task<IActionResult> DeleteCustomer(string partitionKey, string rowKey)
        {
            await _tableService.DeleteCustomerProfileAsync(partitionKey, rowKey);
            return RedirectToAction("Index");
        }
    }
}
