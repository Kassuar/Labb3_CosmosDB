using Labb3_CosmosDB.Interfaces;
using Labb3_CosmosDB.Models;

namespace Labb3_CosmosDB.Endpoints
{
    public static class CustomerEndPoint
    {
       public static void Register (this WebApplication app)
        {
            app.MapPost("/customers", async (Customer customer, ICustomerService service) =>
            {
                await service.AddCustomer(customer);

                return Results.Ok(customer);
            });

            app.MapGet("/customers", async (ICustomerService repo) =>
            {
                var customer = await repo.GetAll();

                return Results.Ok(customer);
            });

            app.MapGet("/customer/{Id}", async (string id, ICustomerService repo) =>
            {
                var customer = await repo.GetCustomerById(id);

                return Results.Ok(customer);
            });

            app.MapPut("/customer/{id}", async (string id, Customer customer, ICustomerService repo) =>
            {

                customer.id = id;

                await repo.UpdateCustomer(customer);

                return Results.Ok(customer);

            });

            app.MapDelete("/customer/{id}", async (string id, ICustomerService repo) =>
            {
                await repo.DeleteCustomer(id);

                return Results.Ok();
            });

            app.MapGet("/customer/search/name", async (string name, ICustomerService repo) =>
            {
                var customers = await repo.SearchCustomerByName(name);

                return Results.Ok(customers);
            });

            app.MapGet("/customer/search/seller", async (string sellerName, ICustomerService repo) =>
            {
                var customers = await repo.SearchCustomerByName(sellerName);

                return Results.Ok(customers);
            });

        }
    }
}
