namespace Comrade.Core.Helpers.Models.Interfaces
{
    public interface IResult
    {
        bool Success { get; set; }
        int Code { get; set; }
        string? Message { get; set; }
    }
}