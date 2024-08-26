using Npgsql;
using UserService.Filters;
using UserService.Repository;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.

builder.Services.AddMvc(
    config =>
    {
        config.Filters.Add(typeof(ExceptionHandler));
    }
 );
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Add services to the container.
builder.Services.AddControllers();
var connectionString = builder.Configuration.GetConnectionString("PostgreDB");

builder.Services.AddScoped((provider) => new NpgsqlConnection(connectionString));
builder.Services.AddScoped<Iuser, User>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.MapControllers();
app.Run();


