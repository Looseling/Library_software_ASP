using BulkyBook.Models;
using BulkyBookModels;
using BulkyBookWeb.Data;

namespace BulkyBookDataAccess.Repository
{
    public class ApplicationIdentityUserRepository : Repository<ApplicationIdentityUser>, IApplicationIdentityUserRepository
    {
        private readonly ApplicationDbContext _db;

        public ApplicationIdentityUserRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

     

 

      

    }
}