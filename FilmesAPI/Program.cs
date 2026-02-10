using Microsoft.EntityFrameworkCore;
using FilmesAPI.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<FilmeContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("FilmeConnection"),
        ServerVersion.AutoDetect(
            builder.Configuration.GetConnectionString("FilmeConnection")
        )
    )
);

// Add services to the container.

builder.Services.AddControllers();
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

app.MapControllers();

app.Run();
