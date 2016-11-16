using CodingHelmet.SampleApp.Presentation;

namespace CodingHelmet.SampleApp.Application.ViewModels
{
    class Downtime : IPurchaseViewModel
    {
        public string Render()
        {
            return "Application is down for maintenance, please visit us later.";
        }
    }
}
