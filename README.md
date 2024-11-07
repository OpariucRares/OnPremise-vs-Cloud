<!-- Table of Contents -->

# :notebook_with_decorative_cover: Table of Contents

- [About the Project](#star2-about-the-project)
  - [Tech Stack](#space_invader-tech-stack)
  - [Features](#dart-features)
  - [Environment Variables](#key-environment-variables)
- [Getting Started](#toolbox-getting-started)
  - [Run Locally](#running-run-locally)
  - [Deployment](#triangular_flag_on_post-deployment)
- [License](#warning-license)
- [Contact](#handshake-contact)
- [Acknowledgements](#gem-acknowledgements)

<!-- About the Project -->

## :star2: About the Project

This is an exemple of an app which is built on the Microservices Architecure. We will show you how to built it and debug it locally.

<!-- TechStack -->

### :space_invader: Tech Stack

<details>
  <summary>Server</summary>
  <ul>
    <li><a href="https://dotnet.microsoft.com/en-us/download/dotnet/8.0">.NET</a></li>
  </ul>
</details>

<details>
<summary>Database</summary>
  <ul>
    <li><a href="https://www.postgresql.org/">PostgreSQL</a></li>
  </ul>
</details>

<details>
<summary>DevOps</summary>
  <ul>
    <li><a href="https://www.docker.com/">Docker Desktop</a></li>
  </ul>
</details>

<!-- Features -->

### :dart: Features

- There are two main APIs (ProductService and OrderService) which implement the CRUD operations (Get all the items, get an item by id, create, update, delete)
- These microservices can be executed using Docker and Docker Compose
- Keep in mind, if you want to access swagger when you execute in docker, you need to comment in the Program.cs file from each microservice. It is about the if where checks if it is in development mode. In the docker runtime, they are published in release mode, not debug.

<!-- Env Variables -->

### :key: Environment Variables

To run this project, you will need to add the following environment variables to your .env file. This is used for the connection of the databse. Each microservice has an .env file.

# .env

`POSTGRES_DB`

`POSTGRES_USER`

`POSTGRES_PASSWORD`

`CONNECTION_STRING_MS_VISUAL_STUDIO=Host=localhost;Port=5432;Database=${POSTGRES_DB};Username=${POSTGRES_USER};Password=${POSTGRES_PASSWORD};`

`CONNECTION_STRING_DOCKER=Host=host.docker.internal;Port=5432;Database=${POSTGRES_DB};Username=${POSTGRES_USER};Password=${POSTGRES_PASSWORD};`

<!-- Getting Started -->

## :toolbox: Getting Started

<!-- Prerequisites -->

### :running: Run Locally

Clone the project

```bash
  git clone https://github.com/OpariucRares/OnPremise-vs-Cloud.git
```

We will use VS Code to run this project. You need:

- [.NET SKD](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- [.NET Install Tool](https://marketplace.visualstudio.com/items?itemName=ms-dotnettools.vscode-dotnet-runtime)
- [C#](https://marketplace.visualstudio.com/items?itemName=ms-dotnettools.csharp)
- [Docker](https://marketplace.visualstudio.com/items?itemName=ms-azuretools.vscode-docker) with Docker Engine

```bash
 # your project has dependencies that need to be restored
 # this must be run in the root of the service
 cd backend/OrderService
 dotnet restore

 # default it is in debug mode
 dotnet build

 dotnet run
```

If you do not like VS Code, you can use Microsoft Visual Studio for building each microservice. Repeat the same process for ProductService.

<!-- Deployment -->

### :triangular_flag_on_post: Deployment

To run each microservice, you need to use the docker file.

```bash
  cd backend

  #OrderService
  docker build -t orderservice -f OrderService/Dockerfile .
  docker run -d -p 5001:5001 orderservice

  # ProductService
  docker build -t productservice -f ProductService/Dockerfile .
  docker run -d -p 5002:5002 productservice

  # Stop the execution of the services
  docker stop productservice orderservice

  # Remove the images
  docker rm productservice orderservice
```

If you want the deploy all the microservices at once, you can use the docker-compose.yaml file.

```bash
  cd backend

  # building the images and the containers
  docker-compose up --build

  # stop and clean images/containers
  docker-compose down
```

<!-- License -->

## :warning: License

Distributed under the no License. See LICENSE.txt for more information.

<!-- Contact -->

## :handshake: Contact

Opariuc Rareș Ioan - opariucraresioan@gmail.com

Tablan Andrei-Răzvan - andreitablan01@gmail.com

Project Link: [https://github.com/OpariucRares/OnPremise-vs-Cloud](https://github.com/OpariucRares/OnPremise-vs-Cloud)

<!-- Acknowledgments -->

## :gem: Acknowledgements

Usefull bibliography

- [Introduction for Microservices](https://www.geeksforgeeks.org/microservices/)
- [Introduction for Docker](https://www.geeksforgeeks.org/introduction-to-docker/)
