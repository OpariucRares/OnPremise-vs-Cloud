using Microsoft.EntityFrameworkCore;
using ProductService.Contracts;
using ProductService.Data;
using DotNetEnv;

Env.Load();
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddMvc();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

//database
builder.Services.AddScoped<IProductDbContext, ProductDbContext>();

//MS Visual Studio
//var connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING_MS_VISUAL_STUDIO");

//Docker
var connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING_DOCKER");

builder.Services.AddDbContext<ProductDbContext>(options =>
    options.UseNpgsql(connectionString));

//repository
builder.Services.AddScoped<IProductRepository, ProductRepository>();

//API

//CORS
string[] allowedCorsHosts = new[] { "*" };
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policyBuilder =>
        {
            policyBuilder.WithOrigins(allowedCorsHosts);
            policyBuilder.AllowAnyHeader();
            policyBuilder.AllowAnyMethod();
        });
});
builder.Services.AddControllers();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ProductDbContext>();
    context.Database.EnsureCreated();
}


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseHttpsRedirection();

app.UseCors();

app.MapControllers();

app.Run();