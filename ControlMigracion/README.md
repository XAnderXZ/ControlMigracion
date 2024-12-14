# Aplicación de Control de Migración

## Descripción

Esta aplicación web desarrollada en C# con ASP.NET Core MVC proporciona un sistema de control de migración. Permite gestionar usuarios, viajeros, viajes y registros de entrada/salida.

## Requisitos Previos

- Visual Studio 2022
- .NET 6.0 SDK o superior
- SQL Server (LocalDB viene incluido con Visual Studio)

## Instalación y Configuración en Visual Studio

1. Clonar o descargar el repositorio en tu máquina local. (https://github.com/XAnderXZ/ControlMigracion.git)

2. Abrir Visual Studio 2022.

3. Seleccionar "Abrir un proyecto o una solución" y navegar hasta la carpeta del proyecto.

4. Seleccionar el archivo de solución (.sln) y hacer clic en "Abrir".

5. Una vez abierto el proyecto, abrir la Consola del Administrador de Paquetes:
   - Ir a "Herramientas" > "Administrador de paquetes NuGet" > "Consola del Administrador de Paquetes"

6. En la Consola del Administrador de Paquetes, ejecutar los siguientes comandos para instalar las dependencias necesarias:

Install-Package Microsoft.EntityFrameworkCore -Version 7.0.5
Install-Package Microsoft.EntityFrameworkCore.SqlServer -Version 7.0.5
Install-Package Microsoft.EntityFrameworkCore.Tools -Version 7.0.5


## Configuración de la Base de Datos

1. Abrir el archivo `appsettings.json` en el Explorador de soluciones y cambiar donde dice "Tu-Servidor" por el servidor de tu dispositivo donde ejecutas el programa:
```json
"ConnectionStrings": {
    "DefaultConnection": "Server=Tu-Servidor;Database=ControlMigracion;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True"
  },

2. En la Consola del Administrador de Paquetes, ejecutar los siguientes comandos para crear y aplicar las migraciones:

Add-Migration InitialCreate
Update-Database

3. Recientemente se agregó una funcionalidad para iniciar sesión, por lo cual para que esa nueva funcion corra de manera correcta, debe ejecutar los siguientes comandos:

Add-Migration AddRoleToUsuario
Update-Database

4. Seguidamente debes ejecutar en el script de la base de datos lo siguiente:

ALTER TABLE Usuario
ADD Rol NVARCHAR(50) NOT NULL DEFAULT 'Usuario';



5. Tambien se agregó una pestaña de viajes, donde se pueden crear viajes para los viajeros registrados en el sistema, para que esto funcione correctamente, solo debes ejecutar estos comando 
en la terminal de visual:

Add-Migration AddDestinoToViaje
Update-Database

6. Seguidamente debes ejecutar en el script de la base de datos lo siguiente:

ALTER TABLE Viaje
ADD Destino NVARCHAR(100) NOT NULL DEFAULT '';
