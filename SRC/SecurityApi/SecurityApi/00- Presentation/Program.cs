using SecurityApi.Application.Interfaces;
using SecurityApi.Application.Services;
using SecurityApi.Infra.Data.Interfaces;
using SecurityApi.Infra.Data.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

//TODO: separar em um arquivo 
#region Services
builder.Services.AddScoped<ISecurityPriceService, SecurityPriceService>();
#endregion
#region Repository
builder.Services.AddScoped<ISecurityPriceRepository, SecurityPriceRepository>();
#endregion
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
