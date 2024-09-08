using AktifTech.Constant;
using AktifTech.Database.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AktifTech.Business.Interfaces
{
    public interface IOrderProductService
    {
        public Task<List<OrderProduct>> GetOrderProductListAsync();
        public Task<OrderProduct?> GetOrderProductAsync(int id);
        public Task<ResultSet> SaveOrderProductAsync(OrderProduct orderOrderProduct);
        public Task<ResultSet> UpdateOrderProductAsync(OrderProduct orderOrderProduct);
        public Task<ResultSet> DeleteOrderProductAsync(OrderProduct orderOrderProduct);
    }
}
