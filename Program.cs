using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using MovieTicketingApplication.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Add CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder => builder.WithOrigins("http://localhost:4200")
                          .AllowAnyMethod()
                          .AllowAnyHeader());
});

var Configuration = builder.Configuration;

builder.Services.AddDbContext<UserContext>(opt =>
    opt.UseSqlite(Configuration.GetConnectionString("UsersConnection")));

builder.Services.AddDbContext<MovieContext>(opt =>
    opt.UseSqlite(Configuration.GetConnectionString("MoviesConnection")));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
// Enable CORS
app.UseCors("AllowSpecificOrigin");

app.MapControllers();

app.Run();
