using System;
using System.Configuration;
using MassTransit;
using MessengerPackage;

namespace MessageProcessor
{
    public static class MessageProcessor
    {
        public static IBusControl BusControl { get; set; }

        static void Main(string[] args)
        {
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
            string address = ConfigurationManager.AppSettings["RabbitMQHost"],
                username = ConfigurationManager.AppSettings["RabbitMQHostUsername"],
                password = ConfigurationManager.AppSettings["RabbitMQHostPassword"];
            return Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                // Configure connection to RabbitMQ host 192.168.10.193
                var host = cfg.Host(new Uri($"rabbitmq://{address}"), h =>
                {
                    h.Username(username);
                    h.Password(password);
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
