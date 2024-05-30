# Drive

Project which simulates online carsharing service.

## Technologies and project structure

### Backend
- Only ASP.NET and C#
- Evolution from Monolyth to "Microservice style"
- Grpc, pure http, bus (rabbitMq) for communication
- Both REST and GraphQL api
- SignalR and Grpc chat

### Frontend
- React.js for admin panel and main site
- web-grpc for online chat

### Mobile 
- Flutter mobile app
- bloc for handling logic

### Infrastructure
- Docker as containerization
- PostgreSql, MongoDB, Redis, ClickHouse, MinIO S3 as storages
- RabbitMQ as transport
- SonaR and eslint pipline as part of CI

## Start up
- Use compose.yml file to up containers for backend and frontend
- mobile application is set up to work from emulator on machine, which is hosting backend
- You must manually create "carsharing" database in ClickHouse before starting Statistics project
- You must register at least one user, and then add "Admin" role and claim to him manually, as there is no any root user

## Commentary
Project was created and devoloped as study project. Project had 3 iterations with tasks, and code base was changing due those tasks. At first it was designed as monolyth system, but then (according to requirements) was splitted to "microservice" like system (not completely microservices). Finally, project allows you to choose select car and book it, to have conversation with support, monitor and edit your profile either from mobileapp or from website. You can download documents or open "occasion" question from site. You can monitor some daily statistics from mobileapp. You can manage system from admin website.