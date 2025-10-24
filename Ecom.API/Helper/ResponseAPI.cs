namespace Ecom.API.Helper
{
    public class ResponseAPI
    {
        public int StatusCode { get; set; }
        public string? Msg { get; set; }
        public ResponseAPI(int statusCode, string msg = null)
        {
            StatusCode = statusCode;
            Msg = msg ?? GetMessageFormStatusCode(StatusCode);
        }
        private string GetMessageFormStatusCode(int statuscode)
        {
            return statuscode switch
            {
                200 => "Done",
                400 => "Bad Request",
                401 => "Un Authorized",
                404 => "resource not found",
                500 => "server Error",
                _ => null,
            };
        }
    }
}
