namespace Server.Repo.Repositories.Interfaces
{
    using Server.Model;
    using System.Collections.Generic;

    public interface IAuthRepository
    {
        /// <summary>
        /// Retrieves all users.
        /// </summary>
        /// <returns>All Identity users.</returns>
        IEnumerable<ApplicationUser> GetAllUsers();

        /// <summary>
        /// Retrieves user details for a given user name.
        /// </summary>
        /// <param name="userName">User name.</param>
        /// <returns>User details.</returns>
        ApplicationUser GetUserByAdAccount(string userName);

        /// <summary>
        /// Retrieves all Roles that the user is in.
        /// </summary>
        /// <param name="userId">User ID.</param>
        /// <returns>User roles.</returns>
        IEnumerable<string> GetUserRoles(string userId);

        /// <summary>
        /// Retrieves all Roles.
        /// </summary>
        /// <returns>All roles.</returns>
        IEnumerable<string> GetAllRoles();

        /// <summary>
        /// Creates a new user and adds the user in given roles.
        /// </summary>
        /// <param name="user">New user to be created.</param>
        /// <param name="roles">Roles that the user needs to be added to.</param>
        /// <returns>User details.</returns>
        ApplicationUser CreateUser(ApplicationUser user, IEnumerable<string> roles);

        /// <summary>
        /// Deletes user.
        /// </summary>
        /// <param name="user">User details</param>
        void DeleteUser(ApplicationUser user);
    }
}