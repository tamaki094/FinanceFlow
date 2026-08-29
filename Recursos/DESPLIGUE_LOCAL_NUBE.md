# 🚀 Despliegue local con Docker y Cloud Run

Este manual es diferente al de Google Cloud Build: aquí la imagen se construye localmente con Docker en tu máquina, no en la plataforma de Google.

> La secuencia es: Docker build local -> Docker push -> Cloud Run deploy.

---

## 1) Variables de entorno

En PowerShell define tu proyecto y la cadena de conexión:

```powershell
$PROJECT_ID = "planificadorgastos"
$IMAGE_TAG = "us-central1-docker.pkg.dev/$PROJECT_ID/finance-flow-repo/finance-flow-api:v1"
$DB_CONN = "Host=db.qrfqlmasfdxyxyzuoipi.supabase.co;Port=5432;Database=postgres;Username=postgres;Password=FinanceFlow2026!;"
```

---

## 2) Compilar la imagen Docker localmente

Desde la raíz del proyecto .NET, ejecuta:

```powershell
docker build -t $IMAGE_TAG .
```

Esto hace el build de la imagen en tu equipo, usando tu `Dockerfile`.

---

## 3) Probar la API localmente

Para levantar el contenedor y verificar que funciona antes de publicarlo:

```powershell
docker run --rm -it -p 8080:8080 -e ASPNETCORE_ENVIRONMENT=Production -e "ConnectionStrings__DefaultConnection=$DB_CONN" $IMAGE_TAG
```

Luego prueba la API en:

```text
http://localhost:8080
```

---

## 4) Autenticar Docker con Google Artifact Registry

```powershell
gcloud auth configure-docker us-central1-docker.pkg.dev
```

Esto permite subir la imagen desde tu máquina al registro de Google.

---

## 5) Subir la imagen a Artifact Registry

```powershell
docker push $IMAGE_TAG
```

---

## 6) Desplegar la imagen en Cloud Run

```powershell
gcloud run deploy finance-flow-api --image $IMAGE_TAG --platform managed --region us-central1 --allow-unauthenticated --set-env-vars "ConnectionStrings__DefaultConnection=$DB_CONN"
```

Esto publica tu contenedor en Cloud Run con la base de datos de Supabase conectada.

---

## 7) Flujo completo en una sola secuencia

Si quieres hacerlo todo en un bloque, este es el comando final recomendado:

```powershell
$PROJECT_ID = "planificadorgastos"
$IMAGE_TAG = "us-central1-docker.pkg.dev/$PROJECT_ID/finance-flow-repo/finance-flow-api:v1"
$DB_CONN = "Host=db.qrfqlmasfdxyxyzuoipi.supabase.co;Port=5432;Database=postgres;Username=postgres;Password=FinanceFlow2026!;"

docker build -t $IMAGE_TAG .
docker run --rm -it -p 8080:8080 -e ASPNETCORE_ENVIRONMENT=Production -e "ConnectionStrings__DefaultConnection=$DB_CONN" $IMAGE_TAG
gcloud auth configure-docker us-central1-docker.pkg.dev
docker push $IMAGE_TAG
gcloud run deploy finance-flow-api --image $IMAGE_TAG --platform managed --region us-central1 --allow-unauthenticated --set-env-vars "ConnectionStrings__DefaultConnection=$DB_CONN"
```

> Importante: en este documento no se usa `gcloud builds submit`. Ese paso pertenece al otro manual de Google Cloud Build.

---

## 📌 Diferencia clave con el otro manual

- Manual de Google Cloud Build: Google compila la imagen en la nube.
- Este manual: tú compilas la imagen localmente con Docker.

La intención es que cada documento tenga un flujo claro y no se mezclen las dos rutas de despliegue.