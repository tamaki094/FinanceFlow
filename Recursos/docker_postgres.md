# PostgreSQL local con Docker

Guía rápida para crear una instancia local de PostgreSQL y conectarla desde
DBeaver o desde la aplicación **FinanceFlow**.

## Requisitos

- Tener [Docker Desktop](https://www.docker.com/products/docker-desktop/) instalado y en ejecución.
- Usar PowerShell para ejecutar los comandos siguientes.

## 1. Crear y levantar el contenedor

El siguiente comando crea el contenedor `postgres-local`, inicializa la base de
datos `PlanificadorFinanciero` y guarda los datos en un volumen persistente.

```powershell
docker run --name postgres-local `
	-e POSTGRES_PASSWORD=12345 `
	-e POSTGRES_DB=PlanificadorFinanciero `
	-v postgres_data:/var/lib/postgresql/data `
	-p 5432:5432 `
	-d postgres:16
```

> El volumen `postgres_data` permite conservar la información aunque el
> contenedor se detenga o se elimine.

## 2. Comprobar la conexión (opcional)

Ejecuta este comando para listar las bases de datos disponibles dentro del
contenedor:

```powershell
docker exec -it postgres-local psql -U postgres -d PlanificadorFinanciero -c "\l"
```

## Datos de conexión

Utiliza estos valores en DBeaver o en `appsettings.Development.json`:

| Parámetro | Valor |
| --- | --- |
| Host | `localhost` |
| Puerto | `5432` |
| Base de datos | `PlanificadorFinanciero` |
| Usuario | `postgres` |
| Contraseña | `12345` |

## Cadena de conexión

Para la configuración de la aplicación:

```text
Host=localhost;Port=5432;Database=PlanificadorFinanciero;Username=postgres;Password=12345
```

> Estas credenciales son adecuadas para desarrollo local. No las utilices en
> entornos compartidos o de producción.