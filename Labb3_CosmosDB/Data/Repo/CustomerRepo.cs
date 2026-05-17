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
            await _container.CreateItemAsync(customer, new PartitionKey(customer.Id));
        }

        public Task DeleteCustomer(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Customer>> GetAll()
        {
               {
            var customers =
                new List<Customer>();

            var query =
                _container
                .GetItemLinqQueryable<
                    Customer>();

            foreach (
                var customer
                in query
            )
            {
                customers
                    .Add(customer);
            }

                return customers;
        };
        }

        public Task<Customer> GetCustomerById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Customer>> SearchCustomerByName(string name)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Customer>> SearchCustomerBySellerName(string sellerName)
        {
            throw new NotImplementedException();
        }

        public Task UpdateCustomer(Customer customer)
        {
            throw new NotImplementedException();
        }
    }
}
