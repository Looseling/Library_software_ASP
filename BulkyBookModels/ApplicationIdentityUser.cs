using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBookModels
{
    public class ApplicationIdentityUser : IdentityUser
    {
        [Required]
        public string Name { get; set; }
        public string? City { get; set; }
        public string? Address { get; set; }
        public string? Postal { get; set; }
        public string? State { get; set; }
        public string? StreetAddress { get; set; }

    }
}
