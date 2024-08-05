using AutoMapper;
using CustomerAPI.Models;
using CustomerAPI.Repositories;
using System.ComponentModel.DataAnnotations;

namespace CustomerAPI.Services
{
    public class CustomerService : ICustomerService
    {
        //Define the local variables
        private readonly ICustomerRepository _customers;
        private readonly IMapper _mapper;

        #region Constructors

        public CustomerService(ICustomerRepository customers,
IMapper mapper)
        {
            _customers = customers ?? throw new ArgumentNullException(nameof(customers));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        #endregion

        #region Customers

        /// <summary> Get all customers matching the filter criteria </summary>
        public async Task<List<Customer>> GetCustomers()
        {
            //Get the entities
            var entities = await _customers.GetCustomers();

            //Map the entities to a model
            return _mapper.Map<List<Customer>>(entities);
        }

        /// <summary> Get the customer matching the identifier</summary>
        public async Task<Customer> GetCustomer(long customerId)
        {
            //Validate the arguments
            if (customerId <= 0)
                throw new ValidationException("An invalid customer id has been defined");

            //Get the entity
            var entities = await _customers.GetCustomer(customerId);

            //Map the entities to a model
            return _mapper.Map<Customer>(entities);
        }


        /// <summary> Create a new customer </summary>
        public async Task<Customer> CreateCustomer(Customer model)
        {
            //Generate a new entity 
            var entity = _mapper.Map<Models.Customer>(model);

            //Create the entity
            // TODO!  entity = await _customers.CreateCustomer(entity);

            //Map the entity to a model
            var result = _mapper.Map<Customer>(entity);

            //Return the result
            return result;
        }

        /// <summary> Update an existing customer  </summary>
        public async Task<Customer> UpdateCustomer(Customer model)
        {
            //Generate the entity 
            var entity = _mapper.Map<Entities.Customer>(model);

            //Update the entity
            entity = await _customers.UpdateCustomer(entity); 

            //Map the entity to a model
            return _mapper.Map<Customer>(entity);
        }

        /// <summary> Delete an existing customer </summary>
        public async Task<Customer> DeleteCustomer(long customerId)
        {
            //Delete the customer
            var entity = await _customers.DeleteCustomer(customerId);

            //Map the entity to a model
            return _mapper.Map<Customer>(entity);
        }

        #endregion

    }
}
