namespace WebApplication2.Responses
{
    public class ResponseData<T>
    {
        public string Message { get; set; }
        public T Data { get; set; }

        public ResponseData(string message, T data)
        {
            Message = message;
            Data = data;
        }
    }
}
