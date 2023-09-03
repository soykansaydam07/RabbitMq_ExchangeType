using Mass_Transit_WorkService_Publisher.Services;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddMassTransit(configurator =>
        {
            configurator.UsingRabbitMq((context, _configurator) =>
            {
                _configurator.Host("amqps://txgjwwvr:N-FhjvIXseMjQpZ4Awmw9Vg2T_gp3cMQ@moose.rmq.cloudamqp.com/txgjwwvr");
            });
        });

        services.AddHostedService<PublishMessageService>(provider =>
        {
            using IServiceScope scope = provider.CreateScope();
            IPublishEndpoint publishEndpoint = scope.ServiceProvider.GetService<IPublishEndpoint>();
            return new(publishEndpoint);
        });
    })
    .Build();

await host.RunAsync();

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