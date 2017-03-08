using Server.Api.Helpers;
using Server.Api.ViewModels;
using Server.Business.User.Interfaces;
using Server.Model;
using Server.Model.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web.Http;

namespace Web.Api.Controllers
{
    public class UserController : ApiController
    {
        private readonly IUserOps _user;

        public UserController(IUserOps user)
        {
            _user = user;
        }

        [Route("User/GetCurrentUser")]
        public IHttpActionResult GetCurrentUser()
        {
            try
            {
                var userPrincipal = new ClaimsPrincipal(User);

                var accountName = userPrincipal.Claims.FirstOrDefault(f => f.Type == ClaimTypes.Name).Value;
                accountName = accountName.ToLower().Replace("willmottdixon\\", string.Empty);

                var user = _user.GetUserByAdAccount(accountName);
                if (user == null) //User does not Admin have permissions
                {
                    user = _user.GetAdDetailsByAccountName(accountName);
                    var userViewModel = new CurrentUserViewModel
                    {
                        EmailAddress = user.Email,
                        Forename = user.Forename,
                        Surname = user.Surname,
                        DisplayName = user.DisplayName,
                        IsAdmin = false,
                        Username = user.UserName
                    };

                    return Ok(userViewModel);
                }

                var userRoles = _user.GetUserRoles(user.Id).ToList();
                var adminViewModel = new CurrentUserViewModel
                {
                    Id = user.Id,
                    EmailAddress = user.Email,
                    Forename = user.Forename,
                    Surname = user.Surname,
                    DisplayName = (_user.GetAdDetailsByAccountName(accountName) ?? new ApplicationUser()).DisplayName,
                    IsAdmin = userRoles.Any(a => a.Equals(Roles.Administrator, StringComparison.CurrentCultureIgnoreCase)),
                    Roles = userRoles,
                    Username = user.UserName
                };

                return Ok(adminViewModel);
            }
            catch (Exception ex)
            {
                return WebApiErrorHandler.Throw(ex);
            }
        }

        [AdminAuthorize]
        [Route("User/GetUsers")]
        public IHttpActionResult GetUsers(int page = 1, int pageSize = 10, string searchTerm = null)
        {
            try
            {
                var users = _user.GetAllUsers().ToList();

                if (!string.IsNullOrEmpty(searchTerm))
                {
                    users = users.Where(x => x.Surname.StartsWith(searchTerm, StringComparison.CurrentCultureIgnoreCase) || x.Forename.StartsWith(searchTerm, StringComparison.CurrentCultureIgnoreCase) || x.Email.StartsWith(searchTerm, StringComparison.CurrentCultureIgnoreCase)).ToList();
                }

                // Prepare paged items before hand as we don't want to convert all users to userViewModels and then paginate.
                var pagedUsers = new PagedCollection().GetPagedCollection(users.OrderBy(x => x.Forename).ThenBy(x => x.Surname), page, pageSize);

                var userViewModelList = new List<CurrentUserViewModel>();
                foreach (var user in pagedUsers.Items)
                {
                    var displayName = (_user.GetAdDetailsByAccountName(user.UserName) ?? new ApplicationUser()).DisplayName;
                    var userRoles = _user.GetUserRoles(user.Id).ToList();
                    userViewModelList.Add(new CurrentUserViewModel
                    {
                        EmailAddress = user.Email,
                        Forename = user.Forename,
                        Surname = user.Surname,
                        DisplayName = displayName,
                        Id = user.Id,
                        IsAdmin = userRoles.Any(a => a.Equals(Roles.Administrator, StringComparison.CurrentCultureIgnoreCase)),
                        Roles = userRoles,
                        Username = user.UserName
                    });
                }

                var viewModel = new PagedCollectionViewModel<CurrentUserViewModel>
                {
                    Items = userViewModelList,
                    Page = pagedUsers.Page,
                    TotalCount = pagedUsers.TotalCount,
                    TotalPages = pagedUsers.TotalPages
                };

                return Ok(viewModel);
            }
            catch (Exception ex)
            {
                return WebApiErrorHandler.Throw(ex);
            }
        }

        [AdminAuthorize]
        [Route("User/Add")]
        public IHttpActionResult PostUser(CurrentUserViewModel userViewModel)
        {
            try
            {
                var userExists = _user.GetUserByAdAccount(userViewModel.Username);
                if (userExists != null) return Ok(userExists);

                var user = new ApplicationUser()
                {
                    UserName = userViewModel.Username,
                    Forename = userViewModel.Forename,
                    Surname = userViewModel.Surname,
                    Email = userViewModel.EmailAddress,
                    EmailConfirmed = true,
                    IsActive = true
                };

                var newUser = _user.CreateUser(user, _user.GetAllRoles());
                var newUserRoles = _user.GetUserRoles(newUser.Id) ?? new List<string>();

                var newUserViewModel = new CurrentUserViewModel
                {
                    EmailAddress = newUser.Email,
                    Forename = newUser.Forename,
                    Surname = newUser.Surname,
                    Id = user.Id,
                    IsAdmin = newUserRoles.Any(a => a.Equals(Roles.Administrator, StringComparison.CurrentCultureIgnoreCase)),
                    Username = newUser.UserName
                };

                return Ok(newUserViewModel);
            }
            catch (Exception ex)
            {
                return WebApiErrorHandler.Throw(ex);
            }
        }

        [AdminAuthorize]
        [Route("api/User/Delete")]
        public IHttpActionResult DeleteUser(string username)
        {
            try
            {
                var existingUser = _user.GetUserByAdAccount(username);
                if (existingUser == null) throw new Exception("User does not exist.");

                _user.DeleteUser(existingUser);
                return Ok();
            }
            catch (Exception ex)
            {
                return WebApiErrorHandler.Throw(ex);
            }
        }

        [Route("api/User/GetADAccounts")]
        public IHttpActionResult GetAdAccounts(string searchTerm = null)
        {
            try
            {
                var userViewModel = new List<CurrentUserViewModel>();
                var users = _user.GetAdAccounts(searchTerm).ToList();

                foreach (var user in users)
                {
                    userViewModel.Add(new CurrentUserViewModel
                    {
                        EmailAddress = user.Email,
                        Forename = user.Forename,
                        Surname = user.Surname,
                        DisplayName = user.DisplayName,
                        Id = user.Id,
                        IsAdmin = false,
                        Username = user.UserName
                    });
                }

                return Ok(userViewModel);
            }
            catch (Exception ex)
            {
                return WebApiErrorHandler.Throw(ex);
            }
        }
    }
}
