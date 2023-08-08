//1.Usings to work with EntitiFramework
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using university_api_backend;
using university_api_backend.DataAccess;
using university_api_backend.Services;

var builder = WebApplication.CreateBuilder(args);

//2. connection with the sql server express
const string connectionName = "UniversityDB";
var connectionString = builder.Configuration.GetConnectionString(connectionName);

//3. Add context to services builder

builder.Services.AddDbContext<UniversityDbContext>(options => options.UseSqlServer(connectionString));

// 7. add services of jwt authorization

builder.Services.AddJwtTokenServices(builder.Configuration);

// Add services to the container.

builder.Services.AddControllers();

// 4. Add custom services (folder serivces)

builder.Services.AddScoped<IStudentsService, StudentServices>();
// add the rest of services
//8.add authorization policy
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("UserOnlyPolicy", policy => policy.RequireClaim("UserOnly", "User1"));
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

// 9. config swagger to take care to authorization of jwt
builder.Services.AddSwaggerGen(options =>
{
    //we define the security for authorization
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT authorization header using bearer scheme"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {

            {
                new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type= ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                new string[]{}
            }
        });
});

//5. CORS configuration
builder.Services.AddCors(options =>
{
    options.AddPolicy(name:"CorsPolicy", builder =>
    {
        builder.AllowAnyOrigin();
        builder.AllowAnyMethod();
        builder.AllowAnyHeader();
    });
});

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

//6. Tell app to use CORS
app.UseCors("CorsPolicy");

app.Run();
