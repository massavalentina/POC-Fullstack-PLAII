using Application.Exceptions;
using Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace Filters
{
    /// <summary>
    /// Clase base para el manejo de excepciones. Modificar solo si es requerido
    /// </summary>
    public class BaseExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            //generamos el mensaje de error
            HttpResponse response = context.HttpContext.Response;
            response.StatusCode = (int)GetErrorCode(context.Exception.GetType());
            response.ContentType = "application/json";

            string resultMessage = context.Exception.Message;
            string errorCode = Guid.NewGuid().ToString();

            var result = new ObjectResult(new HttpMessageResult()
            {
                Success = false,
                Data = string.Empty,
                Message = IsManagedException(context.Exception) ? resultMessage : "His operation could not be completed. try again later. Error Code (" + errorCode + ").",
                Code = errorCode,
                StatusCode = response.StatusCode
            });
            context.Result = result;
        }

        private static readonly Dictionary<Type, HttpStatusCode> ExceptionStatusMap = new()
        {
            [typeof(EntityDoesNotExistException)] = HttpStatusCode.NotFound,
            [typeof(BussinessException)] = HttpStatusCode.BadRequest,
            [typeof(EntityDoesExistException)] = HttpStatusCode.BadRequest,
            [typeof(InvalidEntityDataException)] = HttpStatusCode.BadRequest,
            [typeof(ArgumentNullException)] = HttpStatusCode.LengthRequired,
        };

        private HttpStatusCode GetErrorCode(Type exceptionType)
        {
            if (ExceptionStatusMap.TryGetValue(exceptionType, out var statusCode))
                return statusCode;

            if (typeof(ApplicationException).IsAssignableFrom(exceptionType))
                return HttpStatusCode.BadRequest;

            return HttpStatusCode.InternalServerError;
        }

        private bool IsManagedException(Exception ex)
        {
            return ex is ApplicationException;
        }
    }

    public enum Exceptions
    {
        ArgumentNullException,
        BussinessException,
        EntityDoesExistException,
        EntityDoesNotExistException,
        InvalidEntityDataException
    }
}
