using System.Diagnostics;
using Application.Commands;
using Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure Database Context and Dependency Injection
builder.Services.AddInfrastructure(builder.Configuration);

// Register MediatR with the correct method
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateClientCommand).Assembly));

// Build the application
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    Task.Run(() =>
    {
        var frontendPath = Path.Combine(Directory.GetCurrentDirectory(), "clientapp"); 
        var startInfo = new ProcessStartInfo("cmd", "/c npm install && npm start")
        {
            WorkingDirectory = frontendPath,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };

        var process = Process.Start(startInfo);

        process.OutputDataReceived += (sender, args) => Console.WriteLine(args.Data);
        process.ErrorDataReceived += (sender, args) => Console.WriteLine(args.Data);

        process.BeginOutputReadLine();
        process.BeginErrorReadLine();
    });
}
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.UseStaticFiles();

app.Run();