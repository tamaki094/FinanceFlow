# 1. Imagen base SDK de .NET para compilar
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# ⚠️ Apuntamos a la subcarpeta FinanceFlow/ donde está el .csproj
COPY ["FinanceFlow/FinanceFlow.csproj", "FinanceFlow/"]
RUN dotnet restore "FinanceFlow/FinanceFlow.csproj"
COPY . .
WORKDIR "/src/FinanceFlow"
RUN dotnet publish "FinanceFlow.csproj" -c Release -o /app/publish

# 2. Imagen ligera de runtime para ejecutar la API
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

ENV PORT=8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "FinanceFlow.dll"]