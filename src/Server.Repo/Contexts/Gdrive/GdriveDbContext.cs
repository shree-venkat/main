using Server.Model;
using System.Data.Entity;

namespace Server.Repo.Contexts.Gdrive
{
    public class GdriveDbContext : DbContext, IGdriveDbContext
    {
        public GdriveDbContext()
            : base("name=BlogDbConnection")
        {
            Configuration.ProxyCreationEnabled = false;
        }

        public virtual DbSet<FileContent> Files { get; set; }
    }
}
