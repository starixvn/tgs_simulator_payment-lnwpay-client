using Microsoft.AspNetCore.HttpLogging;
using System.Net;
using System.Net.Mime;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services
 .AddHttpLogging(logging =>
{
    logging.LoggingFields = HttpLoggingFields.All;
    logging.MediaTypeOptions.AddText(MediaTypeNames.Application.FormUrlEncoded);
    logging.MediaTypeOptions.AddText(MediaTypeNames.Application.Json);
    logging.MediaTypeOptions.AddText(MediaTypeNames.Text.Html);
    logging.MediaTypeOptions.AddText(MediaTypeNames.Multipart.FormData);
})
  .AddHealthChecks();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseWhen(context => !(context.Request.Path.StartsWithSegments("/healthz") && context.Response.StatusCode == ((int)HttpStatusCode.OK)), app => app.UseHttpLogging());
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapHealthChecks("/healthz");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Use(async (context, next) =>
{
    await next();

    var logger = context.RequestServices.GetService<ILoggerFactory>()
        ?.CreateLogger("Microsoft.AspNetCore.HttpLogging.HttpLoggingMiddleware");

    if (logger?.IsEnabled(LogLevel.Information) == true)
    {
        using var stream = new StreamReader(context.Request.Body);

        _ = await stream.ReadToEndAsync().ConfigureAwait(false);
    }
});


await app.RunAsync();
