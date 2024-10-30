# RRBank Project

## General Description

This project is a simulated digital bank, developed as part of a portfolio to test technologies like Ocelot, RabbitMQ, Redis, Entity Framework Core, and Docker. The system consists of an **API Gateway** (Ocelot) and two main APIs: **Client** and **Manager**. The project uses Docker to orchestrate the containers and ensure consistent service execution across different environments.

## Technologies Used

- **Ocelot**: API Gateway used for routing and load balancing between the Client and Manager APIs.
- **Docker** and **Docker Compose**: Tools used to containerize the services, ensuring portability and consistency across different environments.
- **Entity Framework Core**: ORM (Object-Relational Mapping) for interacting with the SQL database, applying migrations to manage the schema.
- **RabbitMQ**: Messaging system for asynchronous communication between services.
- **Redis**: Used for caching, improving performance by storing frequently accessed data in memory.
- **Swagger**: Interactive documentation interface for easily testing and exploring the APIs.
- **Unit Tests**: Written to validate code behavior and ensure quality.

## Setup and Dependencies

This project uses **.NET 8**, and all major dependencies are encapsulated in Docker containers, simplifying the setup. However, if you wish to run unit tests, you may need to have the .NET SDK installed locally.

### Prerequisites

- **Docker**: [Install Docker](https://docs.docker.com/get-docker/)
- **Docker Compose**: [Install Docker Compose](https://docs.docker.com/compose/install/)
- **.NET SDK 8** (only if you want to run tests locally outside of Docker): [Install .NET SDK](https://dotnet.microsoft.com/download/dotnet/8.0)

## Project Setup

To run the project locally, follow the steps below:

1. Clone the repository:
   ```bash
   git clone https://github.com/RochaRaphael/RRBank

## Start the Docker services:
docker-compose up --build

## Access Swagger to test the APIs:
Gateway (Ocelot): http://localhost:8084/swagger

## Run migrations to create the database:
dotnet ef migrations add InitialCreate --project ../RRBank.Infra
dotnet ef database update

## After these steps, the project will be ready for use, and the APIs will be accessible via the API Gateway.

## Internal Services
The project has two additional services that are used in certain operations:

### Redis: Used as a cache to store client data in memory and speed up repeated queries.
### RabbitMQ: Used for asynchronous communication between services, especially for tasks involving message queues.
Both services are configured in Docker Compose and will be automatically provisioned when running the docker-compose up command.


Português[PT/BR]
## Descrição Geral

Este projeto é um banco digital simulado, desenvolvido como parte de um portfólio para testar tecnologias como Ocelot, RabbitMQ, Redis, Entity Framework Core e Docker. O sistema é composto por um **API Gateway** (Ocelot) e duas APIs principais: **Client** e **Manager**. O projeto utiliza Docker para orquestrar os contêineres e garantir a execução padronizada dos serviços.

## Tecnologias Utilizadas

- **Ocelot**: Gateway de API usado para roteamento e balanceamento de carga entre as APIs Client e Manager.
- **Docker** e **Docker Compose**: Ferramentas utilizadas para containerizar os serviços, garantindo portabilidade e consistência em diferentes ambientes.
- **Entity Framework Core**: ORM (Object-Relational Mapping) para trabalhar com o banco de dados SQL, aplicando migrations para gerenciar o esquema.
- **RabbitMQ**: Sistema de mensageria para comunicação assíncrona entre os serviços.
- **Redis**: Utilizado para caching, melhorando a performance ao armazenar dados frequentemente acessados em memória.
- **Swagger**: Interface de documentação interativa para testar e explorar as APIs de maneira fácil e eficiente.
- **Testes Unitários**: Escritos para validar o comportamento do código e garantir a qualidade.

## Setup e Dependências

Este projeto utiliza **.NET 8** e todas as dependências principais estão encapsuladas em contêineres Docker, o que simplifica a configuração. No entanto, se você deseja executar os testes unitários, pode ser necessário ter o SDK do .NET instalado localmente.

### Pré-requisitos

- **Docker**: [Instalar Docker](https://docs.docker.com/get-docker/)
- **Docker Compose**: [Instalar Docker Compose](https://docs.docker.com/compose/install/)
- **.NET SDK 8** (apenas se você pretende rodar os testes localmente fora do Docker): [Instalar .NET SDK](https://dotnet.microsoft.com/download/dotnet/8.0)

## Configuração do Projeto

Para executar o projeto localmente, siga os passos abaixo:

1. Clone o repositório:
   ```bash
   git clone https://github.com/RochaRaphael/RRBank

### Suba os serviços Docker:
docker-compose up --build

## Acesse o Swagger para testar as APIs:
Gateway (Ocelot): http://localhost:8084/swagger

## Execute as migrations para criar o banco de dados:
dotnet ef migrations add InitialCreate --project ../RRBank.Infra
dotnet ef database update

## Após esses passos, o projeto estará pronto para uso, e as APIs estarão acessíveis via o API Gateway.

## Serviços Internos
O projeto conta com dois serviços adicionais que são utilizados em algumas operações:

### Redis: Usado como cache para armazenar dados de clientes em memória e acelerar consultas repetidas.
### RabbitMQ: Usado para comunicação assíncrona entre os serviços, especialmente para tarefas que envolvem filas de mensagens.
Ambos os serviços estão configurados no Docker Compose e serão automaticamente provisionados na execução do comando docker-compose up.
