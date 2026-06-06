# Evidencia - API de Productos en C# Web API

## 1. Descripcion del proyecto

Se desarrollo una API REST en C# ASP.NET Core Web API para almacenar productos. La API permite:

- Crear producto.
- Obtener todos los productos.
- Obtener producto por ID.
- Actualizar producto.
- Eliminar producto.
- Probar los endpoints desde Swagger.
- Utilizar autenticacion JWT.
- Utilizar base de datos MySQL en un contenedor Docker.

## 2. Servicios en Docker

Comando para levantar el proyecto:

```powershell
docker compose up --build -d
```

Comando para verificar contenedores:

```powershell
docker compose ps
```

Servicios esperados:

- API: `http://localhost:5000`
- Swagger: `http://localhost:5000/swagger`
- MySQL: `localhost:3306`

Credenciales de MySQL:

- Base de datos: `productsdb`
- Usuario: `productsuser`
- Password: `productspass`
- Root password: `root123`

## 3. Verificacion de MySQL

Comando para entrar y verificar la tabla:

```powershell
docker compose exec mysql mysql -uproductsuser -pproductspass productsdb -e "SHOW TABLES; DESCRIBE products;"
```

Tabla esperada:

- `products`

Campos:

- `Id`
- `Name`
- `Description`
- `Price`
- `Stock`
- `CreatedAt`

## 4. Swagger

URL:

```text
http://localhost:5000/swagger
```

En Swagger se visualizan:

- `POST /api/Auth/login`
- `POST /api/Products`
- `GET /api/Products`
- `GET /api/Products/{id}`
- `PUT /api/Products/{id}`
- `DELETE /api/Products/{id}`

## 5. Autenticacion JWT en Swagger

Primero ejecutar:

```text
POST /api/Auth/login
```

Body:

```json
{
  "username": "admin",
  "password": "admin123"
}
```

La respuesta devuelve un token JWT.

Luego presionar el boton `Authorize` en Swagger y pegar:

```text
Bearer TOKEN_GENERADO
```

Despues de autorizar, ya se pueden probar los endpoints de productos.

## 6. Solicitudes de prueba en Swagger

### Crear producto

Endpoint:

```text
POST /api/Products
```

Body:

```json
{
  "name": "Laptop Lenovo",
  "description": "Laptop para pruebas del examen",
  "price": 7500.50,
  "stock": 8
}
```

Resultado esperado:

- Codigo `201 Created`.
- Devuelve el producto creado con su `id`.

### Obtener todos los productos

Endpoint:

```text
GET /api/Products
```

Resultado esperado:

- Codigo `200 OK`.
- Lista de productos almacenados.

### Obtener producto por ID

Endpoint:

```text
GET /api/Products/{id}
```

Ejemplo:

```text
GET /api/Products/1
```

Resultado esperado:

- Codigo `200 OK`.
- Producto solicitado.

### Actualizar producto

Endpoint:

```text
PUT /api/Products/{id}
```

Ejemplo:

```text
PUT /api/Products/1
```

Body:

```json
{
  "name": "Laptop Lenovo Actualizada",
  "description": "Producto actualizado desde Swagger",
  "price": 7200.00,
  "stock": 6
}
```

Resultado esperado:

- Codigo `204 No Content`.

### Eliminar producto

Endpoint:

```text
DELETE /api/Products/{id}
```

Ejemplo:

```text
DELETE /api/Products/1
```

Resultado esperado:

- Codigo `204 No Content`.

## 7. Capturas sugeridas para entregar

Tomar capturas de:

- `docker compose ps` mostrando `api` y `mysql` corriendo.
- Swagger abierto en `http://localhost:5000/swagger`.
- Login en Swagger devolviendo token JWT.
- Boton `Authorize` con token Bearer aplicado.
- `POST /api/Products` con respuesta `201 Created`.
- `GET /api/Products` mostrando productos.
- Consulta de MySQL mostrando tabla `products`.

## 8. Comandos utiles

Apagar contenedores:

```powershell
docker compose down
```

Levantar de nuevo:

```powershell
docker compose up --build -d
```

Ver logs:

```powershell
docker compose logs api
```
