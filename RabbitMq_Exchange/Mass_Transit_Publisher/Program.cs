using Mass_Transit_Shared.Messages;
using MassTransit;

string rabbitMqUri = "amqps://txgjwwvr:N-FhjvIXseMjQpZ4Awmw9Vg2T_gp3cMQ@moose.rmq.cloudamqp.com/txgjwwvr";

string queueName = "example-queue";

//MasTransit ile çalışılıyorsa ilk başta bu kısımda hangi sunucuda çalışıldığı ve buna ait
//configurasyonları neler bu kısımlar belirlenir 

IBusControl bus =  Bus.Factory.CreateUsingRabbitMq(factory =>
{
    factory.Host(rabbitMqUri);
});

ISendEndpoint sendEndpoint = await  bus.GetSendEndpoint(new($"{rabbitMqUri}/{queueName}"));

Console.Write("Gönderiliecek Mesaj : ");
string message =  Console.ReadLine();

await  sendEndpoint.Send<IMessage>(new ExampleMessage()
{
    Text = message
});

Console.Read();


//Mass Transit için iki farklı kullanım incelemiş olduk Baktığımızda Mass transit
//için servisler arasındaki mesaj iletimini publish ve send mantığında iki yapıda
//gerçekleştirmiş olduk 

//Publish : Event tabanlı mesaj iletim yönetimini ifade eder ,
//publish ve subscribe pattetni uygulanmaktadır Event publish edildiğinde o event a
//subscribe olan tüm queuelara mesaj iletilecektir 
//MassTransit de IPublishEndPoint referansıyla kullanılmaktadır 

//Send :  Command tabanlı mesaj iletim yönetimini ifade eder , hangi kuyruğa mesaj
//iletimi gerçekleştirilecekse endpoint olarka bildirilmesi gerekmektedir 
//MassTransit de ISendEndPointProvider referansı üzerinden GetSendEndPoint metodu
//ile mesajın gönderileceği kuruk ifade edilerek kullanılabilir
