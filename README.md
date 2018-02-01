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
 3. Open Windows Powershell (as Administrator)
 4. CD to project folder
 5. Execute command: "docker run -d --platform=linux --name rabbit1 -e RABBITMQ_ERLANG_COOKIE='SWQOKODSQALRPCLNMEQG' -e RABBITMQ_DEFAULT_USER=rabbitmq -e RABBITMQ_DEFAULT_PASS=rabbitmq -p 15672:15672 -p 5672:5672 rabbitmq:3-management"
 6. CD to MessageProcessor folder that contains the Dockerfile
 7. Execute command: "docker build -t messageprocessor ." (This might take a long time, it will download the microsoft/dotnet-framework:4.7.1 docker image)
 8. Execute command : "docker run -d --name messageprocessor1 -e localhost -e rabbitmq -e rabbitmq"
 8. Enter the RabbitMQ Server IP (localhost in this case)
 9. Enter your username
 10. Start messaging!
