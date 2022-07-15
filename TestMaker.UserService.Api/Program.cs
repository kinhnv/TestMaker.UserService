using AspNetCore.Environment.Extensions;
using Microsoft.EntityFrameworkCore;
using TestMaker.Common.Extensions;
using TestMaker.UserService.Infrastructure.Entities;
using TestMaker.UserService.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args)
    .AddACS();

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "AllowAll",
        builder => builder.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod());
});
builder.Services.AddDbContext<ApplicationDbContext>(optionsBuilder =>
{
    optionsBuilder.UseSqlServer(builder.Configuration["Mssql:ConnectionString"]);
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient();

// Add Bearer Authentication
builder.Services.AddBearerAuthentication(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAll");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
