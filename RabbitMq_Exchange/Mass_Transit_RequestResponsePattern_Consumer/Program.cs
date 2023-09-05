using MassTransit;
using Mass_Transit_Shared.RequestResponseMessages;
using Mass_Transit_RequestResponsePattern_Consumer.Consumers;

Console.WriteLine("Consumer");

string rabbitMQUri = "amqps://txgjwwvr:N-FhjvIXseMjQpZ4Awmw9Vg2T_gp3cMQ@moose.rmq.cloudamqp.com/txgjwwvr";

string requestQueue = "request-queue";

IBusControl bus = Bus.Factory.CreateUsingRabbitMq(factory =>
{
    factory.Host(rabbitMQUri);

    factory.ReceiveEndpoint(requestQueue, endpoint =>
    {
        endpoint.Consumer<ReqeustMessageConsumer>();
    });
});

await bus.StartAsync();

Console.Read();