using BulkyBookWeb.Data;

namespace BulkyBookDataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {

        public ICategoryRepository Category { get; }

        public ICoverTypesRepository CoverType { get; }
        
        public IProductRepository Product { get; }

        public ICompanyRepository Company { get; }

        public IShoppingCardRepository ShoppingCard { get; }

        public IApplicationIdentityUserRepository ApplicationIdentityUser { get; }

        public IOrderDetailsRepository OrderDetails { get; }

        public IOrderHeaderRepository OrderHeader { get; }

        private readonly ApplicationDbContext _db;

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Category = new CategoryRepository(db);
            CoverType = new CoverTypesRepository(db);
            Product = new ProductRepository(db);
            Company = new CompanyRepository(db);
            ShoppingCard = new ShoppingCardRepository(db);
            ApplicationIdentityUser = new ApplicationIdentityUserRepository(db);
            OrderDetails = new OrderDetailsRepository(db);
            OrderHeader = new OrderHeaderRepository(db);
        }


        public void Save()
        {
            _db.SaveChanges();
        }



    }
}
