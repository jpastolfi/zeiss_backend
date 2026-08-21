using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using zeiss_api.Data;
using zeiss_api.DTOs;
using zeiss_api.Handlers;
using zeiss_api.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddExceptionHandler<AppExceptionHandler>();
builder.Services.AddProblemDetails();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddControllers();

var app = builder.Build();

app.UseExceptionHandler();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}
app.UseHttpsRedirection();
app.MapControllers();
app.Run();
