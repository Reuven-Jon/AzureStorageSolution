using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using AzureStorageSolution.Services;

namespace AzureStorageSolution.Controllers
{
        public class MediaController : Controller
        {
            private readonly BlobStorageService _blobStorageService;

            public MediaController(BlobStorageService blobStorageService)
            {
                _blobStorageService = blobStorageService;
            }

            // Upload Action
            [HttpPost]
            public async Task<IActionResult> UploadMedia(IFormFile file)
            {
                if (file != null && file.Length > 0)
                {
                    using (var stream = file.OpenReadStream())
                    {
                        string contentType = file.ContentType;
                        string fileName = file.FileName;

                        string url = await _blobStorageService.UploadBlobAsync("mediacontainer", fileName, stream, contentType);

                        ViewBag.Message = "Upload successful";
                        ViewBag.Url = url; // This is the URL of the uploaded blob
                    }
                }
                return View();
            }

            // Download Action
            [HttpGet]
            public async Task<IActionResult> DownloadMedia(string fileName)
            {
                var stream = await _blobStorageService.DownloadBlobAsync("mediacontainer", fileName);
                return File(stream, "application/octet-stream", fileName);
            }

            // Delete Action
            [HttpPost]
            public async Task<IActionResult> DeleteMedia(string fileName)
            {
                await _blobStorageService.DeleteBlobAsync("mediacontainer", fileName);
                ViewBag.Message = "Delete successful";
                return View();
            }
        }
    }
