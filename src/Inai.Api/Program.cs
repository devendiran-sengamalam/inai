using Microsoft.EntityFrameworkCore;
using Inai.Api.Data;
using Inai.Api.Services;
using Inai.Api.Endpoints;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<InaiDbContext>(options =>
    options.UseSqlite("Data Source=inai.db"));

builder.Services.AddScoped<TaskService>();
builder.Services.AddScoped<ReminderService>();
builder.Services.AddScoped<ChatService>();
builder.Services.AddScoped<PaymentService>();
builder.Services.AddScoped<HealthService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/", () => "Inai API running!");

// Map endpoints
app.MapTasks();
app.MapReminders();
app.MapChat();
app.MapPayments();
app.MapHealth();

app.Run();
