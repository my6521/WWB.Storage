using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Threading.Tasks;
using WWB.Storage.Error;
using WWB.Storage.Model;

namespace WWB.Storage.Local
{
    public class LocalStorageProvider : IStorageProvider
    {
        private readonly LocalStorageConfig _cfg;
        private readonly string _rootPath;
        private readonly string _rootUrl;

        public LocalStorageProvider(IServiceProvider serviceProvider, LocalStorageConfig cfg)
        {
            _cfg = cfg;
            _rootUrl = cfg.BaseUrl;
            _rootPath = GetRootPath(serviceProvider);
        }

        private string GetRootPath(IServiceProvider serviceProvider)
        {
            var env = serviceProvider.GetService<IHostingEnvironment>();
            if (string.IsNullOrEmpty(env.WebRootPath))
                env.WebRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
            var rootPath = env.WebRootPath;
            if (!Directory.Exists(rootPath))
                Directory.CreateDirectory(rootPath);

            return rootPath;
        }

        private void ExceptionHandling(Action ioAction)
        {
            try
            {
                ioAction();
            }
            catch (UnauthorizedAccessException ex)
            {
                throw new StorageException(StorageErrorCode.InvalidAccess.ToStorageError(), ex);
            }
            catch (ArgumentException ex)
            {
                throw new StorageException(StorageErrorCode.InvalidBlobName.ToStorageError(), ex);
            }
            catch (DirectoryNotFoundException ex)
            {
                throw new StorageException(StorageErrorCode.ContainerNotFound.ToStorageError(), ex);
            }
            catch (NotSupportedException ex)
            {
                throw new StorageException(StorageErrorCode.InvalidBlobName.ToStorageError(), ex);
            }
            catch (FileNotFoundException ex)
            {
                throw new StorageException(StorageErrorCode.FileNotFound.ToStorageError(), ex);
            }
            catch (IOException ex)
            {
                throw new StorageException(StorageErrorCode.BlobInUse.ToStorageError(), ex);
            }
            catch (Exception ex)
            {
                throw new StorageException(StorageErrorCode.GenericException.ToStorageError(), ex);
            }
        }

        private T ExceptionHandling<T>(Func<T> ioFunc)
        {
            try
            {
                return ioFunc();
            }
            catch (UnauthorizedAccessException ex)
            {
                throw new StorageException(StorageErrorCode.InvalidAccess.ToStorageError(), ex);
            }
            catch (ArgumentException ex)
            {
                throw new StorageException(StorageErrorCode.InvalidBlobName.ToStorageError(), ex);
            }
            catch (DirectoryNotFoundException ex)
            {
                throw new StorageException(StorageErrorCode.ContainerNotFound.ToStorageError(), ex);
            }
            catch (NotSupportedException ex)
            {
                throw new StorageException(StorageErrorCode.InvalidBlobName.ToStorageError(), ex);
            }
            catch (FileNotFoundException ex)
            {
                throw new StorageException(StorageErrorCode.FileNotFound.ToStorageError(), ex);
            }
            catch (IOException ex)
            {
                throw new StorageException(StorageErrorCode.BlobInUse.ToStorageError(), ex);
            }
            catch (Exception ex)
            {
                throw new StorageException(StorageErrorCode.GenericException.ToStorageError(), ex);
            }
        }

        public async Task<BlobFileInfo> PutBlob(string blobName, Stream source)
        {
            return await Task.Run(() =>
            {
                return ExceptionHandling(() =>
                 {
                     // check file exists
                     var filePath = Path.Combine(_rootPath, _cfg.BucketName, blobName);
                     if (File.Exists(filePath))
                         throw new StorageException(StorageErrorCode.BlobInUse.ToStorageError());
                     //check directory
                     var fileInfo = new FileInfo(filePath);
                     if (!Directory.Exists(fileInfo.DirectoryName))
                         Directory.CreateDirectory(fileInfo.DirectoryName);

                     using (var file = File.Create(filePath))
                     {
                         source.CopyTo(file);
                     }

                     return new BlobFileInfo()
                     {
                         Name = blobName,
                         BucketName = _cfg.BucketName,
                         BaseUrl = GetUrlByKey(blobName)
                     };

                 });
            });
        }

        public async Task DeleteBlob(string blobName)
        {
            await Task.Run(() =>
            {
                ExceptionHandling(() =>
                {
                    var filePath = Path.Combine(_rootPath, _cfg.BucketName, blobName);
                    if (File.Exists(filePath))
                    {
                        File.Delete(filePath);
                    }
                });
            });
        }

        public async Task DeleteBlob(string bucketName, string blobName)
        {
            await Task.Run(() =>
            {
                ExceptionHandling(() =>
                {
                    var filePath = Path.Combine(_rootPath, bucketName, blobName);
                    if (File.Exists(filePath))
                    {
                        File.Delete(filePath);
                    }
                });
            });
        }

        private string GetUrlByKey(string key) => $"{_cfg.BaseUrl}/{_cfg.BucketName}/{key}";
    }
}
