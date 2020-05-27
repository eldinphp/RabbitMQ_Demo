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
        public static string QueueName = "My6Queue";
        public static string ExchangeName = "My6Exchange";

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
            model.ExchangeDeclare(ExchangeName, "direct", true, true, null);
            model.QueueDeclare(QueueName, true, false, true, null);
            model.QueueBind(QueueName, ExchangeName, "");
            
            
            var consumer = new EventingBasicConsumer(model);
            
            consumer.Received += (m, ev) =>
            {
                var readMessage = ev.Body;
            };

            var numberOfMessages = model.MessageCount(QueueName);
            var message = Encoding.ASCII.GetBytes("Ovo je druga testna poruka");
            model.BasicPublish("", QueueName, null, message); //CANT PUBLISH TO EXCHANGE NAME ?
            numberOfMessages = model.MessageCount(QueueName);
            numberOfMessages = model.MessageCount(QueueName);
            Thread.Sleep(200);
            model.BasicConsume(QueueName, autoAck: false, consumer: consumer);
            numberOfMessages = model.MessageCount(QueueName);
            numberOfMessages = model.MessageCount(QueueName);
            Thread.Sleep(200);
            numberOfMessages = model.MessageCount(QueueName);
            numberOfMessages = model.MessageCount(QueueName);
        }
    }
}
