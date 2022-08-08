using BulkyBook.Models;
using BulkyBookDataAccess.Repository.IRepository;
using BulkyBookWeb.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBookDataAccess.Repository
{
    public class OrderHeaderRepository : Repository<OrderHeader>, IOrderHeaderRepository
    {
        private readonly ApplicationDbContext _db;
        public OrderHeaderRepository(ApplicationDbContext db): base(db)
        {
            _db = db;
        }
        public void Update(OrderHeader obj)
        {
            _db.OrderHeader.Update(obj);
        }

        public void UpdateStatus(int Id, string orderStatus, string? paymentStatus)
        {
            var OHFromDb= _db.OrderHeader.FirstOrDefault(u => u.Id == Id);

            if (OHFromDb!= null)
            {
                OHFromDb.OrderStatus = orderStatus;
                
                if (paymentStatus != null)
                {
                    OHFromDb.PaymentStatus = paymentStatus;
                }
            }
        }
    }
}
