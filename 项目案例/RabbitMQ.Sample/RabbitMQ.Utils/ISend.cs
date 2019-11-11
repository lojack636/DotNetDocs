using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using EasyNetQ;

namespace RabbitMQ.Utils
{
    internal interface ISend
    {
        void SendMessage(PublishMessage message, IBus bus);
        Task SendMessageAsync(PublishMessage message, IBus bus);
    }
}
