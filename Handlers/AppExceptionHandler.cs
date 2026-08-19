using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using zeiss_api.Exceptions;


namespace zeiss_api.Handlers
{
    public class AppExceptionHandler : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(
            HttpContext httpContext,
            Exception exception,
            CancellationToken cancellationToken
        )
        {
            if (exception is not AppException appException)
            {
                return false;
            }
            httpContext.Response.StatusCode = appException.StatusCode;
            var exceptionDetails = new ProblemDetails
            {
                Status = appException.StatusCode,
                Title = appException.GetType().Name,
                Detail = appException.Message,
            };

            await httpContext.Response.WriteAsJsonAsync(exceptionDetails, cancellationToken);

            return true;
        }
    }
}