using System.ComponentModel.DataAnnotations;

namespace CustomerAPI.Models
{
    public class Customer
    {
        public long Id { get; set; }

        public string? Name { get; set; }

        public string? City { get; set; }

        public int EmployeeCount { get; set; }

        public DateTimeOffset DateCreated { get; set; }

        public string? CreatedBy { get; set; }

        public DateTimeOffset DateUpdated { get; set; }

        public string? UpdatedBy { get; set; }
    }
}
