using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using EFContext;
using CustomerAPI.Entities;

namespace CustomerAPI.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        //Define the local variables
        private readonly CustomerDbContext _context;
        private readonly IMapper _mapper;
        private static bool isFirstTime = true;

        #region Constructors
        public CustomerRepository(CustomerDbContext context,
            IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
      

            if (isFirstTime)
            {
                // While development of the app is being done: delete the database, then create again, and add some data
                _context.Database.EnsureDeleted();
                _context.Database.EnsureCreated();
                _context.Customer.Add(new CustomerAPI.Entities.Customer
                {
                    Name = "ACME Ltd.",
                    City = "Birmingham",
                    EmployeeCount = 1,
                    CreatedBy = Environment.UserName,
                    DateCreated = DateTime.UtcNow,
                    UpdatedBy = "",
                    DateUpdated = DateTime.MinValue
                });
                _context.Customer.Add(new CustomerAPI.Entities.Customer
                {
                    Name = "McD",
                    City = "Manchester",
                    EmployeeCount = 101,
                    CreatedBy = Environment.UserName,
                    DateCreated = DateTime.UtcNow,
                    UpdatedBy = "",
                    DateUpdated = DateTime.MinValue
                });
                _context.SaveChanges();
                isFirstTime = false;
            }
        }

        #endregion

        #region Methods
        /// <summary> Get all customers </summary>
        public async Task<List<Customer>> GetCustomers()
        {
            //Generate the query
            var query = _context.Customer.AsQueryable();

            //  var query = filter.ToQuery(_context);
            var entities = await query.ToListAsync();

            return entities;
        }

        /// <summary> Get the customer matching the identifier </summary>
        public async Task<Customer> GetCustomer(long customerId)
        {

            //Exit if no id has been defined
            if (customerId == 0)
                throw new ValidationException("No identifer has been supplied");

            //Generate the query
            var query = _context.Customer.AsQueryable();

            //Filter by the id
            query = query.Where(x => x.Id == customerId);

            var entities = await query.ToListAsync();

            //Have we found a single entity?
            if (entities.Count > 1)
            {
                //Throw an error up
                throw new ValidationException("More than one customer was found for the identifier");
            }

            //Have we found a single entity?
            if (entities.Count > 0)
            {
                //Return the entity
                return entities[0];
            }

            //Return the default value
            return null;
        }

        /// <summary> Create a new customer </summary>
        public async Task<Customer> CreateCustomer(Customer model)
        {
            //Validate the model
            if (model.Id != 0)
                throw new ValidationException("An invalid customer id has been defined");
            if (string.IsNullOrWhiteSpace(model.Name))
                throw new ValidationException("The customer name has not been defined");
 
            //Does the entity already exist?
            var entity = await _context.Customer.FirstOrDefaultAsync(x => x.Name.ToUpper() == model.Name.ToUpper());
            if (entity != null)
                throw new ValidationException("A customer with the same name already exists");

            //Update the date/time values
            model.DateCreated = DateTime.UtcNow;
            //  model.CreatedBy = _identity.UserId;
            model.DateUpdated = model.DateCreated;
            // model.UpdatedBy = _identity.UserId;

            //Add the entity
            _context.Customer.Add(model);

            //Return the updated entity
            return model;
        }

        /// <summary> Update an existing customer </summary> 
        public async Task<Customer> UpdateCustomer(Customer model) 
        {
            //Validate the model
            if (model.Id <= 0)
                throw new ValidationException("An invalid customer id has been defined");
            if (string.IsNullOrWhiteSpace(model.Name))
                throw new ValidationException("The customer name has not been defined");

            //Does the entity exist?
            var entity = await _context.Customer.FirstOrDefaultAsync(x => x.Id == model.Id);
            if (entity == null)
                throw new ValidationException("The customer does not exist");

            //Has the name changed?
            if (model.Name != entity.Name)
            {
                //Check for duplicates
                var duplicate = await _context.Customer.FirstOrDefaultAsync(x => x.Id != entity.Id && x.Name.ToUpper() == model.Name.ToUpper());
                if (duplicate != null)
                    throw new ValidationException("A customer with the same name already exists");
            }

            //Check for changes and exit if we have none
            if (!this.HasChanged(model, entity))
                return entity;

            //Update the existing entity
            var dateCreated = entity.DateCreated;
            var createdBy = entity.CreatedBy;
            _mapper.Map(model, entity, options =>
            {
                options.AfterMap((source, target) =>
                {
                    //Restore the creation values
                    target.DateCreated = dateCreated;
                    target.CreatedBy = createdBy;
                });
            });

            //Return the updated entity
            return entity;
        }

        /// <summary> Delete an existing customer </summary>
        public async Task<Customer> DeleteCustomer(long customerId)
        {
            //Validate the arguments
            if (customerId <= 0)
                throw new ValidationException("An invalid customer id has been defined");

            //Get the customer
            var entity = await this.GetCustomer(customerId);

            //Does the customer exist OR has the customer already been deleted?
            if (entity == null) 
                throw new ValidationException("The customer does not exist");

            await _context.SaveChangesAsync();

            //Return the deleted entity
            return entity;
        }

        #endregion

        #region Helpers

        /// <summary> Compare the two objects for changes (excluding auto generated fields etc) and throw a nochange exception if nothing has changed </summary>
        private bool HasChanged(Customer source, Customer target)
        {
            //Compare the values
            return source.Name != target.Name;
        }

        #endregion
    }
}
