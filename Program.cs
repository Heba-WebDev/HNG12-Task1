using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using System.Diagnostics;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(opt =>
{
    opt.AddPolicy("AllowAll",
        policy => policy.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader()
    );
    opt.AddPolicy("AllowTelex", policy => 
        policy.AllowAnyMethod()
              .AllowAnyHeader()
              .WithOrigins(
                  "https://telex.im",
                  "https://*.telex.im"
              )
              .AllowCredentials()
    );
});

builder.Services.AddHealthChecks()
    .AddCheck<SystemHealthCheck>("system_health_check");

builder.Services.AddHttpClient();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapHealthChecks("/tick", new HealthCheckOptions
{
    ResponseWriter = async (context, report) =>
    {
        var currentProcess = Process.GetCurrentProcess();
        var cpuUsage = currentProcess.TotalProcessorTime.TotalSeconds;
        var ramUsageMB = currentProcess.WorkingSet64 / 1024 / 1024;

        var healthData = new
        {
            status = report.Status.ToString(),
            checks = report.Entries.Select(e => new
            {
                name = e.Key,
                status = e.Value.Status.ToString(),
                description = e.Value.Description,
                data = e.Value.Data
            }),
            totalCpuUsage = cpuUsage,
            workingSet64 = ramUsageMB // in MB
        };
        
        var result = System.Text.Json.JsonSerializer.Serialize(healthData);
        context.Response.ContentType = "application/json";
        await context.Response.WriteAsync(result);
        
        try 
        {
            var httpClientFactory = context.RequestServices.GetRequiredService<IHttpClientFactory>();
            var httpClient = httpClientFactory.CreateClient();
            
            var webhookPayload = new
            {
                event_name = "ASP .NET Core Health Check",
                message = $"Health status: {report.Status}\n RAM usage: {ramUsageMB} MB\n CPU usage: {cpuUsage.ToString("F2")} seconds",
                status = "success",
                username = "system"
            };
            
            var webhookContent = new StringContent(
                System.Text.Json.JsonSerializer.Serialize(webhookPayload),
                Encoding.UTF8,
                "application/json");
                
            var webhookUrl = "https://ping.telex.im/v1/webhooks/01952349-b3fe-78f9-865d-811573d50330";
            await httpClient.PostAsync(webhookUrl, webhookContent);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to send webhook: {ex.Message}");
        }
    }
});

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
