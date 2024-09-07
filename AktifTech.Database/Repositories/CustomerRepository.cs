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
    public class CustomerRepository : ICustomerRepository
    {
        private readonly AppDbContext _context;

        public CustomerRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ResultSet> DeleteCustomerAsync(Customer customer)
        {
            ResultSet result = new ResultSet();
            _context.Remove(customer);
            int count = await _context.SaveChangesAsync();
            if (count <= 0)
            {
                result.Result = Result.Fail;
                result.Message = "Silme işlemi başarısız";
            }
            return result;
        }

        public async Task<Customer?> GetCustomerAsync(int id)
        {
            return await _context.Customer.FindAsync(id);
        }

        public async Task<Customer?> LoginAsync(string mail, string password)
        {
            //string _password = Encryption.Encrypt(password); //business tarafına alınacak
            return await _context.Customer.FirstOrDefaultAsync(x => x.Mail == mail && x.Password == password);
        }

        public async Task<ResultSet> SaveCustomerAsync(Customer customer)
        {
            ResultSet result = new ResultSet();
            try
            {
                //customer.Password = Encryption.Encrypt(customer.Password); //business tarafına alınacak
                _context.Add(customer);
                int count = await _context.SaveChangesAsync();
                if (count > 0)
                {
                    result.Id = customer.Id;
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

        public async Task<ResultSet> UpdateCustomerAsync(Customer customer)
        {
            ResultSet result = new ResultSet();
            try
            {
                _context.Update(customer);
                int count = await _context.SaveChangesAsync();
                if (count > 0)
                {
                    result.Id = customer.Id;
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
