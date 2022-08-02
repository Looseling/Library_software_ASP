using BulkyBookModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBookDataAccess.Repository
{
    public interface ICompanyRepository : IRepository<Company>
    {
        public void Update(Company obj);
    }
}
