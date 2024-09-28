using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace NetReactMovie.Server.Models.Entities
{
    public class User : IdentityUser
    {
        [Key]
        public override string Id { get; set; } 
        public string? Name { get; set; }
        public override string? Email { get; set; }
        public string?  Password { get; set; }
    }
}
