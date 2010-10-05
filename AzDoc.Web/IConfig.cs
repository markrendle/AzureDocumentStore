using Microsoft.WindowsAzure;

namespace AzDoc.Web
{
    public interface IConfig
    {
        CloudStorageAccount GetStorageAccount();
    }
}