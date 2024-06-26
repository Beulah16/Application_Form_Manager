using ApplicationFormManager.ApplicationFormManager.Data;
using ApplicationFormManager.ApplicationFormManager.Interfaces;
using ApplicationFormManager.ApplicationFormManager.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var configuration = builder.Configuration;

// Configure Cosmos DB context
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseCosmos(
        configuration["CosmosConnectionStrings:Url"],
        configuration["CosmosConnectionStrings:Key"],
        configuration["CosmosConnectionStrings:TestDB"]
    ));

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