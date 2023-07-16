//1.Usings to work with EntitiFramework
using Microsoft.EntityFrameworkCore;
using university_api_backend.DataAccess;


var builder = WebApplication.CreateBuilder(args);

//2. connection with the sql server express
const string connectionName = "UniversityDB";
var connectionString = builder.Configuration.GetConnectionString(connectionName);

//3. Add context to services builder

builder.Services.AddDbContext<UniversityDbContext>(options => options.UseSqlServer(connectionString));

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
