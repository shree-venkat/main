namespace Server.Business.User.Interfaces
{
    using Server.Model;
    using System.Collections.Generic;

    public interface IGdriveOps
    {
        IEnumerable<string> GetAllFileNames();

        IEnumerable<FileContent> SearchFileNames(string searchTerm);

        FileContent GetFile(int id);
    }
}