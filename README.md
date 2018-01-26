# AutofacMessageQueueMessenger
Messenger that uses autofac and RabbitMQ Event Queue with Masstransit Framework

## Instructions
 1. Install Docker
 2. Run Docker
 3. Open Windows Powershell (as Administrator)
 4. CD to project folder
 5. Execute command: "docker run -d --name rabbit1 -e RABBITMQ_ERLANG_COOKIE='SWQOKODSQALRPCLNMEQG' -e RABBITMQ_DEFAULT_USER=rabbitmq -e RABBITMQ_DEFAULT_PASS=rabbitmq -p 15672:15672 -p 5672:5672 rabbitmq:3-management"
 6. Run the Messenger
 7. Enter the RabbitMQ Server IP (localhost in this case)
 8. Enter your username
 9. Start messaging!
