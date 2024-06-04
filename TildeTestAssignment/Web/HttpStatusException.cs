namespace TildeTestAssignment.Web
{
    public class HttpStatusException : Exception
    {
        public int StatusCode { get; }

        public HttpStatusException(string message, int statusCode = StatusCodes.Status400BadRequest)
            : base(message)
        {
            StatusCode = statusCode;
        }
    }
}
