using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Domain.Exceptions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web.Middlewares
{
    public class GlobalExceptionHandlingMiddleware : IMiddleware
    {
        private readonly ILogger<GlobalExceptionHandlingMiddleware> _logger;
        public GlobalExceptionHandlingMiddleware(ILogger<GlobalExceptionHandlingMiddleware> logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            //HttpContext es el objeto que representa la request y la response actual.
            try
            {
                await next(context);
            }
            catch (NotFoundException ex)
            {
                _logger.LogError(ex, ex.Message); //mensaje para el desarrollador

                int statusCode = (int)HttpStatusCode.NotFound; //me traigo el numero correspondiente a un not found

                ProblemDetails problem = new ProblemDetails()
                {
                    Status = statusCode,
                    Type = "Not Found Error",
                    Title = "Not Found Error",
                    Detail = ex.Message
                };

                string json = JsonSerializer.Serialize(problem); //creacion del json a retornar

                context.Response.ContentType = "application/json";

                await context.Response.WriteAsync(json); //escribe el json en la response del context
            }

            catch (BadRequestException ex)
            {
                _logger.LogError(ex, ex.Message); 

                int statusCode = (int)HttpStatusCode.BadRequest;

                ProblemDetails problem = new ProblemDetails()
                {
                    Status = statusCode,
                    Type = "BadRequest Error",
                    Title = "BadRequest Error",
                    Detail = ex.Message
                };

                string json = JsonSerializer.Serialize(problem);

                context.Response.ContentType = "application/json";

                await context.Response.WriteAsync(json);
            }

            catch (UnauthorizedException ex)
            {
                _logger.LogError(ex, ex.Message);

                int statusCode = (int)HttpStatusCode.Unauthorized;

                ProblemDetails problem = new ProblemDetails()
                {
                    Status = statusCode,
                    Type = "Unauthorized Error",
                    Title = "Unauthorized Error",
                    Detail = ex.Message
                };

                string json = JsonSerializer.Serialize(problem);

                context.Response.ContentType = "application/json";

                await context.Response.WriteAsync(json);
            }
        }
    }
}