# Mettec Backend & Frontend

## Table of Contents

- [Backend](#backend)
  - [Prerequisites](#prerequisites)
  - [Docker Setup](#building-the-docker-container-locally-optional)
  - [Setup](#setup)
  - [Run Unit Tests](#run-unit-tests)
- [Frontend](#frontend)
  - [Prerequisites](#prerequisites)
  - [Setup](#setup)
  - [Run Unit Tests](#run-unit-tests)

---

## Backend

### Prerequisites

- **.NET SDK**: version 8 or higher
- **Docker**: Optional

### Building the Docker Container Locally (Optional)

***Please skip those steps if you do not have Docker installed on your machine. Otherwise, create Database PostgreSQL instance manually.***

To run the application in Docker locally, follow these steps:

#### 1. Navigate to the project root
Make sure you are in the folder containing the `init-local-container.sh` file. Typically it is `Backend/.init-local-container.sh` folder.

#### 2. Run `init-local-container.sh`
Run the `init-local-container.sh` script. The script will check whether a `.env` file already exists in the root of your repository. If it doesn’t, default credentials will be created.

#### 3. Environment Variables
Your `.env` file should contain something like if not created previously:

```env
# Database
POSTGRES_USER=postgres
POSTGRES_PASSWORD=postgres
POSTGRES_DB=mettec-service-api-database

# pgAdmin
PGADMIN_DEFAULT_EMAIL=admin@gmail.com
PGADMIN_DEFAULT_PASSWORD=admin
```

#### 5. Migration

In general, migrations will be applied first time by EF Core once app starts against database.
If you need a new migration, please follow those steps:

Install dotnet-ef globally on your local machine (one time):

```
dotnet tool install --global dotnet-ef
```

- Go to `backend` folder in terminal.
- Create EF migration with command (each time for a new migration):

```
dotnet ef migrations add {MIGRATION_NAME} --project MettecService.DataAccess --startup-project MettecService.API
```

### Run Service Locally

If you run via terminal, use the following command from `backend` folder:

```
dotnet run --project ./MettecService.API/MettecService.API.csproj --launch-profile "MettecService.API.Local"

```

Service localhost URL: http://localhost:7300/swagger/index.html

#### appsettings.development.json

The `appsettings.development.json` file is already configured to connect to the local PostgreSQL container. **If you have use another database instance, please change connection string.**

---

## Frontend

Angular 18 frontend for **Mettec Task Manager** using Bootstrap 5.

### Prerequisites

- **Node.js**: version 20.x or higher  
- **npm**: comes with Node.js  
- **Angular CLI**: version 18.x or higher

Check versions:

```bash
node -v
npm -v
ng version
```

### Setup

Run the following commands one by one:

```bash
cd frontend
npm install
npm build
npm start
```
### Run Unit Tests

```bash
npm test
```

Service Localhost URL: http://localhost:55500/