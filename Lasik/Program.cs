using System.Net.Http;
using Generation;
using Lasik;
using Lasik.Controllers;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers(options => options.InputFormatters.Add(new TextPlainInputFormatter()));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<HttpClient>();
builder.Services.AddScoped<Parser>();
builder.Services.AddScoped<Generator>();

builder.Services.AddCors(options =>
{
    if (builder.Environment.IsProduction())
    {
        options.AddDefaultPolicy(
            builder =>
            {
                builder.WithOrigins("http://lasik.michaelepps.me");
            });
    }
    else
    {
        options.AddDefaultPolicy(builder => builder.AllowAnyOrigin());
    }

});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors();
app.UseAuthorization();
app.MapControllers();
app.Run();