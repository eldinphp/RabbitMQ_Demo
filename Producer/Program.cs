using System;
using RabbitMQ.Client;
using Common;
using Newtonsoft.Json;
using System.Text;
using System.Threading;

namespace Producer
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
                channel.QueueDeclare(Queue.Name, false, false, false, null);

                while (true)
                {
                    var message = new Message
                    {
                        Date = new DateTime(),
                        Text = "The message with ID " + new Random().Next(100,999) + " has been posted.",
                        Processed = false
                    };
                    var byteArrayMessage = Encoding.ASCII.GetBytes(JsonConvert.SerializeObject(message));
                    channel.BasicPublish("", Queue.Name, null, byteArrayMessage);
                    Console.WriteLine(message.Text);
                    Thread.Sleep(700);
                }

            }

        }
    }
}
