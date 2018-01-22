# AutofacMessageQueueMessenger
Messenger that uses autofac and RabbitMQ Event Queue

## Instructions
 1. Install Docker
 2. Run Docker
 3. Open Windows Powershell (as Administrator)
 4. CD to project folder
 5. Execute command: "docker swarm init"
 6. Now deploy RabbitMQ: "docker stack deploy -c .\docker-compose.yml rabbitmq". Where rabbitmq is the name that will be given to the service.
 7. Run the Messenger
 8. Enter the RabbitMQ Server IP
 9. Enter your username
 10. Start messaging!
