using Labb3_CosmosDB.Data.Interfaces;
using Labb3_CosmosDB.Data.Models;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel;
using Microsoft.Azure.Cosmos;

namespace Labb3_CosmosDB.Data.Repo
{
    public class CustomerRepo : ICustomerRepo
    {
        private readonly Microsoft.Azure.Cosmos.Container _container;

        public CustomerRepo(CosmosContext context)
        {
            _container = context.Container;
        }
        public async Task AddCustomer(Customer customer)
        {
            await _container.CreateItemAsync(customer, new PartitionKey(customer.id));
        }

        public async Task DeleteCustomer( string id)
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

        public Task<IEnumerable<Customer>> SearchCustomerByName(string name)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Customer>> SearchCustomerBySellerName(string sellerName)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateCustomer(Customer customer)
        {
            await _container.ReplaceItemAsync(customer, customer.id, new PartitionKey(customer.id));
        }
    }
}
