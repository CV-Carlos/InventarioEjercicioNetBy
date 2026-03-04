# Sistema de Gestión de Inventario

Aplicación full-stack para la gestión de productos y transacciones de inventario, desarrollada con Angular 18, .NET 10 y PostgreSQL bajo una arquitectura de microservicios.

---

## Requisitos

Asegúrese de tener instaladas las siguientes herramientas antes de ejecutar el proyecto:

| Herramienta | Versión mínima |
|---|---|
| [Node.js](https://nodejs.org/) | 20+ |
| [Angular CLI](https://angular.io/cli) | 18+ |
| [.NET SDK](https://dotnet.microsoft.com/download) | 10 |
| [PostgreSQL](https://www.postgresql.org/) | 17 |
| [Visual Studio 2022](https://visualstudio.microsoft.com/) | 17+ |
| [pgAdmin](https://www.pgadmin.org/) | 4+ (opcional) |

---

## Base de datos

### 1. Crear la base de datos

Abra pgAdmin, conéctese al servidor PostgreSQL y ejecute:

```sql
CREATE DATABASE inventario;
```

### 2. Ejecutar el script de inicialización

- Seleccione la base de datos `inventario`
- Abra el **Query Tool** (`Tools` → `Query Tool`)
- Copie y pegue el contenido del archivo `database/init.sql`
- Ejecute con `F5`

Esto creará los schemas, tablas, índices y datos iniciales necesarios.

---

## Ejecución del backend

### 1. Abrir la solución

Abra el archivo `InventarioApp.sln` en Visual Studio 2022.

### 2. Configurar la cadena de conexión

En ambos proyectos (`Productos.API` y `Transacciones.API`), edite el archivo `appsettings.json` y reemplace la contraseña de PostgreSQL:

```json
{
  "ConnectionStrings": {
    "PostgreSQL": "Host=localhost;Database=inventario;Username=postgres;Password=SU_PASSWORD"
  }
}
```

### 3. Configurar la URL de comunicación entre microservicios

En `Transacciones.API/appsettings.json`, configure el puerto de `Productos.API`:

```json
{
  "ProductosAPI": {
    "BaseUrl": "https://localhost:PUERTO_PRODUCTOS_API"
  }
}
```

El puerto de `Productos.API` se puede ver en las propiedades del proyecto en Visual Studio (`Properties` → `Debug` → `App URL`).

### 4. Configurar inicio múltiple

Para correr ambos microservicios simultáneamente:

- Click derecho en la solución → `Set Startup Projects`
- Seleccionar `Multiple startup projects`
- Establecer ambos proyectos (`Productos.API` y `Transacciones.API`) en `Start`
- Click en `OK`

### 5. Ejecutar

Presione `F5` o el botón `Start` en Visual Studio.

Los microservicios estarán disponibles con su documentación Swagger en:
- `Productos.API`: `https://localhost:PUERTO/swagger`
- `Transacciones.API`: `https://localhost:PUERTO/swagger`

---

## Ejecución del frontend

### 1. Instalar dependencias

Abra una terminal en la carpeta `frontend` y ejecute:

```bash
npm install
```

### 2. Configurar las URLs de las APIs

Edite el archivo `src/environments/environment.ts` con los puertos correctos de cada microservicio:

```typescript
export const environment = {
  production: false,
  productosApiUrl: 'https://localhost:PUERTO_PRODUCTOS_API/api',
  transaccionesApiUrl: 'https://localhost:PUERTO_TRANSACCIONES_API/api',
};
```

### 3. Ejecutar el servidor de desarrollo

```bash
ng serve
```

### 4. Abrir en el navegador

```
http://localhost:4200
```

---

## Evidencias

### 1. Listado de productos con paginación
<img width="1181" height="853" alt="image" src="https://github.com/user-attachments/assets/2b7b3e14-744b-48f2-af74-bbba348fec52" />
<img width="1212" height="551" alt="image" src="https://github.com/user-attachments/assets/a43b7d50-3cde-4b6c-b991-b274023b6026" />
<img width="1255" height="913" alt="image" src="https://github.com/user-attachments/assets/9cc475c1-899c-41e1-a1d4-2e470ec6b2fa" />

### 2. Listado de transacciones con paginación
<img width="1182" height="815" alt="image" src="https://github.com/user-attachments/assets/6d4ab270-5f3b-49da-8fe6-67360f1b1c40" />
<img width="1183" height="610" alt="image" src="https://github.com/user-attachments/assets/6c2e864c-9011-4593-9eab-cc695f0a8855" />
<img width="894" height="764" alt="image" src="https://github.com/user-attachments/assets/0ea105de-c1f0-4c25-95d3-8c060860263d" />

### 3. Creación de producto
<img width="1343" height="593" alt="image" src="https://github.com/user-attachments/assets/f6f27716-98d0-46de-92a1-ab78681ae653" />
<img width="1282" height="629" alt="image" src="https://github.com/user-attachments/assets/070e2c24-03bb-42d7-a4b1-279df9af5983" />
<img width="1264" height="631" alt="image" src="https://github.com/user-attachments/assets/438bca40-8f33-4af4-814f-d8439d20e8d1" />

### 4. Edición de producto
<img width="1274" height="610" alt="image" src="https://github.com/user-attachments/assets/27215d6a-b8d8-4cfe-aa8d-667a1162fa72" />

### 5. Creación de transacción
<img width="1256" height="544" alt="image" src="https://github.com/user-attachments/assets/1cd83ac3-6db3-4fa5-8cda-a708b2f27f3f" />
<img width="1373" height="681" alt="image" src="https://github.com/user-attachments/assets/dd6f71ac-ae48-4ae6-91ac-58831d33440d" />
<img width="1283" height="584" alt="image" src="https://github.com/user-attachments/assets/f13fb965-195c-4baa-878b-3a9a433f6c4c" />
<img width="1282" height="582" alt="image" src="https://github.com/user-attachments/assets/434ccc83-64f1-43e8-9cb4-6385eb701e9d" />

### 6. Edición de transacción
<img width="1319" height="527" alt="image" src="https://github.com/user-attachments/assets/cdad0098-7bfe-493a-a81a-b2e072529aa7" />

### 7. Filtros dinámicos
<img width="1306" height="704" alt="image" src="https://github.com/user-attachments/assets/8118b008-5409-48dc-a171-898804e16d96" />
<img width="1278" height="579" alt="image" src="https://github.com/user-attachments/assets/18796fcf-8184-4be4-8251-dca3257adc02" />
<img width="1271" height="501" alt="image" src="https://github.com/user-attachments/assets/b143156e-cff6-45a3-84e0-8a192681ab46" />
<img width="1326" height="524" alt="image" src="https://github.com/user-attachments/assets/163747f4-29a4-4134-af27-766ad47c5239" />
<img width="1291" height="522" alt="image" src="https://github.com/user-attachments/assets/f39def77-5e28-4230-b15d-754c9db2cfe6" />

<img width="1293" height="606" alt="image" src="https://github.com/user-attachments/assets/aea353ac-1ff9-44d5-8c76-f3221cfd9f20" />
<img width="1387" height="518" alt="image" src="https://github.com/user-attachments/assets/ed922b0d-2cda-45d7-858e-9f09b7ab84db" />
<img width="1288" height="470" alt="image" src="https://github.com/user-attachments/assets/5facd3a7-5112-42b2-bff8-ac438cb21ecd" />

### 8. Consulta de información en formulario (extra)
<img width="1321" height="457" alt="image" src="https://github.com/user-attachments/assets/c901779a-3adc-4025-af86-16775b946364" />
<img width="1295" height="512" alt="image" src="https://github.com/user-attachments/assets/18a30a5d-bccf-48b7-a2a8-38460eb0fd54" />
