using Labb3_CosmosDB.Data;
using Labb3_CosmosDB.Data.Interfaces;
using Labb3_CosmosDB.Data.Models;
using Labb3_CosmosDB.Data.Repo;
using Microsoft.OpenApi;
using Microsoft.AspNetCore.Builder;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<CosmosContext>();

builder.Services.AddScoped<ICustomerRepo, CustomerRepo>();


var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();


app.MapPost(
"/customers",

async (
Customer customer,
ICustomerRepo repo
) =>
{
    await repo
       .AddCustomer(
          customer
       );

    return Results.Ok(
       customer
    );
});

app.MapGet(
"/customers/{Id}",

async (
string id,
ICustomerRepo repo
) =>
{
    var customer =
       await repo
       .GetCustomerById(
          id
       );

    return Results.Ok(
       customer
    );
});

app.Run();
