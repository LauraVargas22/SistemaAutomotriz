# SistemaAutomotriz

## Descripción 📌
SistemaAutomotriz es un sistema backend robusto desarrollado en ASP.NET Core bajo el esquema de arquitectura hexagonal. Su objetivo es automatizar y gestionar las operaciones esenciales dentro de un taller automotriz, permitiendo registrar clientes y vehículos, gestionar órdenes de servicio, controlar el inventario de repuestos y generar facturas, todo mientras se mantiene un control para el acceso el cual esta basado en roles(Administrator, Recepcionist y Mechanic) y autenticación segura mediante JWT.

## Estructura del Proyecto 🏗️
El proyecto sigue la estructura de la arquitectura hexagonal:
```bash
InventoryManagementSystem/
├── ApiSGTA/
│   ├── Controllers/
│   ├── Extensions/
│   ├── Helpers/
│   │   └── Errors/
│   ├── Profiles/
│   ├── Properties/
│   └── Services/
├── Application/
│   ├── DTOs/
│   ├── Interefaces/
│   │   └── Auth/
│   └── Services/
├── Domain/
│   └── Entities/
├── Infrastructure/
│   ├── Configuration/
│   ├── Data/
│   │   └── Migrations/
│   ├── Interceptors/
│   ├── Repositories/
│   └── UnitOfWork/
├── db/
│   └── dml.sql
├── .gitignore
└── README.md
```

## Funcionalidades Principales ✅
- Registro e inicio de sesión de usuarios con autenticación basada en JWT.
- Asignación de roles (`Administrator`, `Recepcionist`, `Mechanic`) a cada usuario.
- Restricción de acceso a los endpoints de la API según el rol del usuario.
- Relación de clientes con uno o varios vehículos registrados.
- Creación y gestión de órdenes de servicio por parte del personal autorizado.
- Asignación de un mecánico a cada orden de servicio.
- Cálculo y asignación de fecha estimada de entrega según el tipo de servicio.
- Gestión del inventario de repuestos utilizados en cada orden.
- Generación de factura al finalizar una orden de servicio.
- Conexión con base de datos PostgreSQL mediante Entity Framework Core.
- Documentación interactiva de endpoints con Swagger / OpenAPI.

## Tecnologias Utilizadas 👾
- Lenguaje: C# / ASP.NET Core
- ORM: Entity Framework Core
- Base de Datos: PostgreSQL
- Autenticación: JWT (JSON Web Tokens)
- Mapeo de Objetos: AutoMapper
- Documentación: Swagger / OpenAPI

## Dependencias NuGet Importantes 📦

- `Microsoft.EntityFrameworkCore`
- `Microsoft.EntityFrameworkCore.Design`
- `Microsoft.EntityFrameworkCore.Tools`
- `Npgsql.EntityFrameworkCore.PostgreSQL`
- `AutoMapper.Extensions.Microsoft.DependencyInjection`
- `Microsoft.AspNetCore.Authentication.JwtBearer`
- `Swashbuckle.AspNetCore` (Swagger)

## Instalación 📥

