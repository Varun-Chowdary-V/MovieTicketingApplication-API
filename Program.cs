using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using MovieTicketingApplication.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;
using System.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

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
    opt.UseSqlServer(Configuration.GetConnectionString("DbConnection")));

builder.Services.AddDbContext<MovieContext>(opt =>
    opt.UseSqlServer(Configuration.GetConnectionString("DbConnection")));

builder.Services.AddDbContext<TheatreContext>(opt =>
    opt.UseSqlServer(Configuration.GetConnectionString("DbConnection")));

builder.Services.AddDbContext<BookingContext>(opt =>
    opt.UseSqlServer(Configuration.GetConnectionString("DbConnection")));


byte[] key = Encoding.ASCII.GetBytes(Configuration["JWT_KEY"]);

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(x =>
    {
        x.RequireHttpsMetadata = false;
        x.SaveToken = true;
        x.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = Configuration["JWT_ISSUER"],
            ValidAudience = Configuration["JWT_AUDIENCE"],
            IssuerSigningKey = new SymmetricSecurityKey(key)
        };
    });


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
