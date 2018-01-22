# AutofacMessageQueueMessenger
Messenger that uses autofac and RabbitMQ Event Queue

## Instructions
 1. Install Docker
 2. Run Docker
 3. Open Windows Powershell (as Administrator)
 4. CD to project folder
 5. Execute command: "docker pull rabbitmq:3-management"
 6. Execute command: "docker swarm init"
 7. Now deploy RabbitMQ: "docker stack deploy -c .\docker-compose.yml rabbitmq". Where rabbitmq is the name that will be given to the service.
 8. Run the Messenger
 9. Enter the RabbitMQ Server IP
 10. Enter your username
 11. Start messaging!
