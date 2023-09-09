using Mass_Transit_Shared.RequestResponseMessages;
using MassTransit;

Console.WriteLine("Publisher");

string rabbitMQUri = "amqps://txgjwwvr:N-FhjvIXseMjQpZ4Awmw9Vg2T_gp3cMQ@moose.rmq.cloudamqp.com/txgjwwvr";

string requestQueue = "request-queue";

IBusControl bus = Bus.Factory.CreateUsingRabbitMq(factory =>
{
    factory.Host(rabbitMQUri);
});

await bus.StartAsync();

var request = bus.CreateRequestClient<RequestMessage>(new Uri($"{rabbitMQUri}/{requestQueue}"));

int i = 1;
while (true)
{
    await Task.Delay(200);

    var response = await request.GetResponse<ResponseMessage>(new() { MessageNo = i, Text = $"{i++}. request" });
    Console.WriteLine($"Response Received : {response.Message.Text}");
}

Console.Read();

//Bu kısım da buna bağlı farklı patternlerde kullanılmakta ama bu patterler daha karmaşık olduğu için
//Bu kısımda bakmak yerine sonrasında örneklerei yapamaya devam edeceğim 