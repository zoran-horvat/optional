using System.Collections.Generic;

namespace CodingHelmet.SampleApp.Domain.Interfaces
{
    interface IProduct
    {
        decimal Price { get; }
        string Name { get; }
        IProduct ApplyDiscounts(IEnumerable<IDiscount> discounts);
    }
}
