# PC3 - Sistema de Gestión de Tareas con Análisis de Sentimiento

## Descripción

Este proyecto es una aplicación web desarrollada en ASP.NET Core MVC y .NET 10.

Permite:

* Gestionar tareas mediante una API REST.
* Consultar tareas de una API externa (JSONPlaceholder).
* Analizar comentarios y determinar si son positivos o negativos usando Machine Learning (ML.NET).
* Probar los servicios mediante Swagger.

---

## Funcionalidades

### Gestión de Tareas

Permite realizar las operaciones básicas:

* Crear tareas
* Listar tareas
* Buscar tareas por ID
* Actualizar tareas
* Eliminar tareas

Además, las tareas pueden filtrarse por:

* Estado
* Prioridad
* Fecha

### Consumo de API Externa

El sistema obtiene tareas desde JSONPlaceholder y las muestra utilizando un modelo propio.

### Análisis de Sentimiento

El usuario envía un comentario y el sistema determina si el sentimiento es:

* Positivo
* Negativo

Ejemplo:

Comentario:
"Este producto es excelente"

Resultado:
"Positivo"

---

## Tecnologías Utilizadas

* ASP.NET Core MVC
* .NET 10
* ML.NET
* Swagger
* Bootstrap 5
* jQuery

---

## Estructura Principal

### Controllers

Contienen la lógica de las APIs y las vistas.

* HomeController
* TareasController
* TareasExternasController
* MlController

### Models

Contienen los modelos de datos.

* Tarea
* TareaExternaDto

### MLModel

Contiene el modelo de inteligencia artificial.

* SentimientoService
* dataset_sentimiento.csv

---

## Endpoints Principales

### API de Tareas

GET /api/tareas

Obtiene todas las tareas.

POST /api/tareas

Crea una nueva tarea.

PUT /api/tareas/{id}

Actualiza una tarea.

DELETE /api/tareas/{id}

Elimina una tarea.

### API Externa

GET /api/tareas-externas

Obtiene tareas desde JSONPlaceholder.

### Análisis de Sentimiento

POST /api/ml/sentimiento

Analiza un comentario y devuelve si es positivo o negativo.

---

## Ejecución del Proyecto

Clonar el repositorio:

git clone <url-del-repositorio>

Ingresar al proyecto:

cd Pc3_Cabana

Ejecutar:

dotnet run

Luego abrir el navegador y acceder a:

[https://localhost:xxxx](https://localhost:xxxx)

Swagger:

[https://localhost:xxxx/swagger](https://localhost:xxxx/swagger)

---

## Conclusión

Este proyecto integra desarrollo web, consumo de APIs externas y Machine Learning en una sola aplicación, permitiendo gestionar tareas y analizar sentimientos de comentarios de forma automática.
