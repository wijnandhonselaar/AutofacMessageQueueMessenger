# AutofacMessageQueueMessenger
Messenger that uses autofac and RabbitMQ Event Queue with the Masstransit Framework

## Version 1.1
### Features
 - Messages are received by an Message Processor, which processes the message and sends it to all connected chat clients.
 - The Message Processor runs in a Docker container
 - Client is .NET Framework 4.6.2 based

## Version 1.0
### Features
 - Sending messages via a Message Bus
 - Client is .NET Framework 4.6.2 based

 ## Instructions
 1. Install Docker
 2. Run Docker
 3. Set docker in Windows Container mode
 4. Open Windows Powershell (as Administrator)
 5. CD to project folder
 6. Execute command: `docker run -d --name rabbitmq -e RABBITMQ_ERLANG_COOKIE='SWQOKODSQALRPCLNMEQG' -p 15672:15672 -p 5672:5672 micdenny/rabbitmq-windows`
 7. CD to MessageProcessor folder
 8. Execute command `docker build --rm -t messageprocessor .`
 9. Get the RabbitMQ IP, execute: `docker inspect -f '{{range .NetworkSettings.Networks}}{{.IPAddress}}{{end}}' rabbitmq`
 10. Execute command : `docker run -d --name messageprocessor messageprocessor:latest IP_FROM_STEP_9 guest guest`
 11. Open the visual studio solution and pres F5 to run the chat-client
 9. Enter the RabbitMQ Server IP (IP_FROM_STEP_9)
 10. Enter the username for rabbitmq, guest in this case
 11. Enter your username
 12. Start messaging!
 
 ## Messageprocessor as a Service!
 To speed up the message processing you can use the docker service features. To make the change in performance visible you can open up the RabbitMQ management dashboard by going into the browser to: `IP_FROM_STEP_9:15672`. The following instructions will show you how:
 1. Stop the `messageprocessor` container
 2. Execute command: `docker service create -t --name processors messageprocessor IP_FROM_STEP_9 guest guest`
 3. To scale up and down execute command: `docker service scale processors=5`
 
 ## Version 1.1 Diagrams
 Class diagram:
 ![Class diagram](https://raw.githubusercontent.com/wijnandhonselaar/EventDrivenMessenger/archive/V1.1/Images/EventDrivenMessenger.png)
 Sequence diagram:
 ![Sequence diagram](https://raw.githubusercontent.com/wijnandhonselaar/EventDrivenMessenger/archive/V1.1/Images/Message%20life%20cycle.png)
