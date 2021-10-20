using CodingHelmet.SampleApp.Domain.Interfaces;

namespace CodingHelmet.SampleApp.Domain.ViewModels
{
    class Receipt : IReceipt
    {

        public string Buyer { get; }
        public string ItemName { get; }
        public decimal Price { get; }

        public Receipt(string buyer, string itemName, decimal price)
        {
            this.Buyer = buyer;
            this.ItemName = itemName;
            this.Price = price;
        }

        public string Render()
        {
            return string.Format("{0} -> {1} {2:C}", this.Buyer, this.ItemName, this.Price);
        }

    }
}
