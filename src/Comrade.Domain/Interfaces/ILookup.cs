namespace Comrade.Domain.Interfaces
{
    public interface ILookup
    {
        int Key { get; set; }
        string? Value { get; set; }
    }
}