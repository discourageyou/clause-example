using Microsoft.EntityFrameworkCore;
using StarRezApi.Contracts.V1_0.Requests;
using StarRezApi.Contracts.V1_0.Validators;
using StarRezApi.Data;
using StarRezApi.Mappers;
using StarRezApi.Repositories;
using StarRezApi.Services;
using StarRezApi.Validation;

var builder = WebApplication.CreateBuilder(args);

// Database
builder.Services.AddDbContext<GameDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Services
builder.Services.AddScoped<IGameService, GameService>();
builder.Services.AddScoped<IGameHistoryRepository, GameHistoryRepository>();
builder.Services.AddScoped<IGameHistoryMapper, GameHistoryMapper>();

// Validators
builder.Services.AddSingleton<IValidator<ValidateRequest>, ValidateRequestValidator>();
builder.Services.AddSingleton<IValidator<CollectionRequest>, CollectionRequestValidator>();
builder.Services.AddSingleton<IValidator<HistoryRequest>, HistoryRequestValidator>();

builder.Services.AddControllers();
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
