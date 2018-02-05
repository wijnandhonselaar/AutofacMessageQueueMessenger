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

 ## Instructions (pulling the image)
 1. Install Docker
 2. Run Docker
 3. Set docker in Windows Container mode
 4. Open Windows Powershell (as Administrator)
 5. CD to project folder
 6. Execute command: "docker run -d --name rabbitmq -e RABBITMQ_ERLANG_COOKIE='SWQOKODSQALRPCLNMEQG' -p 15672:15672 -p 5672:5672 micdenny/rabbitmq-windows"
 7. Get the containers IP, execute: docker inspect -f '{{range .NetworkSettings.Networks}}{{.IPAddress}}{{end}}' rabbitmq
 8. Execute command : "docker run -d whonselaar/messageprocessor:v1.0.1 IP_FROM_STEP_7 guest guest"
 9. Enter the RabbitMQ Server IP (localhost in this case)
 10. Enter the username for rabbitmq, guest in this case
 11. Enter your username
 12. Start messaging!

## Instructions (building the image) -- OLD NOT WORKING RIGHT NOW
 1. Install Docker
 2. Run Docker
 3. Set docker in Windows Container mode
 4. Open Windows Powershell (as Administrator)
 5. CD to project folder
 6. Execute command: "docker run -d -e RABBITMQ_ERLANG_COOKIE='SWQOKODSQALRPCLNMEQG' -p 15672:15672 -p 5672:5672 micdenny/rabbitmq-windows"
 //6. *Doesn't work with the latest Docker release. Run rabbitmq as a service or in a VM!* Execute command: "docker run -d --platform=linux --name rabbit1 -e RABBITMQ_ERLANG_COOKIE='SWQOKODSQALRPCLNMEQG' -e RABBITMQ_DEFAULT_USER=rabbitmq -e RABBITMQ_DEFAULT_PASS=rabbitmq -p 15672:15672 -p 5672:5672 rabbitmq:3-management"
 7. CD to MessageProcessor folder that contains the Dockerfile
 8. Execute command: "docker build -t messageprocessor ." (This might take a long time, it will download the microsoft/dotnet-framework:4.7.1 docker image)
 9. Execute command : "docker run -d localhost rabbitmq rabbitmq"
 10. Enter the RabbitMQ Server IP (localhost in this case)
 11. Enter your username
 12. Start messaging!
