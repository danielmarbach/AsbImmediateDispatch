using System.Threading.Tasks;
using NServiceBus;
using NServiceBus.Logging;

namespace AsbImmediateDispatch
{
    public class WorkHandler : IHandleMessages<DoSomeWork>
    {
        private static ILog Logger = LogManager.GetLogger<WorkHandler>();

        public Task Handle(DoSomeWork message, IMessageHandlerContext context)
        {
            Logger.Info(message.Work.ToString());
            return Task.CompletedTask;
        }
    }
}