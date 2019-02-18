using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using NServiceBus;
using NServiceBus.Logging;

namespace AsbImmediateDispatch
{
    public class DispatchHandler : IHandleMessages<KickOffDispatches>
    {
        static ILog Logger = LogManager.GetLogger<DispatchHandler>();

        public async Task Handle(KickOffDispatches message, IMessageHandlerContext context)
        {
            var stopWatch = Stopwatch.StartNew();
            var numberOfItems = 1000;
            var tasks = new List<Task>(numberOfItems);

            for (var i = 0; i < numberOfItems; i++)
            {
                tasks.Add(CreateWork(i, context));
            }

            await Task.WhenAll(tasks);

            stopWatch.Stop();

            Logger.Warn($"Done dispatch, took {stopWatch.Elapsed}");
        }

        private static Task CreateWork(int i, IMessageHandlerContext context)
        {
            var options = new SendOptions();
            options.RequireImmediateDispatch();
            options.RouteToThisEndpoint();

            return context.Send(new DoSomeWork { Work = i }, options);
        }
    }
}