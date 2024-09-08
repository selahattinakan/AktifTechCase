using AktifTech.Business.Interfaces;
using AktifTech.Constant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AktifTech.Business.Decorators
{
    public class BaseCustomerOrderDecorator : IMessageBroker
    {
        private readonly ICustomerOrderService _customerOrderService;

        public BaseCustomerOrderDecorator(ICustomerOrderService customerOrderService)
        {
            _customerOrderService = customerOrderService;
        }

        public virtual async Task<ResultSet> ConfirmCustomerOrder(int id)
        {
            return await _customerOrderService.ConfirmCustomerOrder(id);
        }
    }
}
