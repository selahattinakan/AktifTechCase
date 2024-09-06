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
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;

        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<ResultSet> DeleteProductAsync(Product product)
        {
            ResultSet result = new ResultSet();
            _context.Remove(product);
            int count = await _context.SaveChangesAsync();
            if (count <= 0)
            {
                result.Result = Result.Fail;
                result.Message = "Silme işlemi başarısız";
            }
            return result;
        }

        public async Task<Product?> GetProductAsync(int id)
        {
            return await _context.Product.FindAsync(id);
        }

        public async Task<List<Product>> GetProductListAsync()
        {
            return await _context.Product.ToListAsync();
        }

        public async Task<ResultSet> SaveProductAsync(Product product)
        {
            ResultSet result = new ResultSet();
            try
            {
                _context.Add(product);
                int count = await _context.SaveChangesAsync();
                if (count > 0)
                {
                    result.Id = product.Id;
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

        public async Task<ResultSet> UpdateProductAsync(Product product)
        {
            ResultSet result = new ResultSet();
            try
            {
                _context.Update(product);
                int count = await _context.SaveChangesAsync();
                if (count > 0)
                {
                    result.Id = product.Id;
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
