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
            Console.WriteLine($"Received: {ctx.Message.Name}: {ctx.Message.Message}");
            IProcessedMessage message = new ProcessedMessage(ctx.Message.Name, ctx.Message.Message);
            Console.WriteLine($"Sent: {message.Name}: {message.Message}");
            await MessageProcessor.BusControl.Publish(message);
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
