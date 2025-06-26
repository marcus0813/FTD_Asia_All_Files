using log4net;

namespace FTD_Asia_API.Middleware
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILog _logger;

        public LoggingMiddleware(RequestDelegate next)
        {
            _next = next;
            _logger = LogManager.GetLogger(typeof(LoggingMiddleware));
        }

        public async Task Invoke(HttpContext context)
        {
            context.Request.EnableBuffering();

            using var reader = new StreamReader(context.Request.Body, leaveOpen: true);
            string body = await reader.ReadToEndAsync();
            context.Request.Body.Position = 0;

            _logger.Info($"Logging Request: {context.Request.Method} {context.Request.Path}\nBody:\n{body}");

            var originalBodyStream = context.Response.Body;
            using var responseBody = new MemoryStream();
            context.Response.Body = responseBody;

            await _next(context);

            context.Response.Body.Seek(0, SeekOrigin.Begin);
            string responseText = await new StreamReader(context.Response.Body).ReadToEndAsync();
            context.Response.Body.Seek(0, SeekOrigin.Begin);

            _logger.Info($"Logging Response: {context.Response.StatusCode} for {context.Request.Path}\nBody:\n{responseText}");

            await responseBody.CopyToAsync(originalBodyStream);
        }
    }
}
