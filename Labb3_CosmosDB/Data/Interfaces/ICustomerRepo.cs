using Labb3_CosmosDB.Data.Models;

namespace Labb3_CosmosDB.Data.Interfaces
{
    public interface ICustomerRepo
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
