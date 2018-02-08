using System;
using MassTransit;
using MessengerPackage;

namespace MessageProcessor
{
    public static class MessageProcessor
    {
        public static IBusControl BusControl { get; set; }
        private static string _address, _username, _password;

        static void Main(string[] args)
        {
            _address = args[0];
            _username = args[1];
            _password = args[2];
            BusControl = ConfigureBus();
            BusControl.Start();
            Console.WriteLine(BusControl.Address.AbsoluteUri);
            Console.WriteLine("Message processor is running, press any key to exit");
            Console.ReadLine();
            BusControl.StopAsync();
        }

        /**
         * Configure RabbitMQ Event Queue
         */
        private static IBusControl ConfigureBus()
        {
            return Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                // Configure connection to RabbitMQ host 192.168.10.193
                var host = cfg.Host(new Uri($"rabbitmq://{_address}"), h =>
                {
                    h.Username(_username);
                    h.Password(_password);
                });

                // Setup listener
                // Each messenger gets its own queue, events can only be consumed once from a queue.
                cfg.ReceiveEndpoint(host, "message_queue", e =>
                {
                    // Call the Event Consumer
                    e.Handler<IUnprocessedMessage>(Listener.Consume);
                });
            });
        }
    }
}
