using AktifTech.Constant;
using AktifTech.Database.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AktifTech.Database.Repositories.Interfaces
{
    public interface IOrderProductRepository
    {
        public Task<List<OrderProduct>> GetOrderProductListAsync();
        public Task<OrderProduct?> GetOrderProductAsync(int id);
        public Task<ResultSet> SaveOrderProductAsync(OrderProduct orderProduct);
        public Task<ResultSet> UpdateOrderProductAsync(OrderProduct orderProduct);
        public Task<ResultSet> DeleteOrderProductAsync(OrderProduct orderProduct);
    }
}
