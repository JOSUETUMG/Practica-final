# Análisis del Proyecto

## 1. Decisiones sobre estructura de datos

Para el proyecto elegimos una entidad principal llamada `Product` con los campos necesarios para una tienda básica:
- `Id`: clave primaria entera.
- `Name`: nombre del producto.
- `Description`: descripción del producto.
- `Price`: precio en número decimal.
- `Stock`: cantidad disponible.
- `CreatedAt`: fecha de creación.

Esta estructura es clara, simple y suficiente para manejar los escenarios de creación, lectura, actualización y eliminación (CRUD) de productos.

## 2. Separación de archivos y capas

Organizamos el código en carpetas para mantener la lógica separada y fácil de navegar:
- `Models/` para las entidades del dominio (`Product`, `LoginRequest`).
- `Data/` para el contexto de Entity Framework (`ProductDbContext`).
- `Controllers/` para los controladores de API (`ProductsController`, `AuthController`).
- `Migrations/` para futuras migraciones de base de datos.

Esta separación ayuda a mantener el proyecto ordenado y permite modificar modelo, datos o controladores sin mezclar responsabilidades.

## 3. Configuración de Swagger y autenticación

Decidimos exponer la documentación con Swagger para poder probar los endpoints desde una interfaz web. Además:
- Configuramos `SwaggerGen` con un documento `v1` y descripción.
- Agregamos la definición de seguridad Bearer en Swagger para que se pueda usar JWT en los endpoints protegidos.
- Protegimos el controlador de productos con `[Authorize]`.

La autenticación funciona usando JWT, generado en `AuthController` tras validar usuario y contraseña.

## 4. Docker y MySQL

Utilizamos Docker Compose para ejecutar dos servicios:
- `mysql`: contenedor de MySQL 8.4 con la base de datos `productsdb` y credenciales definidas.
- `api`: contenedor de la API que se conecta a MySQL usando la cadena de conexión `Server=mysql;Port=3306;Database=productsdb;User=productsuser;Password=productspass;`.

Esto permite que la API y la base de datos corran en un entorno reproducible, sin depender de instalaciones locales.

## 5. Comandos usados

Se emplearon los siguientes comandos para levantar y verificar los servicios:
- `docker compose up --build -d` para construir y levantar los contenedores.
- `docker compose ps` para ver qué servicios están corriendo.
- `docker compose logs mysql --tail 200` para revisar los logs de MySQL.
- `docker compose exec -T mysql mysqladmin -uroot -proot123 ping` para verificar que MySQL responde.
- `curl -X POST http://localhost:5000/api/Auth/login -H "Content-Type: application/json" -d '{"username":"admin","password":"admin123"}'` para obtener el token JWT.
- `curl -H "Authorization: Bearer <TOKEN>" http://localhost:5000/api/Products` para probar el endpoint protegido.

## 6. Qué hice yo

Tú realizaste las decisiones principales de diseño y funcionalidad:
- Elegir el dominio de productos y los campos de la entidad.
- Definir el uso de MySQL en Docker.
- Decidir que la API se pruebe con Swagger y autenticación JWT.
- Pedir que se generara evidencia y capturas para el examen.

## 7. Qué hice la IA

Yo implementé y apoyé con la configuración técnica:
- Añadí y ajusté Swagger en `Program.cs` para aceptar JWT Bearer.
- Verifiqué y confirmé la configuración de `docker-compose.yml` y MySQL.
- Generé las evidencias en archivos de texto y JSON.
- Tomé capturas de pantalla de Swagger y de los resultados de Docker/MySQL.
- Creé el PDF `EVIDENCIA_EXAMEN.pdf` que incluye las pruebas y capturas en imágenes.

## 8. Conclusión

La solución quedó estructurada con un modelo claro, separación de responsabilidades y un flujo de trabajo reproducible con Docker. El uso de Swagger facilita las pruebas y JWT asegura el acceso a los endpoints protegidos.
