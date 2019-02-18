using NServiceBus;

namespace AsbImmediateDispatch
{
    public class DoSomeWork : ICommand
    {
        public int Work { get; set; }
    }
}