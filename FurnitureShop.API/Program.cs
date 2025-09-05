using FurnitureShop.API.Extensions;
using FurnitureShop.API.Middlewares;
using FurnitureShop.BL;
using FurnitureShop.DAL;
using FurnitureShop.DAL.Context;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseNpgsql(builder.Configuration.GetConnectionString("PostgreSQL"))); 

builder.Services.AddPersistence();
builder.Services.AddCache();
builder.Services.AddServices();
builder.Services.ConfigureCustomApiBehavior();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseMiddleware<ExceptionMiddleware>();
app.UseHttpsRedirection();

app.Run();
