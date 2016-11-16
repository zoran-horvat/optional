namespace CodingHelmet.SampleApp.Domain.Interfaces
{
    class MoneyTransaction
    {
        public decimal Amount { get; }

        public MoneyTransaction(decimal amount)
        {
            this.Amount = amount;
        }
    }
}
