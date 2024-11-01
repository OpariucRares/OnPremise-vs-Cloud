using Microsoft.EntityFrameworkCore;
using OrderService.Contracts;
using OrderService.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddMvc();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

//database
builder.Services.AddScoped<IOrderDbContext, OrderDbContext>();
builder.Services.AddDbContext<OrderDbContext>(options =>
    options.UseInMemoryDatabase("OrderDb")); 

//repository
builder.Services.AddScoped<IOrderRepository, OrderRepository>();

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
    var context = scope.ServiceProvider.GetRequiredService<OrderDbContext>();
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