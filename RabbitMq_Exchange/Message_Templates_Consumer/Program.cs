using RabbitMQ.Client;
using RabbitMQ.Client.Events;
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

//EventingBasicConsumer consumer = new(channel);

//channel.BasicConsume(
//    queue: queueName,
//    autoAck: false,
//    consumer: consumer);

////Event yazımı 
//consumer.Received += (sender, e) =>
//{
//    string message = Encoding.UTF8.GetString(e.Body.Span);
//    Console.WriteLine(message);
//};

#endregion

#region Publish/Subscribe(Pub/Sub) Tasarımı

//string exchangeName = "example-pub-sub-exchange";

//channel.ExchangeDeclare(
//    exchange: exchangeName,
//    type: ExchangeType.Fanout);

//string queueName = channel.QueueDeclare().QueueName;


//channel.QueueBind(
//    queue: queueName,
//    exchange: exchangeName,
//    routingKey: string.Empty);

////Genellikle ölçeklendirme ayarı olarak da BasicQos ayarlamasıda yapılacaktır bu kısım için.

//EventingBasicConsumer consumer = new(channel);
//channel.BasicConsume(
//    queue: queueName,
//    autoAck: false,
//    consumer: consumer);


////Event yazımı 
//consumer.Received += (sender, e) =>
//{
//    string message = Encoding.UTF8.GetString(e.Body.Span);
//    Console.WriteLine(message);
//};

#endregion

#region Work/Queue(İş Kuyruğu) Tasarımı

//string queueName = "exaple-work-queue";

//channel.QueueDeclare(
//    queue: queueName,
//    durable: false,
//    exclusive: false,
//    autoDelete: false);

//EventingBasicConsumer consumer = new(channel);

//channel.BasicConsume(
//    queue: queueName,
//    autoAck: true,  // Rabbit mq verinin otomatik bir şekilde silinmesi burada bu veri
//                    // bilerek açıldı çünkü work queue tarafında her veri tek bir consuma gitmesi istendiği için 
//    consumer: consumer);

//channel.BasicQos(
//    prefetchCount:1,
//    prefetchSize:0,
//    global:false);

////Event yazımı 
//consumer.Received += (sender, e) =>
//{
//    string message = Encoding.UTF8.GetString(e.Body.Span);
//    Console.WriteLine(message);
//};

#endregion

#region Request/Response Tasarımı 



#endregion

Console.Read();