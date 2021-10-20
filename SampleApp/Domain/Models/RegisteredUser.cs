using System.Collections.Generic;
using CodingHelmet.SampleApp.Domain.Interfaces;
using CodingHelmet.SampleApp.Domain.ViewModels;

namespace CodingHelmet.SampleApp.Domain.Models
{
    class RegisteredUser : IUser
    {

        public string UserName { get; }
        public string DisplayName => this.UserName;
        private decimal totalPurchases;
        private bool hasReceivedLoyaltyDiscount;
        private IList<RelativeDiscount> Discounts { get; } = new List<RelativeDiscount>();

        public RegisteredUser(string userName)
        {
            this.UserName = userName;
        }

        public IReceipt Purchase(IProduct item)
        {
            IProduct discountedItem = item.ApplyDiscounts(this.Discounts);
            this.RegisterPurchase(discountedItem.Price);
            return new Receipt(this.UserName, discountedItem.Name, discountedItem.Price);
        }

        public void SetReferrer(RegisteredUser referrer)
        {
            if (referrer != null)
            {
                referrer.ReferralAdded();
            }
        }

        private void RegisterPurchase(decimal price)
        {
            this.totalPurchases += price;
            if (!hasReceivedLoyaltyDiscount && this.totalPurchases > 1000.0M)
            {
                this.Discounts.Add(new RelativeDiscount(0.05M));
                this.hasReceivedLoyaltyDiscount = true;
            }
        }

        private void ReferralAdded()
        {
            this.Discounts.Add(new RelativeDiscount(.02M));
        }

    }
}
