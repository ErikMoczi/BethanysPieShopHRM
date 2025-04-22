# SQL Server with Docker Compose

This project sets up **Microsoft SQL Server** using **Docker Compose**.

## 📌 Prerequisites
- Install [Docker](https://docs.docker.com/get-docker/)
- Install [Docker Compose](https://docs.docker.com/compose/install/)

## 🚀 Getting Started

### Start the SQL Server Container

```sh
docker compose up -d

```

### Verify Running Containers

```sh
docker ps

```

### Connect to SQL Server

```sh
docker exec -it bethanyspieshophrm-sql-db /opt/mssql-tools18/bin/sqlcmd -S localhost -U SA -P 'Password123!' -C

```

```sql
SELECT @@VERSION;
GO

```

### Create database

```sh
docker exec -it bethanyspieshophrm-sql-db /bin/bash -c "
        echo '⏳ Waiting for SQL Server...';
        /opt/mssql-tools18/bin/sqlcmd -S localhost -U SA -P 'Password123!' -C -Q \"IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = N'BethanysPieShopHRM') CREATE DATABASE BethanysPieShopHRM;\";
        echo '✅ Database created (if it didn''t exist already)';
      "
```

### Stop and Remove the Container

```sh
docker compose down

```