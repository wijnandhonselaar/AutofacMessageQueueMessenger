using System;
using Autofac;
using Services;
using MassTransit;
using MessengerPackage;

namespace MessageQueueMessenger
{
    class Program
    {
        private static IContainer Container { get; set; }
        private static IBusControl BusControl { get; set; }

        static void Main(string[] args)
        {
            string name;
            string username;
            // Using Autofac to autoinject classes
            var builder = new ContainerBuilder();
            // Register interface for autoinjection when ConsoleOuput is instantiated.
            builder.RegisterType<ConsoleOutput>().As<IOutput>();
            // Register interface for autoinjection when TodayWriter is instantiated.
            builder.RegisterType<TodayWriter>().As<IDateWriter>();
            // Build container to create the register
            Container = builder.Build();
            // Execute function that uses the Autoinjected classes
            WriteDate();

            // Connect to the RabbitMQ server
            do
            {
                Console.WriteLine("Enter RabbitMQ Server IPAddress like so: 127.0.0.1");
                var address = Console.ReadLine();
                Console.WriteLine("Enter RabbitMQ Server username");
                username = Console.ReadLine();
                Console.WriteLine("Enter your name");
                name = Console.ReadLine();
                Console.WriteLine("Connecting to message bus...");
                BusControl = ConfigureBus(username, name, address);
                try
                {
                    BusControl.Start();
                    // Success! So break out of the loop.
                    break;
                }
                catch { Console.WriteLine("Error connecting to the RabbitMQ server on given IP, please try again!");}

            } while (true);
            // Start the messenger
            Console.WriteLine("Enter message (or quit to exit)");
            do
            {
                Console.Write(name + " - ");
                var message = Console.ReadLine();

                if ("quit".Equals(message, StringComparison.OrdinalIgnoreCase))
                {
                    Publish(name, "<< Disconnected >>");
                    break;
                }

                Publish(name, message);
            }
            while (true);

            BusControl.Stop();
        }

        /**
         *  Write current date to the console.
         */
        public static void WriteDate()
        {
            using (var scope = Container.BeginLifetimeScope())
            {
                var writer = scope.Resolve<IDateWriter>();
                writer.WriteDate();
            }
        }

        private static void Publish(string name, string message)
        {
            BusControl.Publish<IUnprocessedMessage>(new
            {
                Name = name,
                Message = message
            });
        }

        /**
         * Configure RabbitMQ Event Queue
         */
        private static IBusControl ConfigureBus(string username, string name, string address)
        {
            return Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                Listener.Name = name;
                // Configure connection to RabbitMQ host 192.168.10.193
                var host = cfg.Host(new Uri("rabbitmq://" + address), h =>
                {
                    h.Username(username);
                    h.Password(username);
                });

                // Setup listener
                // Each messenger gets its own queue, events can only be consumed once from a queue.
                cfg.ReceiveEndpoint(host, "message_queue_" + name, e =>
                {
                    // Setup a listener for Processed Messages
                    e.Handler<IProcessedMessage>(Listener.Consume);
                });
            });
        }
    }
}
