namespace Server.Repo.Contexts
{
    using System.Linq;
    using System.Net.Http;
    using System.Security.Claims;
    using System.Web;

    public class UserContext
    {
        /*public string GetUserName()
        {
            if (HttpContent.Current == null)
                return "System Import";

            var currentUser = (ClaimsPrincipal)HttpContext.Current.User;
            string name = string.Empty;

            if (currentUser != null && currentUser.Claims.Any(w => w.Type == ClaimTypes.Name))
            {
                var firstOrDefault = currentUser.Claims.FirstOrDefault(w => w.Type == ClaimTypes.Name);
                if (firstOrDefault != null)
                    name = firstOrDefault.Value;
            }

            return name ?? "System";
        }

        public string GetUserId()
        {
            if (HttpContext.Current == null)
                return null;

            var currentUser = (ClaimsPrincipal)HttpContext.Current.User;
            string id = string.Empty;

            if (currentUser != null && currentUser.Claims.Any(w => w.Type == "UserId"))
            {
                var firstOrDefault = currentUser.Claims.FirstOrDefault(w => w.Type == "UserId");
                if (firstOrDefault != null)
                    id = firstOrDefault.Value;
            }

            return id;
        }

        public bool IsInRole(string role)
        {
            return HttpContext.Current.User.IsInRole(role);
        }*/
    }
}
