using FurnitureShop.API.Extensions;
using FurnitureShop.API.Middlewares;
using FurnitureShop.BL;
using FurnitureShop.DAL;
using FurnitureShop.DAL.Context;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddControllers(); 
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseNpgsql(builder.Configuration.GetConnectionString("PostgreSQL"))); 

builder.Services.AddPersistence();
builder.Services.AddCache();
builder.Services.AddServices();
builder.Services.AddMapper(); 
builder.Services.ConfigureCustomApiBehavior();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseDeveloperExceptionPage();
app.UseHttpsRedirection();
app.UseMiddleware<ExceptionMiddleware>();
app.UseAuthorization();

app.MapControllers();
app.Run();
