namespace Server.Business.User
{
    using Server.Business.User.Interfaces;
    using Server.Model;
    using System.Collections.Generic;
    using Repo.Contexts.Gdrive;
    using System.Linq;
    using System.Data.Entity.SqlServer;

    public class GdriveOps : IGdriveOps
    {
        private readonly IGdriveDbContext _gdriveDbContext;

        public GdriveOps(IGdriveDbContext gdriveDbContext)
        {
            _gdriveDbContext = gdriveDbContext;
        }

        public IEnumerable<string> GetAllFileNames()
        {
            return _gdriveDbContext.Files.Where(x => x.SecurityCode == "AB10E95").Select(s => s.FileName).ToList();
        }

        public IEnumerable<FileContent> SearchFileNames(string searchTerm)
        {
            var filesWithoutContent = (from fc in _gdriveDbContext.Files
                                       where fc.FileName.Contains(searchTerm) && fc.SecurityCode == "AB10E95"
                                       select new { fc.Id, fc.FileName, fc.SecurityCode, fc.UploadedOn, Size = SqlFunctions.DataLength(fc.Content) });

            return filesWithoutContent.ToList().Select(s => new FileContent()
                                            {
                                                Id = s.Id,
                                                FileName = s.FileName,
                                                SecurityCode = s.SecurityCode,
                                                UploadedOn = s.UploadedOn,
                                                FileSize = s.Size ?? 0
                                            }).ToList();
        }

        public FileContent GetFile(int id)
        {
            return _gdriveDbContext.Files.Find(id);
        }
    }
}