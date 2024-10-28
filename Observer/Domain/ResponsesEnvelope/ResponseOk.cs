namespace Observer.Domain.ResponsesEnvelope
{
    internal class ResponseOk<T> : IResponse<T>
    {
        public bool IsSuccess { get; } = true;

        public T Data { get; } = default!;

        public string Details { get; } = default!;

        public ResponseOk(T data, string details)
        {
            Data = data;
            Details = details;
        }

        public ResponseOk(T data)
        {
            Data = data;
        }
    }
}
