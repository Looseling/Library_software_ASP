using BulkyBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBookDataAccess.Repository
{
    public interface IProductRepository : IRepository<Product>
    {
        public void Update(Product obj);



    }
}
