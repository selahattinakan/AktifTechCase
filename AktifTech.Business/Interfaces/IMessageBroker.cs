using AktifTech.Constant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AktifTech.Business.Interfaces
{
    public interface IMessageBroker
    {
        public Task<ResultSet> ConfirmCustomerOrder(int id);
    }
}
