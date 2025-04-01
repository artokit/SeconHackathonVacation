using DataAccess.Extensions;
using Services.Extensions;
using Services.Users;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRepositories();
builder.Services.AddServices();
builder.Services.AddMigrations(builder.Configuration);
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Services.AddRepositories();
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));

var app = builder.Build();
app.Services.UseMigrations();
app.MapControllers();
app.MapSwagger();
app.UseSwaggerUI();
app.Run();