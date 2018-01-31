using System;
using System.Threading.Tasks;
using MassTransit;

namespace Services
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
            {
                ClearCurrentConsoleLine();
                await Console.Out.WriteLineAsync(ctx.Message.Name + " - " + ctx.Message.Message);
                Console.Write(Name + " - ");
            }
                
        }

        /**
         * Filter own messages
         */
        private static bool ShowMessage(IMessage m)
        {
            return true;// Name != m.Name;
        }
        public static void ClearCurrentConsoleLine()
        {
            int currentLineCursor = Console.CursorTop;
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, currentLineCursor);
        }
    }
}
