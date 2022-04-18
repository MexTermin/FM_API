namespace FMAPI.Helpers
{
    public class ResponseHelper<T>
    {
        public string Message { get; set; }
        public bool Error { get; set; }
        public T? Body { get; set; }

        public ResponseHelper(string message, T? body, bool error = false)
        {
            Message = message;
            Error = error;
            Body = body;
        }
    }

    public class ResponseHelper
    {
        public string Message { get; set; }
        public bool Error { get; set; }

        public ResponseHelper(string message, bool error = false)
        {
            Message = message;
            Error = error;
        }
    }
}
