using System.ComponentModel.DataAnnotations;

namespace DemoApp.Data.Models
{
    public class Company
    {
        [Key]
        public int CompanyId { get; set; }
        public string CompanyName { get; set; } = default!;
    }
}
