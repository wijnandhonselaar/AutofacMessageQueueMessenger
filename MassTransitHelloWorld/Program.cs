using System;
using Autofac;
using HelloServiceNet;
using MassTransit;
using IContainer = Autofac.IContainer;

namespace MessageQueueMessenger
{
    class Program
    {
        private static IContainer Container { get; set; }
        static void Main(string[] args)
        {
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
            
            //
            Console.WriteLine("Enter your name");
            var name = Console.ReadLine();
            Console.WriteLine("Connecting to message bus...");

            var busControl = ConfigureBus(name);
            busControl.Start();

            do
            {
                Console.WriteLine("Enter message (or quit to exit)");
                Console.Write("> ");
                string value = Console.ReadLine();

                if ("quit".Equals(value, StringComparison.OrdinalIgnoreCase))
                {
                    busControl.Publish<IMessage>(new
                    {
                        Name = name,
                        Message = "<< Disconnected >>"
                    });
                
                    break;
                }

                busControl.Publish<IMessage>(new
                {
                    Name = name,
                    Message = value
                });
            }
            while (true);

            busControl.StopAsync(TimeSpan.FromMilliseconds(5000));
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

        /**
         * Configure RabbitMQ Event Queue
         */
        private static IBusControl ConfigureBus(string name)
        {
            return Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                // Set sender name, to filter own messages.
                Listener.Name = name;
                // Configure connection to RabbitMQ host
                var host = cfg.Host(new Uri("rabbitmq://192.168.10.193"), h =>
                {
                    h.Username("rabbitmq");
                    h.Password("rabbitmq");
                });
                // Setup listener
                cfg.ReceiveEndpoint(host, "event_queue_" + name, e =>
                {
                    // Call the Event Consumer
                    e.Handler<IMessage>(Listener.Consume);
                });
            });
        }
    }
}
