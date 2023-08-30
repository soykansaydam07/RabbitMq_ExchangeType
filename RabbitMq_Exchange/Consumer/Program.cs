using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

ConnectionFactory factory = new();
factory.Uri = new("amqps://txgjwwvr:N-FhjvIXseMjQpZ4Awmw9Vg2T_gp3cMQ@moose.rmq.cloudamqp.com/txgjwwvr");

using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();

channel.ExchangeDeclare(exchange: "direct-exchange-example", type: ExchangeType.Direct);
//Publisher daki exchange detype isim ile birebir aynı olmalıdır 

string queueName = channel.QueueDeclare().QueueName;
//Publisher tarafından routing key de bulunana değerdeki kuyruğa gönderilen mesajları
//kendi oluşturduğumuz kuyruğa tüketmemiz gerekmektedir , Bunun için öncelikle bir kuyruk oluşturulmalı 
//Bu şekilde kullanım ile genrete edilecek olan kuruğun ismine erişmiş olmaktayız 

channel.QueueBind(queue: queueName, exchange: "direct-exchange-example", routingKey: "direct-queue-example");

EventingBasicConsumer consumer = new(channel);
channel.BasicConsume(queue: queueName, autoAck: true,consumer: consumer);

//Event yazımı 
consumer.Received += (sender, e) =>
{
    string message = Encoding.UTF8.GetString(e.Body.Span);
    Console.WriteLine(message);
};

//Aşağıdaki yapının uzun yolu olup yukarısında kısa olarak yazılmış hali bulunmaktafır 

//consumer.Received += Consumer_Received;

//void Consumer_Received(object? sender, BasicDeliverEventArgs e)
//{
//    throw new NotImplementedException();
//}

Console.Read();