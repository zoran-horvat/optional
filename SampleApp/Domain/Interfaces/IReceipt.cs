using CodingHelmet.SampleApp.Presentation;

namespace CodingHelmet.SampleApp.Domain.Interfaces
{
    interface IReceipt : IPurchaseViewModel
    {
        string ItemName { get; }
        decimal Price { get; }
    }
}
