using MediatR;
using Microsoft.Extensions.Logging;
using Serilog;

namespace DentalApplication.Behavior
{
    public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;

        public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
        {
            _logger = logger;
        }

        public async Task<TResponse> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            // Log the request type and details
            Log.Information("Request {RequestName} made : {@Request}", typeof(TRequest).Name, request);

            // Process the next behavior/handler
            var response = await next();

            // Log the response
            Log.Information("Handled {RequestName} sucessfuly", typeof(TRequest).Name);

            return response;
        }
    }
}
