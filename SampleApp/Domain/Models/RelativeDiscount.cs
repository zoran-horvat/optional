using CodingHelmet.SampleApp.Domain.Interfaces;

namespace CodingHelmet.SampleApp.Domain.Models
{
    class RelativeDiscount : IDiscount
    {

        private decimal Rate { get; }

        public RelativeDiscount(decimal rate)
        {
            this.Rate = rate;
        }

        public decimal Apply(decimal price)
        {
            return price * (1.0M - this.Rate);
        }
    }
}
