using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TestRabbitMQ
{
    class Program
    {
        public static string QueueName = "MyFanoutQueue";
        public static string ExchangeName = "MyFanoutExchange";

        static void Main(string[] args)
        {
            var factory = new ConnectionFactory
            {
                HostName = "localhost",
                UserName = "guest",
                Password = "guest"
            };

            var connection = factory.CreateConnection();
            var model = connection.CreateModel();
            model.ExchangeDeclare(ExchangeName, "fanout", true, true, null);
            model.QueueDeclare(QueueName, true, false, true, null);
            model.QueueBind(QueueName, ExchangeName, "");
            
            
            var consumer = new EventingBasicConsumer(model);
            
            consumer.Received += (m, ev) =>
            {
                var readMessage = ev.Body;
            };

            var numberOfMessages = model.MessageCount(QueueName);
            var message = Encoding.ASCII.GetBytes("Ovo je druga testna poruka");
            model.BasicPublish(ExchangeName, "route6", null, message); 
            numberOfMessages = model.MessageCount(QueueName);
            numberOfMessages = model.MessageCount(QueueName);
            Thread.Sleep(200);
            model.BasicConsume(QueueName, autoAck: false, consumer: consumer);
            model.BasicQos(0, 1, false);
            numberOfMessages = model.MessageCount(QueueName);
            numberOfMessages = model.MessageCount(QueueName);
            Thread.Sleep(200);
            numberOfMessages = model.MessageCount(QueueName);
            numberOfMessages = model.MessageCount(QueueName);
        }
    }
}
