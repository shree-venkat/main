namespace Server.Business.User
{
    using Server.Business.Helpers.Interfaces;
    using Server.Business.User.Interfaces;
    using Server.Repo.Repositories.Interfaces;
    using Server.Model;
    using Server.Model.Constants;
    using System;
    using System.DirectoryServices;
    using System.Collections.Generic;

    public class UserOps : IUserOps
    {
        private readonly IAuthRepository _authRepo;
        private readonly IConfigManager _config;

        public UserOps(IAuthRepository authRepo, IConfigManager config)
        {
            _authRepo = authRepo;
            _config = config;
        }

        public IEnumerable<ApplicationUser> GetAllUsers()
        {
            var users = _authRepo.GetAllUsers();
            return users;
        }

        public ApplicationUser GetUserByAdAccount(string userName)
        {
            var userDetails = _authRepo.GetUserByAdAccount(userName);
            return userDetails;
        }

        public IEnumerable<string> GetUserRoles(string userId)
        {
            var userRoles = _authRepo.GetUserRoles(userId);
            return userRoles;
        }

        public IEnumerable<string> GetAllRoles()
        {
            var roles = _authRepo.GetAllRoles();
            return roles;
        }

        public ApplicationUser CreateUser(ApplicationUser user, IEnumerable<string> roles)
        {
            var newUser = _authRepo.CreateUser(user, roles);
            return newUser;
        }

        public void DeleteUser(ApplicationUser user)
        {
            _authRepo.DeleteUser(user);
        }

        public IEnumerable<ApplicationUser> GetAdAccounts(string searchTerm = null)
        {
            var users = new List<ApplicationUser>();
            var numberOfSuggestions = _config.Get<int>(ConfigKeys.AdUsersSuggestionSize);

            using (var directoryEntry = new DirectoryEntry(
                            _config.Get<string>(ConfigKeys.ActiveDirectoryEntryPath),
                            _config.Get<string>(ConfigKeys.ActiveDirectoryUser),
                            _config.Get<string>(ConfigKeys.ActiveDirectoryPassword)))
            {
                using (var searcher = new DirectorySearcher(directoryEntry))
                {
                    searcher.PageSize = 10000;
                    searchTerm = string.IsNullOrWhiteSpace(searchTerm) ? string.Empty : searchTerm + "*";

                    searcher.Filter = string.Format("(&(objectCategory=Person)(objectClass=user)(displayName=*{0})(!displayName=*System*)(!displayName=*Admin*))", searchTerm);

                    searcher.PropertiesToLoad.Add("samAccountName");
                    searcher.PropertiesToLoad.Add("displayName");
                    searcher.PropertiesToLoad.Add("mail");
                    searcher.PropertiesToLoad.Add("givenName");
                    searcher.PropertiesToLoad.Add("sn");

                    var results = searcher.FindAll();

                    for (int i = 0; i < results.Count; i++)
                    {
                        try
                        {
                            if (numberOfSuggestions > 0 && users.Count >= numberOfSuggestions) break;

                            users.Add(new ApplicationUser
                                {
                                    UserName = results[i].Properties["samAccountName"][0].ToString(),
                                    Email = results[i].Properties["mail"][0].ToString(),
                                    Forename = results[i].Properties["givenName"][0].ToString(),
                                    Surname = results[i].Properties["sn"][0].ToString(),
                                    DisplayName = results[i].Properties["displayName"][0].ToString()
                                });
                        }
                        catch (Exception)
                        {
                            //an account might not have email. hence index out of range exception will be throw
                            //safely ignore such accounts
                        }
                    }
                }
            }

            return users;
        }

        public ApplicationUser GetAdDetailsByAccountName(string accountName)
        {
            var user = new ApplicationUser();

            using (var directoryEntry = new DirectoryEntry(
                            _config.Get<string>(ConfigKeys.ActiveDirectoryEntryPath),
                            _config.Get<string>(ConfigKeys.ActiveDirectoryUser),
                            _config.Get<string>(ConfigKeys.ActiveDirectoryPassword)))
            {
                using (var searcher = new DirectorySearcher(directoryEntry))
                {
                    searcher.PageSize = 10000;

                    searcher.Filter = string.Format("(&(objectCategory=Person)(objectClass=user)(SAMAccountname={0})(!displayName=*System*)(!displayName=*Admin*))", accountName);

                    searcher.PropertiesToLoad.Add("samAccountName");
                    searcher.PropertiesToLoad.Add("displayName");
                    searcher.PropertiesToLoad.Add("mail");
                    searcher.PropertiesToLoad.Add("givenName");
                    searcher.PropertiesToLoad.Add("sn");

                    var result = searcher.FindOne();
                    if (result == null) return user;

                    user = new ApplicationUser
                                    {
                                        UserName = result.Properties["samAccountName"][0].ToString(),
                                        Email = result.Properties["mail"][0].ToString(),
                                        Forename = result.Properties["givenName"][0].ToString(),
                                        Surname = result.Properties["sn"][0].ToString(),
                                        DisplayName = result.Properties["displayName"][0].ToString()
                                    };
                }
            }

            return user;
        }

        public ApplicationUser GetAdDetailsByDisplayName(string displayName)
        {
            var user = new ApplicationUser();

            using (var directoryEntry = new DirectoryEntry(
                            _config.Get<string>(ConfigKeys.ActiveDirectoryEntryPath),
                            _config.Get<string>(ConfigKeys.ActiveDirectoryUser),
                            _config.Get<string>(ConfigKeys.ActiveDirectoryPassword)))
            {
                using (var searcher = new DirectorySearcher(directoryEntry))
                {
                    searcher.PageSize = 10000;

                    searcher.Filter = string.Format("(&(objectCategory=Person)(objectClass=user)(displayName={0})(!displayName=*System*)(!displayName=*Admin*))", displayName);

                    searcher.PropertiesToLoad.Add("samAccountName");
                    searcher.PropertiesToLoad.Add("displayName");
                    searcher.PropertiesToLoad.Add("mail");
                    searcher.PropertiesToLoad.Add("givenName");
                    searcher.PropertiesToLoad.Add("sn");

                    var result = searcher.FindOne();
                    if (result == null) return user;

                    user = new ApplicationUser
                                    {
                                        UserName = result.Properties["samAccountName"][0].ToString(),
                                        Email = result.Properties["mail"][0].ToString(),
                                        Forename = result.Properties["givenName"][0].ToString(),
                                        Surname = result.Properties["sn"][0].ToString(),
                                        DisplayName = result.Properties["displayName"][0].ToString()
                                    };
                }
            }

            return user;
        }
    }
}