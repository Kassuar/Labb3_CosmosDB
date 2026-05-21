using Labb3_CosmosDB.Data;
using Labb3_CosmosDB.Models;
using Microsoft.OpenApi;
using Microsoft.AspNetCore.Builder;
using Labb3_CosmosDB.Endpoints;
using Labb3_CosmosDB.Interfaces;
using Labb3_CosmosDB.Service;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<CosmosContext>();

builder.Services.AddScoped<ICustomerService, CustomerService>();


var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.Register();


app.Run();
