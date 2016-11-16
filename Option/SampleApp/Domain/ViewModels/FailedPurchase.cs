using CodingHelmet.SampleApp.Presentation;

namespace CodingHelmet.SampleApp.Domain.ViewModels
{
    class FailedPurchase : IPurchaseViewModel
    {

        private static FailedPurchase instance;

        public static FailedPurchase Instance
        {
            get
            {
                if (instance == null)
                    instance = new FailedPurchase();
                return instance;
            }
        }

        private FailedPurchase() { }

        public string Render()
        {
            return "Purchase failed.";
        }
    }
}
