Comando 1: Crear y levantar el contenedor PostgreSQL con persistencia
PowerShell
docker run --name postgres-local -e POSTGRES_PASSWORD=12345 -e POSTGRES_DB=PlanificadorFinanciero -v postgres_data:/var/lib/postgresql/data -p 5432:5432 -d postgres:16
Comando 2: Probar la conexión local desde PowerShell (Opcional)
PowerShell
docker exec -it postgres-local psql -U postgres -d PlanificadorFinanciero -c "\l"
Datos de conexión para DBeaver / appsettings.Development.json:
Host: localhost

Port: 5432

Database: PlanificadorFinanciero

Username: postgres

Password: 12345