using System.Data;

namespace Pickleball_project.Repository
{
    public interface IClientRepository
    {
        string DocumentUpload(IFormFile formFile);
        DataTable ClientDataTable(string path);
        void ImportClient(DataTable client);
    }
}
