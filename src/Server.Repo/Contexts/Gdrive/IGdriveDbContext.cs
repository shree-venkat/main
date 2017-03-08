using Server.Model;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Threading.Tasks;

namespace Server.Repo.Contexts.Gdrive
{
    public interface IGdriveDbContext
    {
        int SaveChanges();

        Task<int> SaveChangesAsync();

        DbEntityEntry Entry(object entity);

        DbSet<FileContent> Files { get; set; }
    }
}
