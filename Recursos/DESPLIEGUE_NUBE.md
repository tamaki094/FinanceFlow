# 🚀 Guía de Despliegue en Google Cloud Run

Documentación completa para desplegar la API de FinanceFlow en Google Cloud Run usando Docker y Cloud Build.

---

## 📋 Tabla de Contenidos

1. [Entender el Dockerfile](#entender-el-dockerfile)
2. [Pasos de Despliegue](#pasos-de-despliegue)
3. [Explicación de Parámetros](#explicación-de-parámetros)

---

## 📦 Entender el Dockerfile

### Estructura General

El Dockerfile utiliza **build stages** (etapas multi-etapa) para optimizar el tamaño final de la imagen. Se compone de 2 etapas principales:

1. **Build Stage** - Compilación del código
2. **Runtime Stage** - Ejecución de la aplicación

---

### ETAPA 1: Compilación de la Aplicación (build)

Esta etapa se ejecuta **una sola vez** cuando corres el comando de compilación en la nube de Google o en tu máquina.

```dockerfile
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# ⚠️ Apuntamos a la subcarpeta FinanceFlow/ donde está el .csproj
COPY ["FinanceFlow/FinanceFlow.csproj", "FinanceFlow/"]
RUN dotnet restore "FinanceFlow/FinanceFlow.csproj"
COPY . .
WORKDIR "/src/FinanceFlow"
RUN dotnet publish "FinanceFlow.csproj" -c Release -o /app/publish
```

#### Desglose línea por línea

| Instrucción | Descripción |
|---|---|
| `FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build` | Descarga la imagen oficial de Microsoft con el SDK completo de .NET 9 (compilador, MSBuild, herramientas CLI). Le asigna el nombre `build` a esta etapa. |
| `WORKDIR /src` | Crea y se posiciona dentro del directorio `/src` en el sistema de archivos del contenedor. |
| `COPY ["FinanceFlow/FinanceFlow.csproj", "FinanceFlow/"]` | Copia únicamente el archivo del proyecto. **Truco Senior:** Se copia primero para que Docker aproveche su caché interna. |
| `RUN dotnet restore "FinanceFlow/FinanceFlow.csproj"` | Lee tu `.csproj` y descarga todas las librerías/paquetes NuGet requeridos. |
| `COPY . .` | Copia todo el resto de los archivos del proyecto (`.cs`, controladores, servicios). |
| `WORKDIR "/src/FinanceFlow"` | Cambia el directorio de trabajo al proyecto principal. |
| `RUN dotnet publish "FinanceFlow.csproj" -c Release -o /app/publish` | Compila en modo **Release** (optimizado para producción) y coloca la salida en `/app/publish`. |

---

### ETAPA 2: Runtime Ligero (final)

Esta etapa se procesa durante la creación de la imagen, pero es la que se ejecutará cada vez que el contenedor encienda en Cloud Run.

```dockerfile
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

ENV PORT=8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "FinanceFlow.dll"]
```

#### Desglose línea por línea

| Instrucción | Descripción |
|---|---|
| `FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final` | Descarga una imagen ligera de Microsoft con solo el runtime de ASP.NET Core 9 (sin SDK). Descarta todo lo de la etapa 1 excepto lo que copiemos explícitamente. |
| `WORKDIR /app` | Crea y se ubica en `/app` dentro del contenedor de runtime. |
| `COPY --from=build /app/publish .` | Copia los archivos `.dll` compilados desde la etapa build hacia `/app`. |
| `ENV PORT=8080` | Define variable de entorno para indicar que la API responderá en puerto **8080**. |
| `EXPOSE 8080` | Instrucción documental que indica que el contenedor escuchará peticiones en el puerto 8080. |
| `ENTRYPOINT ["dotnet", "FinanceFlow.dll"]` | Se ejecuta cada vez que el contenedor enciende. Inicia la aplicación ASP.NET Core. |

---

## 🔧 Pasos de Despliegue

### ✅ Paso 1: Habilitar los servicios en Google Cloud

> **Nota:** Solo se ejecuta una vez.

Desde la terminal de PowerShell (donde ya autenticaste `gcloud`), ejecuta:

```powershell
gcloud services enable cloudbuild.googleapis.com run.googleapis.com
```

**Respuesta esperada:**
```
Operation "operations/acf.p2-520242405013-eabca8a2-c394-45fe-8922-939873d3739d" finished successfully.
```

---

### ✅ Paso 2: Compilar la imagen en la nube (Cloud Build)

Posiciónate en la raíz de tu proyecto .NET y ejecuta:

```powershell
gcloud builds submit --tag gcr.io/planificadorgastos/finance-flow-api
```

> **Ubicación:** `C:\Users\edria\OneDrive\Documentos\Proyectos\DotNet\FinanceFlow`

Google tomará tu código, leerá tu Dockerfile y empaquetará el contenedor en sus servidores.

**Respuesta esperada:**
```
latest: digest: sha256:7005fc443a1ede9743030166106f989072e274c2121aa930eb39b5657feda5d5 size: 1998
DONE
-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
ID: ceceb6cc-d01f-43d6-97f9-cfd7d180f784
CREATE_TIME: 2026-08-28T07:42:53+00:00
DURATION: 1M20S
SOURCE: gs://planificadorgastos_cloudbuild/source/1787902973.142202-becfd8a1ca9e4d8eb0a6969484300243.tgz
IMAGES: gcr.io/planificadorgastos/finance-flow-api (+1 more)
STATUS: SUCCESS
```

---

### ✅ Paso 3: Desplegar en Google Cloud Run

Desde tu terminal de PowerShell, ejecuta:

```powershell
gcloud run deploy finance-flow-api --image gcr.io/planificadorgastos/finance-flow-api --platform managed --region us-central1 --set-env-vars "ASPNETCORE_ENVIRONMENT=Production" --allow-unauthenticated
```

Este comando ordena a Google Cloud que:
- Tome la imagen compilada de tu API
- Levante la infraestructura Serverless
- Asigne variables de producción
- Genere una **URL HTTPS pública**

**Respuesta esperada:**
```
OK Routing traffic...
OK Setting IAM Policy...
Done.
Service [finance-flow-api] revision [finance-flow-api-00001-fn2] has been deployed and is serving 100 percent of traffic.
Service URL: https://finance-flow-api-520242405013.us-central1.run.app
```

🎉 **¡Tu API está lista para consumir desde Angular o Postman!**


---

## 📝 Explicación de Parámetros

### Comando: `gcloud run deploy`

| Parámetro | Valor | Descripción |
|---|---|---|
| **gcloud run deploy** | `finance-flow-api` | Orden principal para desplegar en Cloud Run. Define el nombre del servicio en la nube. |
| **--image** | `gcr.io/planificadorgastos/finance-flow-api` | Indica de dónde jalar el contenedor compilado. Apunta al registro de imágenes de tu proyecto de Google. |
| **--platform** | `managed` | Usa la versión totalmente administrada de Cloud Run (Serverless - sin gestionar servidores físicos). |
| **--region** | `us-central1` | Región geográfica de los datacenters donde se ejecutará tu contenedor (centro de EE.UU., rápida y económica). |
| **--set-env-vars** | `ASPNETCORE_ENVIRONMENT=Production` | Inyecta una variable de entorno. Fuerza a ASP.NET Core a ejecutar en modo productivo. |
| **--allow-unauthenticated** | — | Abre acceso público por internet sin requerir tokens. Permite que Angular, Postman u otras apps accedan directamente. |

### Comportamiento de `ASPNETCORE_ENVIRONMENT=Production`

Cuando estableces esta variable:
- ❌ Se **ignora** `appsettings.Development.json` (configuración de Trace)
- ✅ Se **aplica** `appsettings.json` (configuración de Warning)
- 💰 Se **ahorra** cuota de logs en Google Cloud

---

## 📌 Resumen Rápido

```
┌─────────────────────────────────────────────────────────────────┐
│                    FLUJO DE DESPLIEGUE                          │
├─────────────────────────────────────────────────────────────────┤
│ 1. Enable Services (una vez)                                    │
│    └─ gcloud services enable cloudbuild.googleapis.com run...   │
│                                                                 │
│ 2. Cloud Build (compilar imagen)                               │
│    └─ gcloud builds submit --tag gcr.io/.../finance-flow-api   │
│                                                                 │
│ 3. Cloud Run Deploy (desplegar contenedor)                     │
│    └─ gcloud run deploy finance-flow-api --image ... --allow...│
│       └─ 🎉 URL HTTPS pública lista                            │
└─────────────────────────────────────────────────────────────────┘
```

---

## 🔗 URL de Acceso

Una vez completado el despliegue, tu API estará disponible en:

```
https://finance-flow-api-520242405013.us-central1.run.app
```

Puedes consumirla desde:
- ✅ Frontend Angular
- ✅ Cliente Postman
- ✅ Cualquier otra aplicación HTTP/HTTPS
