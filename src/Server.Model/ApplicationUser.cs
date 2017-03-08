using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Model
{
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            var userIdentity = await manager.CreateIdentityAsync(this, null);

            return userIdentity;
        }

        public bool IsActive { get; set; }

        public string Forename { get; set; }

        public string Surname { get; set; }

        [NotMapped]
        public string DisplayName { get; set; }
    }
}