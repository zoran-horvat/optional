using System;
using System.Collections.Generic;
using System.IO;
using CodingHelmet.SampleApp.Application.ViewModels;
using CodingHelmet.SampleApp.Domain;
using CodingHelmet.SampleApp.Presentation;

namespace CodingHelmet.SampleApp.Application
{
    class ApplicationServices
    {

        private Dictionary<string, string> IisSession { get; } = new Dictionary<string, string>();
        private DomainServices Domain { get; } = new DomainServices();

        public void RegisterUser(string userName) =>
            this.RegisterUser(userName, () => this.Domain.RegisterUser(userName));

        public void RegisterUser(string userName, string referrerName) =>
            this.RegisterUser(userName, () => this.Domain.RegisterUser(userName, referrerName));

        private void RegisterUser(string userName, Action domainRegister)
        {

            if (IsDownForMaintenance())
                return;

            domainRegister();

            this.IisSession["logged-in-user"] = userName;

        }

        public void Login(string userName)
        {

            if (IsDownForMaintenance())
                return;

            if (this.Domain.VerifyCredentials(userName))
                this.IisSession["logged-in-user"] = userName;
            else
                this.IisSession["logged-in-user"] = null;

        }

        public IPurchaseViewModel Purchase(string itemName)
        {
            if (IsDownForMaintenance())
                return new Downtime();
            return this.Domain.Purchase(this.IisSession["logged-in-user"], itemName);
        }

        public IPurchaseViewModel AnonymousPurchase(string itemName)
        {
            if (IsDownForMaintenance())
                return new Downtime();
            return this.Domain.AnonymousPurchase(itemName);
        }

        public void Deposit(decimal amount)
        {
            if (IsDownForMaintenance())
                return;
            this.Domain.Deposit(this.IisSession["logged-in-user"], amount);
        }

        private bool IsDownForMaintenance() => File.Exists("maintenance.lock");

    }
}
