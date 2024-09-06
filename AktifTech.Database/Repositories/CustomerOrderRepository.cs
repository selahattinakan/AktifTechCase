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
    public class CustomerOrderRepository : ICustomerOrderRepository
    {
        private readonly AppDbContext _context;

        public CustomerOrderRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ResultSet> DeleteCustomerOrderAsync(CustomerOrder customerOrder)
        {
            ResultSet result = new ResultSet();
            _context.Remove(customerOrder);
            int count = await _context.SaveChangesAsync();
            if (count <= 0)
            {
                result.Result = Result.Fail;
                result.Message = "Silme işlemi başarısız";
            }
            return result;
        }

        public async Task<CustomerOrder?> GetCustomerOrderAsync(int id)
        {
            return await _context.CustomerOrder.FindAsync(id);
        }

        public async Task<ResultSet> SaveCustomerOrderAsync(CustomerOrder customerOrder)
        {
            ResultSet result = new ResultSet();
            try
            {
                _context.Add(customerOrder);
                int count = await _context.SaveChangesAsync();
                if (count > 0)
                {
                    result.Id = customerOrder.Id;
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

        public async Task<ResultSet> UpdateCustomerOrderAsync(CustomerOrder customerOrder)
        {
            ResultSet result = new ResultSet();
            try
            {
                _context.Update(customerOrder);
                int count = await _context.SaveChangesAsync();
                if (count > 0)
                {
                    result.Id = customerOrder.Id;
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
