using BulkyBook.Models;
using BulkyBookModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBookDataAccess.Repository
{
    public interface IApplicationIdentityUserRepository: IRepository<ApplicationIdentityUser>  
    {
        //public void Update(ApplicationIdentityUser obj);
    }
}
