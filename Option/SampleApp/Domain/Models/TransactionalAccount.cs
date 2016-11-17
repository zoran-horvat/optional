using System;
using System.Collections.Generic;
using System.Linq;
using CodingHelmet.Optional;
using CodingHelmet.SampleApp.Domain.Interfaces;

namespace CodingHelmet.SampleApp.Domain.Models
{
    class TransactionalAccount : IAccount
    {

        private RegisteredUser User { get; }
        private IList<MoneyTransaction> Transactions { get; } = new List<MoneyTransaction>();

        public TransactionalAccount(RegisteredUser user)
        {
            this.User = user;
        }

        public MoneyTransaction Deposit(decimal amount)
        {

            MoneyTransaction transaction = new MoneyTransaction(amount);
            this.Transactions.Add(transaction);

            Log(string.Format($"{this.UserName} deposited {amount:C} balance {this.Balance:C}"));

            return transaction;

        }

        public IOption<MoneyTransaction> TryWithdraw(decimal amount)
        {
            if (this.Balance < amount)
                return Option.None<MoneyTransaction>();

            MoneyTransaction transaction = new MoneyTransaction(-amount);
            this.Transactions.Add(transaction);

            this.Log(string.Format($"{this.UserName} withdrew {amount:C} balance {this.Balance:C}"));

            return Option.Some(transaction);
        }

        public string UserName => this.User.UserName;

        private decimal Balance =>
            this.Transactions
                .Select(tran => tran.Amount)
                .DefaultIfEmpty(0.0M).Sum();

        private void Log(string message)
        {
            ConsoleColor color = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("\tLOG ===> {0}", message);
            Console.ForegroundColor = color;
        }

    }
}
