using Microsoft.EntityFrameworkCore;
using TE.TT.MarketApi.Abstarct;
using TE.TT.MarketApi.Database;
using TE.TT.MarketApi.ExtensionMethods;
using TE.TT.MarketApi.Repository;
using TE.TT.MarketApi.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddService();
builder.Services.AddDbContext<DataContext>(opt => opt.UseMySql(builder.Configuration.GetValue<string>("Db"),
    ServerVersion.AutoDetect((builder.Configuration.GetValue<string>("Db")))));
var app = builder.Build();
//??????? ??? ????????? api ??? ????? ??????? ?????
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapOpenApi();
}
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
