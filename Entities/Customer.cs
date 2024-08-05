using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CustomerAPI.Entities
{
    [Table("Customer")]
    public class Customer
    {
        [Key]
        public long Id { get; set; }

        [Required]
        [StringLength(250)]
        public string? Name { get; set; }

        [StringLength(10)]
        public string? City { get; set; }

        public int EmployeeCount { get; set; }

        [Required]
        public DateTimeOffset DateCreated { get; set; }

        [StringLength(50)]
        public string CreatedBy { get; set; }

        public DateTimeOffset DateUpdated { get; set; }

        [StringLength(50)]
        public string UpdatedBy { get; set; }
     
    }
}
