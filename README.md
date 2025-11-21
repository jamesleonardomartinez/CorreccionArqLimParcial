# BadCleanArch - Proyecto de Demostración

Proyecto de ejemplo de una API Web ASP.NET Core que demuestra correcciones de arquitectura limpia y análisis de calidad de código con SonarQube.

## 🏗️ Arquitectura

El proyecto sigue una estructura de Clean Architecture con las siguientes capas:

```
├── Domain/          # Entidades y servicios de dominio
├── Application/     # Casos de uso y lógica de aplicación
├── Infrastructure/  # Implementaciones de BD, logging y repositorios
└── WebApi/          # API REST y punto de entrada
```

## 🚀 Ejecutar el Proyecto

### Requisitos
- .NET 8.0 SDK
- (Opcional) SQL Server para persistencia de datos

### Compilar
```bash
dotnet build
```

### Ejecutar
```bash
cd src/WebApi
dotnet run
```

La aplicación estará disponible en: `http://localhost:5000`

## 📡 Endpoints Disponibles

- **GET** `/health` - Verificar estado de la aplicación
- **POST** `/orders` - Crear una nueva orden
  - Body: `customer,product,quantity,price` (separado por comas)
- **GET** `/orders/last` - Obtener últimas órdenes creadas
- **GET** `/info` - Información de configuración y versión

## ✅ Correcciones Realizadas

### Análisis con SonarQube
El proyecto ha sido analizado y corregido según las recomendaciones de SonarQube:

#### Domain Layer
- ✅ Campos públicos convertidos a propiedades en `Order.cs`
- ✅ Encapsulación mejorada en `OrderService.cs` usando `IReadOnlyList`

#### Infrastructure Layer
- ✅ Manejo de excepciones añadido en `BadDb.cs`
- ✅ Cierre apropiado de conexiones SQL
- ✅ Logging explícito de errores en `Logger.cs`

#### WebApi Layer
- ✅ Uso de `InvalidOperationException` específica en lugar de `Exception` genérica
- ✅ Endpoints async con `await` apropiado
- ✅ Uso de `RunAsync()` en lugar de `Run()`
- ✅ Manejo de errores mejorado con logging

## 🔧 Configuración

### Connection String
Por defecto usa:
```
Server=localhost;Database=master;User Id=sa;Password=SuperSecret123!;TrustServerCertificate=True
```

Puede configurarse en `appsettings.json`:
```json
{
  "ConnectionStrings": {
    "Sql": "tu-connection-string-aquí"
  }
}
```

## 📝 Notas

Este es un proyecto de demostración con implementaciones intencionales de "malas prácticas" que fueron corregidas. No está diseñado para uso en producción.

## 🛠️ Tecnologías

- ASP.NET Core 8.0
- System.Data.SqlClient
- Minimal APIs
- Clean Architecture (mejorada)
