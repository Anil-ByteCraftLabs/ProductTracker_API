using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using ProductTracker.Application.Interfaces.FileStorage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductTracker.Infrastructure.Repository.FileStorage
{
    public class AzureBlobStorageProvider : IFileStorageProvider
    {
        private static readonly object SyncRoot = new object();
        private CloudBlobContainer _blobContainer;

        private CloudBlobContainer BlobContainer
        {
            get
            {
                if (_blobContainer == null)
                {
                    lock (SyncRoot)
                    {
                        if (_blobContainer == null)
                        {
                            var storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("AzureBlobStorageConnectionString"));
                            var blobClient = storageAccount.CreateCloudBlobClient();
                            var blobContainerName = (CloudConfigurationManager.GetSetting("AzureBlobStorageContainerName") ?? "Default")
                                        .ToLower();

                            _blobContainer = blobClient.GetContainerReference(blobContainerName);
                            _blobContainer.CreateIfNotExistsAsync().Wait();
                        }
                    }
                }

                return _blobContainer;
            }
        }

        public async Task<bool> DoesFileExists(string fileName)
        {
            var blob = BlobContainer.GetBlobReference(fileName);
            return await blob.ExistsAsync();
        }

        public async Task RemoveFileAsync(string fileName)
        {
            var blob = BlobContainer.GetBlockBlobReference(fileName);
            await blob.DeleteAsync().ConfigureAwait(false);
        }

        public async Task SaveFileAsync(string fileName, Stream sourceStream, bool overwriteFile = false)
        {
            sourceStream.Position = 0;
            var blob = BlobContainer.GetBlockBlobReference(fileName);

            if (overwriteFile)
                await blob.UploadFromStreamAsync(sourceStream).ConfigureAwait(false);
            else
                await
                    blob.UploadFromStreamAsync(sourceStream, AccessCondition.GenerateIfNoneMatchCondition("*"), null,
                        null).ConfigureAwait(false);
        }
    }
}
