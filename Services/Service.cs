using System;
using System.Threading.Tasks;
using MassTransit;

namespace HelloServiceNet
{
    public interface IOutput
    {
        void Write(string content);
    }

    public class ConsoleOutput : IOutput
    {
        public void Write(string content)
        {
            Console.WriteLine(content);
        }
    }

    public interface IDateWriter
    {
        void WriteDate();
    }

    public class TodayWriter : IDateWriter
    {
        private readonly IOutput _output;

        public TodayWriter(IOutput output)
        {
            _output = output;
        }

        public void WriteDate()
        {
            _output.Write(DateTime.Today.ToShortDateString());
        }
    }

    /**
     * Message interface used for events
     */
    public interface IMessage
    {
        string Name { get; set; }
        string Message { get; set; }
    }

    /**
     * Listener to process events from the Event Queue
     */
    public static class Listener
    {
        public static string Name = "";

        /**
         * Consume message from the Event Queue
         */
        public static async Task Consume(ConsumeContext<IMessage> ctx)
        {
            if (ShowMessage(ctx.Message))
                await Console.Out.WriteLineAsync(ctx.Message.Name + " - " + ctx.Message.Message);
        }

        /**
         * Filter own messages
         */
        private static bool ShowMessage(IMessage m)
        {
            return Name != m.Name;
        }
    }
}
