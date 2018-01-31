using System;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using MessengerPackage;

namespace MessageProcessor
{
    /**
     * Listener to process events from the Event Queue
     */
    public class Listener
    {
        /**
         * Consume message from the Event Queue
         */
        public static async Task Consume(ConsumeContext<IMessage> ctx)
        {
//            if (Terminated(ctx.Message))
//            {
//                MessageProcessor.BusControl.StopAsync();
//            }
//            else
//            {
            Console.WriteLine($"{ctx.Message.Name}: {ctx.Message.Message}");
            IProcessedMessage message = new ProcessedMessage(ctx.Message.Name, ctx.Message.Message);
            Console.WriteLine($"{message.Name}: {message.Message}");
            await MessageProcessor.BusControl.Publish(message);
//            }
        }

        private static bool Terminated(IMessage m)
        {
            return m.Message.Equals("close session", StringComparison.InvariantCultureIgnoreCase);
        }
    }

    public class ProcessedMessage : IProcessedMessage
    {
        public string Name { get; set; }
        public string Message { get; set; }

        public ProcessedMessage(string name, string message)
        {
            Name = name;
            Message = message;
        }
    }
}
