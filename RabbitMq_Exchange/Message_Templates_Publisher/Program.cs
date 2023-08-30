using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

class Program
{
    static async Task Main()
    {

                ConnectionFactory factory = new();
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

        //İki kuyruk yapısı olucaktır Request/Response Queue Kısımları

        string requestQueueuName = "exaple-request-response-queue";

        channel.QueueDeclare(
            queue: requestQueueuName,
            durable: false,
            exclusive: false,
            autoDelete: false);

        string replyQueueName = channel.QueueDeclare().QueueName;

        string correlationId = Guid.NewGuid().ToString();
        //İlgili alınan requeste ait reposu bulabilmek için oluşturulan ve sorasında response verisinde
        //bu kısma bakılarak çalıştırılması sağlanacak olan unique bir Id 

        #region Request Mesajını oluşturma ve gönderme

        IBasicProperties properties =  channel.CreateBasicProperties();
        properties.CorrelationId = correlationId;
        properties.ReplyTo = replyQueueName;

        for (int i = 0; i < 10; i++)
        {
            await Task.Delay(200);

            byte[] message = Encoding.UTF8.GetBytes($"Merhaba {i}");

            channel.BasicPublish(
                exchange: string.Empty,
                routingKey: requestQueueuName,
                body: message,
                basicProperties: properties);
        }

        #endregion

        #region Response Kuyruğu Dinleme 

        EventingBasicConsumer consumer = new(channel);

        channel.BasicConsume(
            queue: replyQueueName,
            autoAck: true,  // Rabbit mq verinin otomatik bir şekilde silinmesi burada bu veri
                            // bilerek açıldı çünkü work queue tarafında her veri tek bir consuma gitmesi istendiği için 
            consumer: consumer);

        consumer.Received += (sender, e) =>
        {
            if (e.BasicProperties.CorrelationId == correlationId)
            {
                string message = Encoding.UTF8.GetString(e.Body.Span);
                Console.WriteLine($"Response  :  {message}");
            }
    
        };


        #endregion

        #endregion

        Console.Read();

    }
}