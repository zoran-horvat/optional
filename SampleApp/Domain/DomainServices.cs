using System.Linq;
using CodingHelmet.SampleApp.Common;
using CodingHelmet.SampleApp.Domain.Interfaces;
using CodingHelmet.SampleApp.Domain.Models;
using CodingHelmet.SampleApp.Domain.ViewModels;
using CodingHelmet.SampleApp.Infrastructure;
using CodingHelmet.SampleApp.Presentation;

namespace CodingHelmet.SampleApp.Domain
{
    class DomainServices
    {

        private UserRepository UserRepository { get; } = new UserRepository();
        private ProductRepository ProductRepository { get; } = new ProductRepository();
        private AccountRepository AccountRepository { get; } = new AccountRepository();

        public void RegisterUser(string userName)
        {
            RegisteredUser user = this.CreateUser(userName);
            this.RegisterUser(user);
        }

        public void RegisterUser(string userName, string referrerName)
        {
            RegisteredUser user = this.CreateUser(userName);
            this.RegisterUser(user);
            this.SetReferrer(user, referrerName);
        }

        private void SetReferrer(RegisteredUser user, string referrerName) =>
            this.UserRepository
                .TryFind(referrerName)
                .Do(referrer => user.SetReferrer(referrer));

        private void RegisterUser(RegisteredUser user)
        {

            this.UserRepository.Add(user);

            TransactionalAccount account = new TransactionalAccount(user);
            this.AccountRepository.Add(account);

        }

        private RegisteredUser CreateUser(string userName) =>
            new RegisteredUser(userName);

        public bool VerifyCredentials(string userName) =>
            this.UserRepository.TryFind(userName).Map(_ => true).Fold(() => false);

        public IPurchaseViewModel Purchase(string userName, string itemName) =>
            this.UserRepository
                .TryFind(userName)
                .Map(user => this.Purchase(user, this.FindAccount(user), itemName))
                .Fold(FailedPurchase.Instance);

        private IAccount FindAccount(RegisteredUser user) =>
            this.AccountRepository.FindByUser(user);

        public IPurchaseViewModel AnonymousPurchase(string itemName) =>
            this.Purchase(new AnonymousBuyer(), new Cash(), itemName);

        private IPurchaseViewModel Purchase(IUser user, IAccount account, string itemName) =>
            this.ProductRepository
                .TryFind(itemName)
                .Map(user.Purchase)
                .Map(receipt => this.Charge(user, account, receipt))
                .Fold(() => new MissingProduct(itemName));

        private IPurchaseViewModel Charge(IUser user, IAccount account, IReceipt receipt) =>
            account
                .TryWithdraw(receipt.Price)
                .Map(trans => (IPurchaseViewModel)receipt)
                .Fold(() => new InsufficientFunds(user.DisplayName, receipt.Price));

        public void Deposit(string userName, decimal amount) =>
            this.UserRepository
                .TryFind(userName)
                .Map(this.FindAccount)
                .Do(account => account.Deposit(amount));
    }
}
