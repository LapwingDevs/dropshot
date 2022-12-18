# dropshot

## Description

The application has been created as university project. 

The main requirement was to handle the case where 2 users want to reserve the same resource. We implemented it in clothes drops system using SignalR.

## Technology stack

- ASP.NET 6
- Clean architecture
- Entity Framework
- PostgreSQL
- Authentication & Authorization in ASP.NET
- SignalR
- Background worker
- React

## Guide

### Database

a) Start db

You can start database with docker compose, it contains PostgreSQL and PgAdmin to db overview. Run `docker compose -f "docker-compose-infra-only.yml" up -d`

b) PgAdmin 

Address: http://localhost:5050

Login into the PgAdmin and then you have to register your Postgres server. Click Servers > Create > Server to create a new server.

To get postgres address, get container id by `docker ps`, then you can run `docker inspect container_id | grep IPAddress`.

Enter address, username and password into from and you can connect to the server.

c) Entity Framework

Swap your database data in connection string in `api/DropShot.API/appsettings.json`

dotnet ef example commands from `/api` directory

```
dotnet ef migrations add MIGRATION_NAME --project DropShot.Infrastructure --startup-project DropShot.API --output-dir DAL/Migrations

dotnet ef database update --project DropShot.Infrastructure --startup-project DropShot.API
```
