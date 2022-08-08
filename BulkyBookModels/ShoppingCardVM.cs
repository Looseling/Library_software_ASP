using BulkyBook.Models;
using BulkyBookModels;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.Models.ViewModels
{
    public class ShoppingCardVM
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        [ForeignKey("ProductId")]
        [ValidateNever]
        public Product product { get; set; }
        [Range(1,1000, ErrorMessage = "Number should be between 1-1000")]
        public int count { get; set; }

        public string ApplicationIdentityUserId { get; set; }
        [ForeignKey("ApplicationIdentityUserId")]
        [ValidateNever]
        public ApplicationIdentityUser applicationIdentityUser{ get; set; }

        [NotMapped]
        public double Price { get; set; }
    }
}
