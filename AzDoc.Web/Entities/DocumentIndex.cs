using Microsoft.WindowsAzure.StorageClient;

namespace AzDoc.Web.Entities
{
    public class DocumentIndex : TableServiceEntity
    {
        public DocumentIndex()
        {
            
        }
        public DocumentIndex(string guid) : base(guid, string.Empty)
        {
        }
    }
}