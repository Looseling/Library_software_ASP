using BulkyBook.Models;
using BulkyBookWeb.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBookDataAccess.Repository
{
    public class CoverTypesRepository : Repository<CoverType>, ICoverTypesRepository
    {
        private readonly ApplicationDbContext _db;

        public CoverTypesRepository( ApplicationDbContext db) : base(db )
        {
            _db = db;
        }

        public void Update(CoverType entity)
        {
            _db.CoverTypes.Update(entity);
        }
    }
}
