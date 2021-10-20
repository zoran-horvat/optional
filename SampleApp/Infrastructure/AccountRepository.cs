using System.Collections.Generic;
using CodingHelmet.SampleApp.Domain.Models;

namespace CodingHelmet.SampleApp.Infrastructure
{
    class AccountRepository
    {
        private Dictionary<string, TransactionalAccount> UserNameToAccount { get; } = new Dictionary<string, TransactionalAccount>();

        public void Add(TransactionalAccount account)
        {
            this.UserNameToAccount.Add(account.UserName, account);
        }

        public TransactionalAccount FindByUser(RegisteredUser user)
        {
            return this.UserNameToAccount[user.UserName];
        }
    }
}
