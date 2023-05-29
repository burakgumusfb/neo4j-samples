using Microsoft.EntityFrameworkCore;
using Mini.Social.Media.Persistence;
using Mini.Social.Media.Persistence.Context;
using Mini.Social.Media.Graphql;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

IConfiguration configuration = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
        .Build();

builder.Services.AddControllers();
builder.Services.AddDbContext<MiniSocialMediaDbContext>(option => option.UseSqlServer(configuration.GetConnectionString("MiniSocialMediaDB")));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddPersistenceServices();
builder.Services.AddApplicationServices();
builder.Services.AddGraphqlServices();

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
