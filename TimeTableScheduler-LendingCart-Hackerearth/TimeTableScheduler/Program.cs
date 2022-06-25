using TimeTableScheduler.Repository.Context;
using Microsoft.EntityFrameworkCore;
using TimeTableScheduler.Common.Services;
using TimeTableScheduler.Common.Services.Contracts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<SchoolDbContext>(o => o.UseSqlite("Data Source = SchoolDb.db"));
builder.Services.AddScoped<IClassService, ClassService>();
builder.Services.AddScoped<ITeacherService, TeacherService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
