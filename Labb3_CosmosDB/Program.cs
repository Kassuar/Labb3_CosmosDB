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
    await repo.AddCustomer(customer);

    return Results.Ok(customer);
});

app.MapGet(
    "/customers",

    async (ICustomerRepo repo) =>
    {
        var customer = await repo.GetAll();

        return Results.Ok(customer);
    });

app.MapGet(
"/customer/{Id}",

async (
string id,
ICustomerRepo repo
) =>
{
    var customer = await repo.GetCustomerById(id);

    return Results.Ok(customer);
});

app.MapPut("/customer/{id}",async (string id,Customer customer,ICustomerRepo repo) =>
{

 customer.id = id;

 await repo.UpdateCustomer(customer);

 return Results.Ok(customer);

});

app.MapDelete("/customer/{id}", async (string id, ICustomerRepo repo) =>
{
    await repo.DeleteCustomer(id);

    return Results.Ok();
});





app.Run();
