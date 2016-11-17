using System.Collections.Generic;
using CodingHelmet.Optional;
using CodingHelmet.SampleApp.Common;
using CodingHelmet.SampleApp.Domain.Models;

namespace CodingHelmet.SampleApp.Infrastructure
{
    class UserRepository
    {
        private Dictionary<string, RegisteredUser> UserNameToUser { get; } = new Dictionary<string, RegisteredUser>();

        public void Add(RegisteredUser user)
        {
            this.UserNameToUser.Add(user.UserName, user);
        }

        public IOption<RegisteredUser> TryFind(string userName) =>
            this.UserNameToUser.TryGetValue(userName);
    }
}
