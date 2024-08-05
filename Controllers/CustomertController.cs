using Microsoft.AspNetCore.Mvc;
using CustomerAPI.Models;
using CustomerAPI.Services;

namespace CustomerAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomerController : ControllerBase
    {
        //Define the variables
        private readonly ICustomerService _customerService;

        /// <summary> Constructor </summary>
        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
         }

        //GET       ->  /api/customers/                 -> Gets all available customers
        //GET       ->  /api/customers/{id}             -> Gets the requested customer
        //POST      ->  /api/customers/{model}          -> Creates a new customer
        //PUT       ->  /api/customers/{model}          -> Updates an existing customer
        //DELETE    ->  /api/customers/{id}             -> Deletes an existing customer

        //GET All Customers  
        [HttpGet]
        public async Task<List<Customer>> GetCustomers()
        {
            var data = await _customerService.GetCustomers();
            return data;
        }

        /// <summary> Get the requested customer </summary>
        [HttpGet("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(Customer), StatusCodes.Status200OK)]
        public async Task<Customer> GetCustomer(long id)
        {
            var data = await _customerService.GetCustomer(id);
            return data;
        }
    }
}
