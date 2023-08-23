using RabbitMQ.Client;
using System.Text;

ConnectionFactory factory = new ConnectionFactory();
factory.Uri = new("amqps://txgjwwvr:N-FhjvIXseMjQpZ4Awmw9Vg2T_gp3cMQ@moose.rmq.cloudamqp.com/txgjwwvr");

using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();



channel.ExchangeDeclare(exchange: "header-exchange-example", type: ExchangeType.Headers);

for (int i = 0; i < 100; i++)
{
    await Task.Delay(200);

    byte[] message = Encoding.UTF8.GetBytes($"Merhaba {i}");

    Console.Write("Mesajın gönderelicei headers value değerlerini  belirtiniz  : ");
    string value = Console.ReadLine();

    IBasicProperties basicProperties = channel.CreateBasicProperties();

    basicProperties.Headers = new Dictionary<string, object>
    {
        ["no"] =  value
    };

    channel.BasicPublish
        (exchange: "header-exchange-example",
        routingKey: string.Empty,
        body: message , 
        basicProperties:basicProperties);
}

Console.Read();