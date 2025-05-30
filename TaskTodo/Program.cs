using Microsoft.EntityFrameworkCore;
using TaskTodo.DAL.AccessService;
using TaskTodo.DAL.Repository;
using TaskTodo.Data;
using TaskTodo.Services;

var builder = WebApplication.CreateBuilder(args);
var Myallowedports = "AllowLocalhost3000";

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddDbContext<ApplicationDbContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<ITask, TaskService>();
builder.Services.AddScoped<ITaskAuth, TaskAuth>();
 builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: Myallowedports,
                      policy =>
                      {
                          policy.WithOrigins("https://localhost:61634", "https://localhost:61635")
                          .AllowAnyHeader()
                          .AllowAnyMethod();
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

app.UseCors(Myallowedports);
app.UseAuthorization();

app.MapControllers();

app.Run();
