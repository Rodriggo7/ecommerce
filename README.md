# E-Commerce API - Clean Architecture (.NET 8)

Proyecto integrador final para la asignatura **Backend (Optativa II)**. 
Esta es una API RESTful desarrollada con **.NET 8** aplicando los principios de **Clean Architecture** (Arquitectura Limpia) y el patrón **CQRS** para garantizar un código escalable, testeable y mantenible.

## Tecnologías y Herramientas principales

- **Framework:** .NET 8 (ASP.NET Core Web API)
- **Base de Datos:** SQLite (desarrollo local)
- **ORM:** Entity Framework Core
- **Patrones:** Clean Architecture, CQRS, Repository Pattern
- **Validaciones:** FluentValidation
- **Mediador:** MediatR
- **Seguridad:** JWT (Bearer) & BCrypt.Net-Next (Hashing)
- **Documentación:** Swagger (Swashbuckle)

## Estructura del Proyecto

El sistema está dividido en 4 proyectos que respetan estrictamente la Regla de Dependencia:

1. **`ECommerce.Domain`:** Entidades puras, Enumeradores y reglas de dominio. No depende de nada.
2. **`ECommerce.Application`:** Lógica de orquestación (CQRS), DTOs, interfaces (puertos) y validadores. Depende del Dominio.
3. **`ECommerce.Infrastructure`:** Persistencia de datos, DbContext, migraciones de EF Core y generación de tokens JWT.
4. **`ECommerce.Api`:** Punto de entrada, controladores HTTP sin lógica de negocio, middlewares (Manejo global de errores RFC 7807) y Swagger.

## Requisitos y Configuración Previa

1. **.NET SDK 8.0** o superior.
2. **Herramienta EF Core CLI** (necesaria para migraciones):
   ```bash
   dotnet tool install --global dotnet-ef


3. **Configuración (`appsettings.Development.json`):**
Antes de ejecutar, asegúrate de contar con la siguiente configuración en tu proyecto API:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=ecommerce.db"
  },
  "Jwt": {
    "Secret": "EstaEsUnaClaveSuperSecretaParaECommerceApi2026!!",
    "Issuer": "ECommerceApi",
    "Audience": "ECommerceClients"
  }
}

```



## Instalación y Ejecución

1. **Clonar y restaurar paquetes:**
```bash
git clone https://github.com/Rodriggo7/ecommerce.git
cd ecommerce
dotnet restore

```


2. **Aplicar migraciones (Crear base de datos SQLite):**
```bash
dotnet ef database update --project ECommerce.Infrastructure --startup-project ECommerce.Api

```


3. **Ejecutar la API:**
```bash
dotnet run --project ECommerce.Api

```


> La documentación interactiva estará disponible en `https://localhost:{puerto}/swagger`.



## Pruebas de Seguridad y Roles (RBAC)

El sistema cuenta con autorización basada en roles. Los endpoints de modificación (POST, PUT, DELETE de Productos) están restringidos.

> **Nota Arquitectónica:** En un entorno de producción real, la creación de usuarios con privilegios administrativos no se expone en el endpoint público de registro, sino que se maneja mediante *Data Seeding* inicial o paneles internos seguros. Sin embargo, a fines prácticos y para facilitar la evaluación y el testing rápido de este proyecto, se implementó una regla temporal en la Capa de Aplicación.

Para **crear el Administrador inicial** y probar estos endpoints:

1. Registra un usuario nuevo en `/api/auth/register` utilizando el email maestro: **`admin@ecommerce.com`**.
2. Inicia sesión en `/api/auth/login` con esas credenciales.
3. Copia el Token devuelto y colócalo en el botón **"Authorize"** de Swagger (formato: `Bearer [tu_token]`).
4. Ahora tendrás acceso HTTP 201/204 a las rutas restringidas (evitando el error HTTP 403 Forbidden).

```
