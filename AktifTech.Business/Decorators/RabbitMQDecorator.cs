using AktifTech.Business.Interfaces;
using AktifTech.Constant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AktifTech.Business.Decorators
{
    public class RabbitMQDecorator : BaseCustomerOrderDecorator
    {
        private readonly IRabbitMQPublishService _rabbitMQPublishService;

        public RabbitMQDecorator(ICustomerOrderService customerOrderService,IRabbitMQPublishService rabbitMQPublishService) : base(customerOrderService)
        {
            _rabbitMQPublishService = rabbitMQPublishService;
        }

        public override async Task<ResultSet> ConfirmCustomerOrder(int id)
        {
            var result = await base.ConfirmCustomerOrder(id);
            if (result.Result == Result.Success)
            {
                string message = $"{DateTime.Now} : Siparişiniz alınmıştır. Sipariş numaranız: {id}";
                _rabbitMQPublishService.Publish(message);
            }
            return result;
        }


    }
}
