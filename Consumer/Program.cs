using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace Consumer
{
    class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory() { HostName = "localhost", UserName = "teste", Password = "teste" };
            using (var conexao = factory.CreateConnection())
            {
                using (var canal = conexao.CreateModel())
                {
                    canal.QueueDeclare(queue: "teste",
                        durable: false,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null);

                    var consumidor = new EventingBasicConsumer(canal);
                    consumidor.Received += (modeal, ea) =>
                    {
                        try
                        {
                            var body = ea.Body;
                            var mensagem = Encoding.UTF8.GetString(body);
                            Console.WriteLine(mensagem);
                            canal.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
                        }
                        catch (Exception)
                        {
                            throw;
                        }
                       
                    };

                    canal.BasicConsume(queue: "teste",
                        autoAck: false,
                        consumer: consumidor);

                    Console.WriteLine("Consumidor rodando...");
                    Console.ReadLine();
                }
            }
        }
    }
}
