using System;
using System.Collections.Generic;
using System.Text;

namespace RabbitMQ.Utils
{
    public interface IMessageConsume
    {
        void Consume(string message);
    }
}
