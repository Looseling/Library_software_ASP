using BulkyBook.Models;
using BulkyBookWeb.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBookDataAccess.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly ApplicationDbContext _db;

        public ProductRepository( ApplicationDbContext db) : base(db )
        {
            _db = db;
        }

        public void Update(Product obj)
        {
            var objFromDb = _db.Products.FirstOrDefault(u => u.Id == obj.Id); 

            if (objFromDb != null)
            {
                objFromDb.ISBN = obj.ISBN;
                objFromDb.Description = obj.Description;
                objFromDb.CoverTypeId = obj.CoverTypeId;
                objFromDb.CategoryId  = obj.CategoryId;
                objFromDb.Title = obj.Title;
                objFromDb.Price = obj.Price;
                objFromDb.Price50  = obj.Price50;
                objFromDb.Price100 = obj.Price100;
                objFromDb.Author  = obj.Author;
                objFromDb.ListPrice = obj.ListPrice;
                if (obj.ImageUrl != null)
                {
                    objFromDb.ImageUrl = obj.ImageUrl;
                }
            }
        }
    }
}
