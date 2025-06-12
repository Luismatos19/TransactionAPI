using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using TransactionAPI.Repositories;
using TransactionAPI.Services;
using TransactionAPI.Interfaces;
using Microsoft.OpenApi.Models;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Transaction API",
        Version = "v1",
        Description = "API para gerenciar transações financeiras usando Dapper e SQL Server.",
        Contact = new OpenApiContact
        {
            Name = "Luis Matos",
            Email = "luis.matos1992@gmail.com",
            Url = new Uri("https://github.com/Luismatos19")
        }
    });
});


builder.Services.AddControllers().AddFluentValidation();
builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();
builder.Services.AddScoped<TransactionService>();
builder.Services.AddControllers().ConfigureApiBehaviorOptions(options =>
{
    options.InvalidModelStateResponseFactory = context =>
    {
        return new BadRequestObjectResult(context.ModelState);
    };
});

var app = builder.Build();
app.UseRouting();
app.MapControllers();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Transaction API v1");
        c.RoutePrefix = string.Empty;
    });
}

app.UseHttpsRedirection();



app.Run();

