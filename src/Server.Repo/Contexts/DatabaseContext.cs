using System.Data.Entity;
using Server.Model;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Server.Repo.Contexts
{
    public class DatabaseContext : IdentityDbContext<ApplicationUser>
    {
        //private UserContext _contextUser;

        public DatabaseContext()
            : base("DatabaseContext", throwIfV1Schema: false)
        {
            //_contextUser = new UserContext();
        }

        //public DbSet<SampleModel> SampleModels { get; set; }
    }
}
