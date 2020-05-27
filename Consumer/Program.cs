using System;
using RabbitMQ.Client;
using Common;
using Newtonsoft.Json;
using System.Text;
using System.Threading;
using RabbitMQ.Client.Events;

namespace Consumer
{
    class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory
            {
                HostName = "localhost",
                UserName = "guest",
                Password = "guest"
            };

            using (var connection = factory.CreateConnection())
            {
                var channel = connection.CreateModel();
                channel.BasicQos(0, 1, false);
                var consumer = new EventingBasicConsumer(channel);

                consumer.Received += (m, ea) =>
                {
                    var message = Encoding.ASCII.GetString(ea.Body.ToArray());
                    Console.WriteLine("Message received: " + message);
                };

                while (true)
                {
                    channel.BasicConsume(Queue.Name, false, consumer);
                    Thread.Sleep(700);
                }

            }
        }
    }
}
