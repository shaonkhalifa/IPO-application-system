
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace DemoApp.Data.Models
{
    public  class User : IdentityUser<int>
    {
       
        public string Name { get; set; } = default!;
        public string CellPhone { get; set; }= default!;

        public virtual ICollection< IpoApplicationDetails> ApplicationDetails { get; set; } = new HashSet<IpoApplicationDetails>();
    }
}
