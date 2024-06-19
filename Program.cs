using ApplicationFormManager.Data;
using ApplicationFormManager.Interfaces;
using ApplicationFormManager.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("CosmosDb")
    ?? throw new InvalidOperationException("Connection string 'CosmosDb' not found.");


builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseCosmos(connectionString, "ApplicationFormDb"));

builder.Services.AddScoped<IApplicationFormService, ApplicationFormService>();
builder.Services.AddScoped<ISubmissionService, SubmissionService>();

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();