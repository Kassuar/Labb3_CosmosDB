using Microsoft.AspNetCore.Identity;
using System.ComponentModel;
using Microsoft.Azure.Cosmos;
using Labb3_CosmosDB.Interfaces;
using Labb3_CosmosDB.Data;
using Labb3_CosmosDB.Models;

namespace Labb3_CosmosDB.Service
{
    public class CustomerService : ICustomerService
    {
        private readonly Microsoft.Azure.Cosmos.Container _container;

        public CustomerService(CosmosContext context)
        {
            _container = context.Container;
        }
            public async Task AddCustomer(Customer customer)
            {
                await _container.CreateItemAsync(customer, new PartitionKey(customer.id));
            }

            public async Task DeleteCustomer(string id)
            {
                await _container.DeleteItemAsync<Customer>(id, new PartitionKey(id));
            }

            public async Task<IEnumerable<Customer>> GetAll()
            {
                var customers =
                    new List<Customer>();

                var iterator =
                    _container
                    .GetItemQueryIterator<Customer>(
                        "SELECT * FROM c"
                    );

                while (
                    iterator.HasMoreResults
                )
                {
                    var response =
                        await iterator
                        .ReadNextAsync();

                    customers.AddRange(
                        response
                    );
                }

                return customers;
            }

            public async Task<Customer> GetCustomerById(string id)
            {
                var response = await _container.ReadItemAsync<Customer>(id, new PartitionKey(id));

                return response.Resource;
            }

            public async Task<IEnumerable<Customer>> SearchCustomerByName(string name)
            {
                var customers = new List<Customer>();

                var iterator = _container.GetItemQueryIterator<Customer>($"Select * from c where c.Name ='{name}'");

                while (iterator.HasMoreResults)
                {
                    var response = await iterator.ReadNextAsync();

                    customers.AddRange(response);
                }

                return customers;
            }

            public async Task<IEnumerable<Customer>> SearchCustomerBySellerName(string sellerName)
            {
                var customers = new List<Customer>();

                var iterator = _container.GetItemQueryIterator<Customer>($"Select ' from c where c.Seller.Name='{sellerName}'");

                while (iterator.HasMoreResults)
                {
                    var response = await iterator.ReadNextAsync();

                    customers.AddRange(response);

                }

                return customers;

            }

            public async Task UpdateCustomer(Customer customer)
            {
                await _container.ReplaceItemAsync(customer, customer.id, new PartitionKey(customer.id));
            }

    }
}





