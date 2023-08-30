//Sistem de örnek yapılırken iki tane publisher 5 tane consumer olarak işlem yapacağız .
//Topicler ile ilgili olara Topic formutu oluşturara gidilmesi gerekmektedir #,.,* gibi yapılar kullanılmalıdır  

using RabbitMQ.Client;
using System.Text;

ConnectionFactory factory = new();
factory.Uri = new("amqps://txgjwwvr:N-FhjvIXseMjQpZ4Awmw9Vg2T_gp3cMQ@moose.rmq.cloudamqp.com/txgjwwvr");

using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();

channel.ExchangeDeclare(exchange: "topic-exchange-example", type:ExchangeType.Topic);


for (int i = 0; i < 100; i++)
{
    await Task.Delay(200);

    byte[] message = Encoding.UTF8.GetBytes($"Merhaba {i}");

    Console.Write("Mesajın gönderelicei topic formatını belirtiniz  : ");
    string topic = Console.ReadLine();


    channel.BasicPublish(exchange: "topic-exchange-example", routingKey: topic, body: message);
}

Console.Read();