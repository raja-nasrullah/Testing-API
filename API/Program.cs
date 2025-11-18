using API.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var constring = builder.Configuration.GetConnectionString("ConString");

builder.Services.AddDbContext<AddDbContext>(options => options.UseSqlServer(constring));

builder.Services.AddCors(options => options.AddPolicy("BlazorYTPolicy", policyBuilder =>
{
    policyBuilder.WithOrigins("https://localhost:7070");
    policyBuilder.AllowAnyHeader();
    policyBuilder.AllowAnyMethod();
}));

var app = builder.Build();




// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("BlazorYTPolicy");
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
