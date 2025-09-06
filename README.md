# BibliotecaAPI

API REST desarrollada en ASP.NET Core (.NET 9) para la gestión de una biblioteca. Permite administrar autores y libros, almacenando la información en una base de datos SQL Server mediante Entity Framework Core.

## Características principales
- CRUD de autores (`api/autores`): crear, consultar, actualizar y eliminar autores.
- CRUD de libros (`api/libros`): crear, consultar, actualizar y eliminar libros, asociando cada libro a un autor existente.
- Serialización JSON optimizada para evitar ciclos de referencia.
- Arquitectura basada en controladores y DbContext.
- Migraciones para la gestión de la base de datos.

## Tecnologías utilizadas
- ASP.NET Core 9
- Entity Framework Core 9
- SQL Server

## Uso
1. Clona el repositorio.
2. Configura la cadena de conexión en `appsettings.json`.
3. Ejecuta las migraciones para crear la base de datos.
4. Inicia la API y consume los endpoints desde Postman, Swagger u otra herramienta.

Ideal para aprender o servir de base para sistemas de gestión de bibliotecas o catálogos de libros.
