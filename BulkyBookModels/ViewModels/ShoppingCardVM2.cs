using BulkyBook.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBookModels.ViewModels
{
    public class ShoppingCardVM2
    {
        public IEnumerable<ShoppingCardVM> ListCart { get; set; }

        public double CartTotal { get; set; }
    }
}
