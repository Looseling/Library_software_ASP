using BulkyBookWeb.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBookDataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {

        public ICategoryRepository Category { get; private set; }
        public ICoverTypesRepository CoverType { get; private set; }
        public IProductRepository Product { get; private set; }

        public ICompanyRepository Company { get; }

        private readonly ApplicationDbContext _db;

        public UnitOfWork( ApplicationDbContext db)
        {
            _db = db;
            Category = new CategoryRepository(db);
            CoverType = new CoverTypesRepository(db); 
            Product = new ProductRepository(db);
            Company = new CompanyRepository(db);
        }


        public void Save()
        {
            _db.SaveChanges();
        }



    }
}
