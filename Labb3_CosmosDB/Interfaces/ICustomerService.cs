using Labb3_CosmosDB.Models;

namespace Labb3_CosmosDB.Interfaces
{
    public interface ICustomerService
    {
        Task AddCustomer(Customer customer);

        Task<IEnumerable<Customer>> GetAll();

        Task<Customer> GetCustomerById(string id);

        Task UpdateCustomer(Customer customer);

        Task DeleteCustomer (string id);

        Task <IEnumerable<Customer>> SearchCustomerByName(string name);

        Task<IEnumerable<Customer>> SearchCustomerBySellerName(string sellerName);


    }
}
