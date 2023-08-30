using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Security.Cryptography.X509Certificates;
using System.Text;

ConnectionFactory factory = new();
factory.Uri = new("amqps://txgjwwvr:N-FhjvIXseMjQpZ4Awmw9Vg2T_gp3cMQ@moose.rmq.cloudamqp.com/txgjwwvr");

using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();

channel.ExchangeDeclare(exchange: "header-exchange-example", type: ExchangeType.Headers);

Console.Write("Mesajın gönderelicei headers value değerlerini  belirtiniz  : ");
string value = Console.ReadLine();

string queueName = channel.QueueDeclare().QueueName;

//x-match : İlgili queueu nun mesajı hangi davranışla alacağının kararını veren bir key dir  (İki veri alır)
//any : Sadece header daki bir değerin olması yeterli iken all : tün key value değerlerinin eşleşmesi lazımdır

channel.QueueBind(
    queue: queueName,
    exchange: "header-exchange-example",
    routingKey: string.Empty,
    new Dictionary<string, object>
    {
        //["x-match"] = "all",      // Yapı olarak bu şekilde kullanılmakta 
        ["no"] = value
    });


EventingBasicConsumer consumer = new EventingBasicConsumer(channel);
channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);

//Event yazımı 
consumer.Received += (sender, e) =>
{
    string message = Encoding.UTF8.GetString(e.Body.Span);
    Console.WriteLine(message);
};

Console.Read();