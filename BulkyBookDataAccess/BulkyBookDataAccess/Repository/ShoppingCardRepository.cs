using BulkyBook.Models;
using BulkyBook.Models.ViewModels;
using BulkyBookWeb.Data;

namespace BulkyBookDataAccess.Repository
{
    public class ShoppingCardRepository : Repository<ShoppingCardVM>, IShoppingCardRepository
    {
        private readonly ApplicationDbContext _db;

        public ShoppingCardRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public int DecrementCount(ShoppingCardVM obj, int count)
        {
            obj.count -= count;
            return obj.count;
        }

        public int IncrementCount(ShoppingCardVM obj, int count)
        {
            obj.count += count;
            return obj.count;
        }
    }
}