using Meteorites.Business.Services;
using Meteorites.DataAccess;
using Meteorites.DataAccess.Repositories;
using Meteorites.Infrastructure.ExternalClients;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;

services.AddCors(options => options.AddDefaultPolicy(policy => policy.AllowAnyOrigin()));

services.AddDbContext<MeteoriteDbContext>(options =>
    options.UseInMemoryDatabase("Meteorites"));

services.AddAutoMapper(typeof(MeteoriteService).Assembly);
services.AddHostedService<MeteoriteLoadService>();
services.AddHttpClient();
services.AddMemoryCache();

services.AddSingleton<ICacheService, CacheService>();

services.AddScoped<IMeteoriteRepository, MeteoriteRepository>();
services.AddScoped<IMeteoriteService, MeteoriteService>();
services.AddScoped<IMeteoriteExternalClient, MeteoriteExternalClient>();

services.AddControllers();
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors();

app.MapControllers();

app.Run();
