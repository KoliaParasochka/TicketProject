using Microsoft.AspNet.Identity.EntityFramework;

namespace Domain.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
        public string LastName { get; set; }
    }
}
