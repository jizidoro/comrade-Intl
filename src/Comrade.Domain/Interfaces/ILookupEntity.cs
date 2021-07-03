namespace Comrade.Domain.Interfaces
{
    public interface ILookupEntity
    {
        int Key { get; set; }
        string Value { get; set; }
    }
}