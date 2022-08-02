using BulkyBook.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.Models.ViewModels
{
    public class ShoppingCardVM
    {
        public Product product = new();
        [Range(1,1000, ErrorMessage = "Number should be between 1-1000")]
        public int count { get; set; }
    }
}
