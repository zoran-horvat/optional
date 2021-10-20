using CodingHelmet.Optional;

namespace CodingHelmet.SampleApp.Domain.Interfaces
{
    interface IAccount
    {
        MoneyTransaction Deposit(decimal amount);
        IOption<MoneyTransaction> TryWithdraw(decimal amount);
    }
}
