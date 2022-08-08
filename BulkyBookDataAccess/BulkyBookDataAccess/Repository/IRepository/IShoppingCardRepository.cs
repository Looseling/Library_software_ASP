using BulkyBook.Models;
using BulkyBook.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBookDataAccess.Repository
{
    public interface IShoppingCardRepository: IRepository<ShoppingCardVM>  
    {
        int IncrementCount(ShoppingCardVM obj, int count);

        int DecrementCount(ShoppingCardVM obj, int count);

    }
}
