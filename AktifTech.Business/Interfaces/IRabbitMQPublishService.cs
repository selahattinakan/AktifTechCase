using AktifTech.Database.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AktifTech.Business.Interfaces
{
    public interface IRabbitMQPublishService
    {
        public void Publish(string message);
    }
}
