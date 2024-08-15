using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using AzureStorageSolution.Services;

namespace AzureStorageSolution.Controllers
{
    public class FileController : Controller
    {
        private readonly FileStorageService _fileStorageService;

        public FileController(FileStorageService fileStorageService)
        {
            _fileStorageService = fileStorageService;
        }

        // Upload Contract or Log File
        [HttpPost]
        public async Task<IActionResult> UploadFile(IFormFile file, string directoryName)
        {
            if (file != null && file.Length > 0)
            {
                using (var stream = file.OpenReadStream())
                {
                    string fileName = file.FileName;

                    string url = await _fileStorageService.UploadFileAsync("contractsandlogs", directoryName, fileName, stream);

                    ViewBag.Message = "File uploaded successfully.";
                    ViewBag.Url = url; // This is the URL of the uploaded file
                }
            }
            return View();
        }

        // Download Contract or Log File
        [HttpGet]
        public async Task<IActionResult> DownloadFile(string directoryName, string fileName)
        {
            var stream = await _fileStorageService.DownloadFileAsync("contractsandlogs", directoryName, fileName);
            return File(stream, "application/octet-stream", fileName);
        }

        // Delete Contract or Log File
        [HttpPost]
        public async Task<IActionResult> DeleteFile(string directoryName, string fileName)
        {
            await _fileStorageService.DeleteFileAsync("contractsandlogs", directoryName, fileName);
            ViewBag.Message = "File deleted successfully.";
            return View();
        }
    }
}