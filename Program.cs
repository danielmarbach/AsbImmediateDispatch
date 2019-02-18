using System;
using System.Threading.Tasks;
using NServiceBus;
using NServiceBus.Logging;

namespace AsbImmediateDispatch
{
    class Program
    {
        static async Task Main(string[] args)
        {
            LogManager.Use<DefaultFactory>().Level(LogLevel.Info);

            var endpointConfiguration = new EndpointConfiguration("asbimmediatedispatch");
            endpointConfiguration.EnableInstallers();

            endpointConfiguration.UseSerialization<XmlSerializer>(); // use built in for now because it doesn't matter here

            var transport = endpointConfiguration.UseTransport<AzureServiceBusTransport>();
            transport.UseForwardingTopology();
            transport.ConnectionString(Environment.GetEnvironmentVariable("AzureServiceBus_ConnectionString"));

            var endpoint = await Endpoint.Start(endpointConfiguration);

            await endpoint.SendLocal(new KickOffDispatches());

            Console.ReadLine();

            await endpoint.Stop();
        }
    }
}
