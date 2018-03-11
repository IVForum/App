using System.Net;

namespace IVForum.App.Data.Shared
{
	public class HttpResult : Result
    {
		public HttpStatusCode StatusCode { get; set; }

		public HttpResult(bool isSuccess)
		{
			IsSuccess = isSuccess;
		}

		public HttpResult(HttpStatusCode statusCode)
		{
			StatusCode = statusCode;
		}

		public HttpResult(bool isSuccess, HttpStatusCode statusCode)
		{
			IsSuccess = isSuccess;
			StatusCode = statusCode;
		}

		public HttpResult(string message)
		{
			Message = message;
		}

		public HttpResult(bool isSuccess, string message)
		{
			IsSuccess = isSuccess;
			Message = message;
		}

		public HttpResult(bool isSuccess, HttpStatusCode statusCode, string message)
		{
			IsSuccess = isSuccess;
			StatusCode = statusCode;
			Message = message;
		}
	}
}
