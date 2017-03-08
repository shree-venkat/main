using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using Microsoft.Practices.Unity;
using Server.Business.User.Interfaces;
using Server.Business.Common;

namespace Server.Api.Helpers
{
    public class AdminAuthorizeAttribute : AuthorizeAttribute
    {
        private readonly IUserOps _user;

        public AdminAuthorizeAttribute()
        {
            var unityContainer = UnityConfig.GetConfiguredContainer();
            _user = unityContainer.Resolve<IUserOps>();
        }

        public override void OnAuthorization(HttpActionContext actionContext)
        {
            var valid = false;
            var user = _user.GetUserByAdAccount(HttpContext.Current.User.Identity.Name.ToLower().Replace("willmottdixon\\", string.Empty));
            if (user != null)
            {
                var userRoles = _user.GetUserRoles(user.Id);
                valid = userRoles.Any(a => a.Equals(Model.Constants.Roles.Administrator));
            }

            if (!valid)
            {
                actionContext.Response = new HttpResponseMessage(System.Net.HttpStatusCode.Forbidden);
            }
        }
    }
}