namespace CodingHelmet.SampleApp.Domain.Interfaces
{
    interface IDiscount
    {
        decimal Apply(decimal price);
    }
}
