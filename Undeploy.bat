docker-compose -f Microservice.Query.Api\docker-compose.yml -f Microservice.Query.Api\docker-compose.override.yml down
docker-compose -f Microservice.Producer.Api\docker-compose.yml -f Microservice.Producer.Api\docker-compose.override.yml down
docker-compose -f Microservice.Consumer.Worker\docker-compose.yml -f Microservice.Consumer.Worker\docker-compose.override.yml down
docker-compose -f Infrastructure\docker-compose.yml down
pause