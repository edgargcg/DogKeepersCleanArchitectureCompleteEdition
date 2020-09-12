namespace DogKeepers.Core.Response
{
    public class Response<T>
    {

        public bool IsDone { get; set; } = true;
        public string Message { get; set; } = "";
        public T Data { get; set; }

        public Response()
        {}

        public Response(bool isDone, string message, T data)
        {
            IsDone = isDone;
            Message = message;
            Data = data;
        }

    }
}
