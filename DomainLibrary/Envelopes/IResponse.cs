namespace DomainLibrary.Envelopes
{
    public interface IResponse<T>
    {
        bool IsSuccess { get; }
        T Data { get; }
        string Details { get; }
    }
}
