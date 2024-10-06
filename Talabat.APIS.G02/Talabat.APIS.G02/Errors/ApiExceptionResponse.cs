namespace Talabat.APIS.G02.Errors
{
    public class ApiExceptionResponse : ApiResponse
    {
        public string? Details { get; set; }

        public ApiExceptionResponse(int StatusCode,string? Message=null,string? _Details=null):base(StatusCode,Message)
        {
            Details = _Details;  
        }
    }
}
