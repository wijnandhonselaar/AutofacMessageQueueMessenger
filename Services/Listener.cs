using System;
using System.Threading.Tasks;
using MassTransit;
using MessengerPackage;

namespace Services
{
    public static class Listener
    {
        public static string Name = "";

        /**
         * Consume message from the Event Queue
         */
        public static Task Consume(ConsumeContext<IMessage> ctx)
        {
            if (!ShowMessage(ctx.Message)) return Task.CompletedTask;

            ClearCurrentConsoleLine();
            Console.WriteLine(ctx.Message.Name + " - " + ctx.Message.Message);
            Console.Write(Name + " - ");

            return Task.CompletedTask;
        }

        /**
         * Filter own messages
         */
        private static bool ShowMessage(IMessage m)
        {
            return Name != m.Name;
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