using Azure;
using Azure.Storage.Files.Shares;
using Azure.Storage.Files.Shares.Models;
using System;
using System.IO;
using System.Threading.Tasks;

namespace AzureStorageSolution.Services
{
    public class FileStorageService
    {
        private readonly ShareServiceClient _shareServiceClient;

        public FileStorageService(string storageAccountConnectionString)
        {
            // Initialize ShareServiceClient
            _shareServiceClient = new ShareServiceClient(storageAccountConnectionString);
        }

        public async Task<string> UploadFileAsync(string shareName, string directoryName, string fileName, Stream content)
        {
            // Get a reference to the file share
            ShareClient shareClient = _shareServiceClient.GetShareClient(shareName);
            await shareClient.CreateIfNotExistsAsync();

            // Get a reference to the directory within the share
            ShareDirectoryClient directoryClient = shareClient.GetDirectoryClient(directoryName);
            await directoryClient.CreateIfNotExistsAsync();

            // Get a reference to the file within the directory
            ShareFileClient fileClient = directoryClient.GetFileClient(fileName);

            // Upload the file
            await fileClient.CreateAsync(content.Length);
            await fileClient.UploadRangeAsync(new HttpRange(0, content.Length), content);

            // Return the URI of the uploaded file
            return fileClient.Uri.ToString();
        }

        public async Task<Stream> DownloadFileAsync(string shareName, string directoryName, string fileName)
        {
            // Get a reference to the file share
            ShareClient shareClient = _shareServiceClient.GetShareClient(shareName);

            // Get a reference to the directory within the share
            ShareDirectoryClient directoryClient = shareClient.GetDirectoryClient(directoryName);

            // Get a reference to the file within the directory
            ShareFileClient fileClient = directoryClient.GetFileClient(fileName);

            // Download the file
            ShareFileDownloadInfo download = await fileClient.DownloadAsync();
            return download.Content;
        }

        public async Task DeleteFileAsync(string shareName, string directoryName, string fileName)
        {
            // Get a reference to the file share
            ShareClient shareClient = _shareServiceClient.GetShareClient(shareName);

            // Get a reference to the directory within the share
            ShareDirectoryClient directoryClient = shareClient.GetDirectoryClient(directoryName);

            // Get a reference to the file within the directory
            ShareFileClient fileClient = directoryClient.GetFileClient(fileName);

            // Delete the file
            await fileClient.DeleteIfExistsAsync();
        }
    }
}