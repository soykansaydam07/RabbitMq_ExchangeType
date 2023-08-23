
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

ConnectionFactory factory = new ConnectionFactory();
factory.Uri = new("amqps://txgjwwvr:N-FhjvIXseMjQpZ4Awmw9Vg2T_gp3cMQ@moose.rmq.cloudamqp.com/txgjwwvr");

using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();


channel.ExchangeDeclare(exchange: "topic-exchange-example", type: ExchangeType.Topic);

Console.Write("Dinlenecek topic formatını belirtiniz  : ");
string topic = Console.ReadLine();

string queueName = channel.QueueDeclare().QueueName;

channel.QueueBind(queue: queueName, exchange: "topic-exchange-example", routingKey: topic);

EventingBasicConsumer consumer = new EventingBasicConsumer(channel);
channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);

//Event yazımı 
consumer.Received += (sender, e) =>
{
    string message = Encoding.UTF8.GetString(e.Body.Span);
    Console.WriteLine(message);
};

Console.Read();