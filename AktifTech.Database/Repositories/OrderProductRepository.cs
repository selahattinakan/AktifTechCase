using AktifTech.Constant;
using AktifTech.Database.DataAccessLayer;
using AktifTech.Database.Entity;
using AktifTech.Database.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AktifTech.Database.Repositories
{
    public class OrderProductRepository : IOrderProductRepository
    {
        private readonly AppDbContext _context;

        public OrderProductRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<ResultSet> DeleteOrderProductAsync(OrderProduct orderProduct)
        {
            ResultSet result = new ResultSet();
            _context.Remove(orderProduct);
            int count = await _context.SaveChangesAsync();
            if (count <= 0)
            {
                result.Result = Result.Fail;
                result.Message = "Silme işlemi başarısız";
            }
            return result;
        }

        public async Task<OrderProduct?> GetOrderProductAsync(int id)
        {
            return await _context.OrderProduct.FindAsync(id);
        }

        public async Task<List<OrderProduct>> GetOrderProductListAsync()
        {
            return await _context.OrderProduct.ToListAsync();
        }

        public async Task<ResultSet> SaveOrderProductAsync(OrderProduct orderProduct)
        {
            ResultSet result = new ResultSet();
            try
            {
                _context.Add(orderProduct);
                int count = await _context.SaveChangesAsync();
                if (count > 0)
                {
                    result.Id = orderProduct.Id;
                }
                else
                {
                    result.Result = Result.Fail;
                    result.Message = "Db işlemi başarısız";
                }
            }
            catch (Exception ex)
            {
                result.Result = Result.Fail;
                result.Message = ex.Message;
            }
            return result;
        }

        public async Task<ResultSet> UpdateOrderProductAsync(OrderProduct orderProduct)
        {
            ResultSet result = new ResultSet();
            try
            {
                _context.Update(orderProduct);
                int count = await _context.SaveChangesAsync();
                if (count > 0)
                {
                    result.Id = orderProduct.Id;
                }
                else
                {
                    result.Result = Result.Fail;
                    result.Message = "Db işlemi başarısız";
                }
            }
            catch (Exception ex)
            {
                result.Result = Result.Fail;
                result.Message = ex.Message;
            }
            return result;
        }
    }
}
