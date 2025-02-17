using Azure.Identity;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

namespace ProgramInformationV2.Data.Images {

    public class AzureStorage {
        private const string _azureContainerName = "images";

        private static readonly Dictionary<string, string> _images = new() {
            {"image/gif", ".gif"},
            {"image/heic", ".heic"},
            {"image/jpeg", ".jpg" },
            {"image/png", ".png"},
            {"image/tiff", ".tif"},
            {"image/webp", ".webp"},
        };

        private readonly string _azureClientUrl = "";

        public AzureStorage() {
        }

        public AzureStorage(string azureClientUrl) {
            _azureClientUrl = azureClientUrl;
        }

        public async Task<bool> Delete(string url) {
            var filename = url.Split('/').Last();
            var blobServiceClient = GetServiceClient();
            var containerClient = blobServiceClient.GetBlobContainerClient(_azureContainerName);
            var blobClient = containerClient.GetBlobClient(filename);
            return await blobClient.DeleteIfExistsAsync();
        }

        public string GetFullPath(string filename) => $"{_azureClientUrl}/{_azureContainerName}/{filename}";

        public async Task<string> Move(string newFilename, string oldUrl) {
            var blobServiceClient = GetServiceClient();
            var containerClient = blobServiceClient.GetBlobContainerClient(_azureContainerName);
            var destBlobClient = containerClient.GetBlobClient(newFilename);
            _ = await destBlobClient.StartCopyFromUriAsync(new Uri(oldUrl));
            return newFilename;
        }

        public async Task<string> Upload(string name, string contentType, Stream stream) {
            if (!_images.TryGetValue(contentType, out var value)) {
                return string.Empty;
            }
            var filename = $"{name}{value}";
            var blobServiceClient = GetServiceClient();
            var containerClient = blobServiceClient.GetBlobContainerClient(_azureContainerName);
            var blobClient = containerClient.GetBlobClient(filename);
            _ = await blobClient.UploadAsync(stream, new BlobUploadOptions { HttpHeaders = new BlobHttpHeaders { ContentType = contentType } });
            return filename;
        }

        private BlobServiceClient GetServiceClient() => new(
                new Uri(_azureClientUrl),
                new DefaultAzureCredential(true));
    }
}