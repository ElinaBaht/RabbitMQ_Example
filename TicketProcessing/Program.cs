using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

Console.WriteLine("Hello to rabbitConnection!");

var factory = new ConnectionFactory()
{
    HostName = "localhost",
    UserName = "user",
    Password = "password",
    VirtualHost = "/"
};

var connection = factory.CreateConnection();
using var channel = connection.CreateModel();

channel.QueueDeclare("bookings", durable: true, exclusive: false);

var consumer = new EventingBasicConsumer(channel);

consumer.Received += (model, eventArgs) =>
{
    var body = eventArgs.Body.ToArray();
    var message = Encoding.UTF8.GetString(body);

    Console.WriteLine($"New ticket - {message}");
};

channel.BasicConsume("bookings", true, consumer);

Console.ReadKey();
