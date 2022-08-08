using BulkyBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBookDataAccess.Repository
{
    public interface IOrderHeaderRepository: IRepository<OrderHeader>
    {
        public void Update(OrderHeader obj);

        public void UpdateStatus(int Id, string orderStatus, string? paymentStatus);

    }
}
