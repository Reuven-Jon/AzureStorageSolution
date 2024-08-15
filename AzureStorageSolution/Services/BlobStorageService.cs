using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using System;
using System.IO;
using System.Threading.Tasks;


namespace AzureStorageSolution.Services
{
    public class BlobStorageService
    {
        private readonly BlobServiceClient _blobServiceClient;

        public BlobStorageService(string storageAccountConnectionString)
        {
            // Initialize BlobServiceClient
            _blobServiceClient = new BlobServiceClient(storageAccountConnectionString);
        }

        public async Task<string> UploadBlobAsync(string containerName, string blobName, Stream content, string contentType)
        {
            // Get a reference to the container
            BlobContainerClient containerClient = _blobServiceClient.GetBlobContainerClient(containerName);
            await containerClient.CreateIfNotExistsAsync(PublicAccessType.Blob);

            // Get a reference to the blob
            BlobClient blobClient = containerClient.GetBlobClient(blobName);

            // Upload the blob
            await blobClient.UploadAsync(content, new BlobHttpHeaders { ContentType = contentType });

            // Return the URI of the uploaded blob
            return blobClient.Uri.ToString();
        }

        public async Task<Stream> DownloadBlobAsync(string containerName, string blobName)
        {
            // Get a reference to the container
            BlobContainerClient containerClient = _blobServiceClient.GetBlobContainerClient(containerName);

            // Get a reference to the blob
            BlobClient blobClient = containerClient.GetBlobClient(blobName);

            // Download the blob
            BlobDownloadInfo download = await blobClient.DownloadAsync();
            return download.Content;
        }

        public async Task DeleteBlobAsync(string containerName, string blobName)
        {
            // Get a reference to the container
            BlobContainerClient containerClient = _blobServiceClient.GetBlobContainerClient(containerName);

            // Get a reference to the blob
            BlobClient blobClient = containerClient.GetBlobClient(blobName);

            // Delete the blob
            await blobClient.DeleteIfExistsAsync();
        }
    }
}