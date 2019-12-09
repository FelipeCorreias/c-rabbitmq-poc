using RabbitMQ.Client;
using System;
using System.Text;

namespace Publisher
{
    class Program
    {
        static void Main(string[] args)
        {

            var factory = new ConnectionFactory() { HostName = "localhost", UserName= "teste", Password = "teste" };
            using (var conexao = factory.CreateConnection())
            {
                using (var canal = conexao.CreateModel()) {
                    canal.QueueDeclare(queue: "teste",
                        durable: false,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null);


                    Console.WriteLine("Escreva a mensagem:");
                    string mensagem = Console.ReadLine();

                    canal.BasicPublish(exchange: "",
                        routingKey: "teste",
                        basicProperties: null,
                        body: Encoding.UTF8.GetBytes(mensagem));

                   
                    Console.ReadLine();
                }
            }
        }
    }
}
