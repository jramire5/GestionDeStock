Este es un proyecto de una API gestión de productos, utilizando una solución .NET, con 2 proyectos, uno de biblioteca de clases, y otro ASP.
Permite cargar productos de 2 categorías (1 y 2) y tiene una función que permite, a partir de un presupuesto, calcular cuales
son los 2 productos de distintas categorias que, sumados, sean lo mas cercano al presupuesto, pero sin pasarlo.

Posee autenticación.

Se puede ejecutar tanto en MySQL como en SQL Server.

Para utilizarlo, primero se debe configurar la conexión a la base de datos, se la debe configurar en DBContext, en el proyecto de Biblioteca de Clases

Para poder utilizar la API se necesita, primero, loguearse para obtener un token, que luego se debe utilizar en un header
de Key: Authorization y Value: Bearer + el token, por ejemplo:

Key: Authorization || Value: bearer eyJcbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6ImFkbWluIiwibmJmIjox...


ENDPOINTS:


POST
/productos
Body tipo JSON
{
  "id": 0,
  "precio": 0,
  "categoria": 0,
  "fechaDeCarga": "2024-09-22T23:45:46.680Z"
}
GET
/productos

PUT
/productos

Body tipo JSON
{
  "id": 0,
  "precio": 0,
  "categoria": 0,
  "fechaDeCarga": "2024-09-22T23:45:46.680Z"
}

DELETE
/productos/{id}

GET
/ProductosFiltrados/{presupuesto}

Parametro tipo INT

POST
/auth/login
Body tipo JSON
{
  "usuario": "string",
  "contrasena": "string"
}
