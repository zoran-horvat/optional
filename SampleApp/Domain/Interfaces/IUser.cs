namespace CodingHelmet.SampleApp.Domain.Interfaces
{
    interface IUser
    {
        string DisplayName { get; }
        IReceipt Purchase(IProduct item);
    }
}
