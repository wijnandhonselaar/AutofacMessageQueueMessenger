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
 6. Execute command: "docker run -d --platform=linux --name rabbit1 -e RABBITMQ_ERLANG_COOKIE='SWQOKODSQALRPCLNMEQG' -e RABBITMQ_DEFAULT_USER=rabbitmq -e RABBITMQ_DEFAULT_PASS=rabbitmq -p 15672:15672 -p 5672:5672 rabbitmq:3-management"
 7. CD to MessageProcessor folder that contains the Dockerfile
 8. Execute command: "docker build -t messageprocessor ." (This might take a long time, it will download the microsoft/dotnet-framework:4.7.1 docker image)
 9. Execute command : "docker run -d --name messageprocessor1 -e localhost -e rabbitmq -e rabbitmq"
 10. Enter the RabbitMQ Server IP (localhost in this case)
 11. Enter your username
 12. Start messaging!
