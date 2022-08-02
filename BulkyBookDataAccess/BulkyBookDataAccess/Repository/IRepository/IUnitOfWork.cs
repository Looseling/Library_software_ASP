using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBookDataAccess.Repository
{
    public interface IUnitOfWork
    {
        ICategoryRepository Category { get; }

        ICoverTypesRepository CoverType { get; }

        IProductRepository Product { get; }

        ICompanyRepository Company { get; }

        void Save();
    }
}
