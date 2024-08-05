using CustomerAPI.Models;
namespace CustomerAPI.Services
{
    public interface ICustomerService
    {

        /// <summary> Get the first customer for the id </summary>
        Task<Customer> GetCustomer(long customerId);

        /// <summary> Get all customers </summary>
        Task<List<Customer>> GetCustomers();

        /// <summary> Create a new customer </summary>
        Task<Customer> CreateCustomer(Customer model);

        /// <summary> Update an existing customer  </summary>
        Task<Customer> UpdateCustomer(Customer model);

        /// <summary> Delete an existing customer </summary>
        Task<Customer> DeleteCustomer(long customerId);
    }
}
