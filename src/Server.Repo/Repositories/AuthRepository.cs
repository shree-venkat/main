namespace Server.Repo.Repositories
{
    using Server.Repo.Contexts;
    using Server.Repo.Repositories.Interfaces;
    using Server.Model;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class AuthRepository : IAuthRepository
    {
        private readonly DatabaseContext _dbContext;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public AuthRepository()
        {
            _dbContext = new DatabaseContext();
            _roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(_dbContext));
            _userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(_dbContext));
        }

        public IEnumerable<ApplicationUser> GetAllUsers()
        {
            var users = _userManager.Users;
            return users;
        }

        public ApplicationUser GetUserByAdAccount(string userName)
        {
            var user = _userManager.FindByName(userName);
            return user;
        }

        public IEnumerable<string> GetUserRoles(string userId)
        {
            var userRoles = _userManager.GetRoles(userId);
            return userRoles;
        }

        public IEnumerable<string> GetAllRoles()
        {
            var user = _roleManager.Roles.Select(s => s.Name).ToList();
            return user;
        }

        public ApplicationUser CreateUser(ApplicationUser user, IEnumerable<string> roles)
        {
            var result = _userManager.Create(user);

            if (result.Succeeded)
            {
                var userRoles = _userManager.GetRoles(user.Id);
                _userManager.RemoveFromRoles(user.Id, userRoles.ToArray());
                _userManager.AddToRoles(user.Id, roles.ToArray());
            }
            else
            {
                throw new Exception(string.Format("Following Errors Occurred: {0}", string.Join("; ", result.Errors)));
            }

            return user;
        }

        public void DeleteUser(ApplicationUser user)
        {
            var result = _userManager.Delete(user);
            if (!result.Succeeded)
            {
                throw new Exception(string.Format("Following Errors Occurred: {0}", string.Join("; ", result.Errors)));
            }
        }
    }
}