### Requisitos Previos 🔧
- Tener instalado [Git](https://git-scm.com/)
- Tener instalado [Postgres](https://www.postgresql.org/download/)
- Tener instalado [.NET](https://dotnet.microsoft.com/en-us/download/dotnet/8.0/)
- Un editor de código como Visual Studio Code, Visual Studio, o el de tu preferencia.

### Pasos para Ejecutar el proyecto 🚀

1. Clonar Repositorio, Ir al directorio del repositorio (En la terminal)

```
git clone https://github.com/LauraVargas22/SistemaAutomotriz.git 
```
```
/cd SistemaAutomotriz
```

2. Configurar tu cadena de conexion  (Cambiar este bloque de codigo dentro de ApiSGTA, en appsettings.Development por TUS credenciales para ingresar a postgres)

```
"ConnectionStrings": {
  "DefaultConnection": "Host=localhost;Port=5432;Database=sistema_automotriz;Username=postgres;Password=tu_contraseña"
}
```
3. Aplicar las migraciones de la base de datos

```
dotnet ef database update
```

4. Ejecucion del proyetco
```
dotnet run
```
5. Visualizacion del Swagger
   
6. Al ejecutar el proyecto, verás en la terminal una URL similar a la siguiente:
```
https://localhost:{puerto}
```

7. Abre esa dirección en tu navegador y agrega /swagger al final para acceder a la documentación interactiva de la API. El resultado será algo como:
```
https://localhost:{puerto}/swagger
```

## Establecer conexion con la base de datos
Es necesario realizar la conexion con Posgres, esta se puede realizar de distintas maneras dependiendo de tu editor de codigo. Por ejemplo (En Visual Studio):
1. Descargar extencion *MySQL*

![1](https://imgur.com/cap8ayo.png)

2. Seleccionar *Create Connection*

![2](https://imgur.com/GHAozRu.png)

3. Establecer la conexion con las credenciales dependiendo del sistema de gestión de bases de datos

![3](https://imgur.com/6GyVxei.png)

4. Guardar y conectar la conexion

![4](https://imgur.com/tx89sKn.png)

## Inserción de Datos de Prueba

Es recomendable ejecutar el script de inserción de datos para poblar la base de datos con información inicial y así poder probar adecuadamente las APIs.
El archivo SQL se encuentra en la carpeta **db/** del proyecto:
```bash
├── db/
│   └── dml.sql
```
Puedes ejecutar este script desde tu cliente PostgreSQL preferido (como pgAdmin, DBeaver o la terminal interactiva psql) una vez creada la base de datos y aplicadas las migraciones.

## Interceptores
El uso de interceptores permite ejecutar lógica personalizada antes o después de que los cambios sean persistidos en la base de datos. Es ideal para auditoría, ya que puede registrar automáticamente todas las inserciones, actualizaciones y eliminaciones.
[Ver Más](https://www.woodruff.dev/tracking-every-change-using-savechanges-interception-for-ef-core-auditing/#:~:text=%C2%BFQu%C3%A9%20es%20SaveChanges%20Interception?,y%20almacenar%20registros%20de%20auditor%C3%ADa%20.)

1. Interceptor Personalizado:
Se implementa una clase que hereda de **SaveChangesInterceptor** y sobreescribe los métodos SavingChanges y SavingChangesAsync.
En estos métodos, se recorren las entidades rastreadas por el contexto y se registra el tipo de cambio (agregado, modificado, eliminado), el nombre de la entidad, el usuario responsable y la fecha/hora.
2. Entidad de Auditoría:
Se utiliza una entidad (por ejemplo, Auditory o AuditLog) para almacenar los registros de auditoría en la base de datos.
3. Registro del Interceptor:
El interceptor se registra en la configuración del DbContext y en el contenedor de dependencias (Program.cs), asegurando que se aplique a cada operación de guardado.

### Implementación
1. [Interceptor Personalizado](./Infrastructure/Interceptors/AuditInterceptor.cs)
2. [Entidad Auditoria](./Domain/Entities/Auditory.cs)
3. [Registro en el DbContext](./Infrastructure/Data/AutoTallerDbContext.cs)
```
public class AutoTallerDbContext : DbContext
{
    private readonly AuditInterceptor _auditInterceptor;

    public AutoTallerDbContext(DbContextOptions<AutoTallerDbContext> options, AuditInterceptor auditInterceptor)
        : base(options)
    {
        _auditInterceptor = auditInterceptor;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.AddInterceptors(_auditInterceptor);
    }
}
```
4. [Inyección](./ApiSGTA/Extensions/ApplicationServiceExtensions.cs)
```
//Interceptor
services.AddScoped<AuditInterceptor>();
services.AddHttpContextAccessor();

```

## Configuración Servicio Email
Implementar un servicio de notificaciones por email robusto y escalable que permita enviar notificaciones automáticas a los clientes del taller automotriz cuando se crean o actualizan órdenes de servicio.

- Funcionalidades Principales

✅ Envío de email al crear orden de servicio
✅ Notificación de cambios de estado
✅ Plantillas HTML personalizables
✅ Soporte para múltiples proveedores SMTP

- Paquetes implementados
```
cd Infrastructure
dotnet add package MailKit --version 4.3.0
dotnet add package MimeKit --version 4.3.0
dotnet restore
```

### Implementación
1. [Clase de Configuración](./Infrastructure/Configuration/EmailConfiguration.cs)
2. [Configuración appsettings.json](./ApiSGTA/appsettings.json)
```
"EmailSettings": {
    "SmtpServer": "smtp.gmail.com",
    "SmtpPort": 587,
    "SenderEmail": "sistemaautomotriz6@gmail.com",
    "SenderPassword": "oxrv ekkz lqaa hpoq",
    "SenderName": "Sistema Taller Automotriz"
  }
```
Para obtener la contraseña del correo se tiene que crear una contraseña para aplicaciones. [Ver más](https://www.youtube.com/watch?v=ZfEK3WP73eY)
3. [Definir puerto Domain](./Domain/Ports/IEmailService.cs)
4. [Adaptador Infrastructura](./Infrastructure/Adapters/EmailService.cs)
7. [Inyección](./ApiSGTA/Extensions/ApplicationServiceExtensions.cs)
```
// Configuración de Email
var emailConfig = services.BuildServiceProvider().GetRequiredService<IConfiguration>().GetSection("EmailSettings").Get<EmailConfiguration>();
services.AddSingleton(emailConfig);

// Registro de servicios
services.AddScoped<IEmailService, EmailService>();
services.AddScoped<CreateEmailServiceOrderService>();
services.AddScoped<ClientServiceOrderService>();
```
5. [Implementar caso de Uso](./Application/Services/CreateEmailServiceOrderService.cs)
Este caso de uso permite el envio del email automaticamente al registrar una orden de servicio.
6. [Aplicación Controlador](./ApiSGTA/Controllers/ServiceOrderController.cs)


# Autores ✒️

- Laura Mariana Vargas  
- Isabella Stefphani Galvis  
- Hodeth Valentina Caballero  
- Andres Felipe Araque
