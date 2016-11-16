using CodingHelmet.SampleApp.Presentation;

namespace CodingHelmet.SampleApp.Domain.ViewModels
{
    class MissingProduct : IPurchaseViewModel
    {
        private string ProductName { get; }

        public MissingProduct(string productName)
        {
            this.ProductName = productName;
        }

        public string Render()
        {
            return string.Format("Product {0} is not available.",
                                 this.ProductName);
        }
    }
}
