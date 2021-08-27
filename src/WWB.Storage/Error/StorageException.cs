using System;

namespace WWB.Storage.Error
{
    public class StorageException : Exception
    {
        public StorageException(StorageError error) : base(error.Message)
        {
            ErrorCode = error.Code;
        }
        public StorageException(StorageError error, Exception ex) : base(error.Message, ex)
        {
            ErrorCode = error.Code;
            ProviderMessage = ex?.Message;
        }

        public int ErrorCode { get; private set; }

        public string ProviderMessage { get; set; }
    }
}
