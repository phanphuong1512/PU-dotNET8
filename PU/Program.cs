using Microsoft.EntityFrameworkCore;
using PU.Data;
using PU.Repositories.Interfaces;
using PU.Repositories.Implementations;
using PU.Services.Interfaces;
using PU.Services.Implementations;

var builder = WebApplication.CreateBuilder(args);

// --- Add database ---
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// --- Add DI for Repository & Service ---
builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
builder.Services.AddScoped(typeof(IBaseService<>), typeof(BaseService<>));

// --- Add controller ---
builder.Services.AddControllers();

// --- Add Swagger ---
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// --- Middleware ---
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers(); // để gọi các controller API của bạn

app.Run();