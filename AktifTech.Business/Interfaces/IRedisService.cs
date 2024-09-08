using AktifTech.Database.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AktifTech.Business.Interfaces
{
    public interface IRedisService
    {
        public Task<bool> CreateProductListCache(List<Product> products);
        public Task<List<Product>> GetProductListFromCache();
    }
}
