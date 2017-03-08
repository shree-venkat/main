namespace Server.Api.ViewModels
{
    using System.Collections.Generic;

    public class CurrentUserViewModel
    {
        public string Id { get; set; }

        public string Forename { get; set; }

        public string Surname { get; set; }

        public string DisplayName { get; set; }

        public string EmailAddress { get; set; }

        public string Username { get; set; }

        public IEnumerable<string> Roles { get; set; }

        public bool IsAdmin { get; set; }
    }
}