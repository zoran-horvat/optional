using CodingHelmet.Optional;
using CodingHelmet.SampleApp.Domain.Models;

namespace CodingHelmet.SampleApp.Infrastructure
{
    class ProductRepository
    {
        public IOption<Product> TryFind(string itemName)
        {
            if (itemName.Length >= 9)
                return Option.None<Product>();
            return Option.Some(new Product(itemName));
        }
    }
}
