using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

ConnectionFactory factory = new();
factory.Uri = new("amqps://txgjwwvr:N-FhjvIXseMjQpZ4Awmw9Vg2T_gp3cMQ@moose.rmq.cloudamqp.com/txgjwwvr");

using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();

channel.ExchangeDeclare(exchange: "fanout_exchange-example", type: ExchangeType.Fanout);

Console.Write("Kuyruk Adını Giriniz : ");
string queueName = Console.ReadLine();

channel.QueueDeclare(queue: queueName, exclusive: false);

channel.QueueBind(queue: queueName, exchange: "fanout_exchange-example", routingKey: string.Empty);

EventingBasicConsumer consumer = new(channel);
channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);

//Event yazımı 
consumer.Received += (sender, e) =>
{
    string message = Encoding.UTF8.GetString(e.Body.Span);
    Console.WriteLine(message);
};

Console.Read();