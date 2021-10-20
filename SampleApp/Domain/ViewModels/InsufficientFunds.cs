using CodingHelmet.SampleApp.Presentation;

namespace CodingHelmet.SampleApp.Domain.ViewModels
{
    class InsufficientFunds : IPurchaseViewModel
    {
        private string UserName { get; }
        private decimal Price { get; }

        public InsufficientFunds(string userName, decimal price)
        {
            this.UserName = userName;
            this.Price = price;
        }

        public string Render()
        {
            return string.Format("Dear {0}, you don't have {1:C} available in your account.",
                                 this.UserName, this.Price);
        }
    }
}
