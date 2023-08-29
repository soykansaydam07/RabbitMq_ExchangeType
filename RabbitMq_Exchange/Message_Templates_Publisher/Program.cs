using RabbitMQ.Client;
using System.Text;

ConnectionFactory factory = new ConnectionFactory();
factory.Uri = new("amqps://txgjwwvr:N-FhjvIXseMjQpZ4Awmw9Vg2T_gp3cMQ@moose.rmq.cloudamqp.com/txgjwwvr");
//Bu Uri kısmı rabbit mq için oluşturulmuş Url olup kuyruk işlemleri için kullanılmaktadır 

using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();

#region P2P(PointToPoint) Tasarımı

//string queueName = "exaple-p2p-queue";

//channel.QueueDeclare(
//    queue: queueName,
//    durable: false,
//    exclusive: false,
//    autoDelete: false);

//byte[] byteMessage = Encoding.UTF8.GetBytes("merhaba");

//channel.BasicPublish(
//    exchange: string.Empty,
//    routingKey: queueName,
//    body: byteMessage);


#endregion

#region Publish/Subscribe(Pub/Sub) Tasarımı

//string exchangeName = "example-pub-sub-exchange";

//channel.ExchangeDeclare(
//    exchange: exchangeName,
//    type: ExchangeType.Fanout);


//for (int i = 0; i < 100; i++)
//{ 
//    await Task.Delay(200);

//    byte[] message = Encoding.UTF8.GetBytes($"Merhaba {i}");

//    channel.BasicPublish(
//        exchange: exchangeName,
//        routingKey: string.Empty,
//        body: message);

//}
#endregion

#region Work/Queue(İş Kuyruğu) Tasarımı

//string queueName = "exaple-work-queue";

//channel.QueueDeclare(
//    queue: queueName,
//    durable: false,
//    exclusive: false,
//    autoDelete: false);

//for (int i = 0; i < 100; i++)
//{
//    await Task.Delay(200);

//    byte[] message = Encoding.UTF8.GetBytes($"Merhaba {i}");

//    channel.BasicPublish(
//        exchange: string.Empty,
//        routingKey: queueName,
//        body: message);

//}

#endregion

#region Request/Response Tasarımı 



#endregion

Console.Read();