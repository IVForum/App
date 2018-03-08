using System.Net;

namespace IVForum.App.Data.Shared
{
	public class Result
    {
		public bool IsSuccess { get; set; }
		public string ErrorMessage { get; set; }
		public HttpStatusCode StatusCode { get; set; }

		public Result(bool isSuccess)
		{
			IsSuccess = isSuccess;
		}

		public Result(HttpStatusCode statusCode)
		{
			StatusCode = statusCode;
		}

		public Result(bool isSuccess, HttpStatusCode statusCode)
		{
			IsSuccess = isSuccess;
			StatusCode = statusCode;
		}

		public Result(string errorMessage)
		{
			ErrorMessage = errorMessage;
		}

		public Result(bool isSuccess, string errorMessage)
		{
			IsSuccess = isSuccess;
			ErrorMessage = errorMessage;
		}

		public Result(bool isSuccess, HttpStatusCode statusCode, string errorMessage)
		{
			IsSuccess = isSuccess;
			StatusCode = statusCode;
			ErrorMessage = errorMessage;
		}

	}
}
