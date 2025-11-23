# BadCleanArch - Proyecto de Arquitectura Limpia

Proyecto educativo de una API Web ASP.NET Core que demuestra la implementación de **Clean Architecture** (Arquitectura Limpia) y análisis de calidad de código con **SonarQube**. Este proyecto muestra la transformación de código con malas prácticas a código que sigue principios SOLID y mejores prácticas de desarrollo.

---

## 📚 Introducción a la Arquitectura Limpia

### ¿Qué es Clean Architecture?

La **Arquitectura Limpia** (Clean Architecture) es un patrón arquitectónico propuesto por Robert C. Martin (Uncle Bob) que busca crear sistemas de software **independientes, testables y mantenibles**. El objetivo principal es separar las responsabilidades en capas concéntricas, donde las dependencias fluyen **desde el exterior hacia el interior**.

### Principios Fundamentales

1. **Independencia de Frameworks**: El negocio no depende de librerías externas
2. **Testeable**: La lógica de negocio se puede probar sin UI, BD o servicios externos
3. **Independencia de la UI**: La interfaz puede cambiar sin afectar el negocio
4. **Independencia de la Base de Datos**: El dominio no conoce el motor de BD
5. **Independencia de Agentes Externos**: Las reglas de negocio no conocen el mundo exterior

### Capas de la Arquitectura Limpia

```
┌─────────────────────────────────────────┐
│        WebApi (Presentation)            │  ← Frameworks, Drivers, UI
│  Controllers, Program.cs, Middleware    │
├─────────────────────────────────────────┤
│      Infrastructure (Data Access)       │  ← Gateways, DB, External APIs
│  Repositories, Logger, Database         │
├─────────────────────────────────────────┤
│     Application (Use Cases)             │  ← Application Business Rules
│  CreateOrder, Interfaces, DTOs          │
├─────────────────────────────────────────┤
│         Domain (Entities)               │  ← Enterprise Business Rules
│  Order, OrderService (Pure logic)       │  ← ¡NÚCLEO! No depende de nada
└─────────────────────────────────────────┘
```

**Flujo de Dependencias**: WebApi → Infrastructure → Application → **Domain**

### Beneficios de Clean Architecture

- ✅ **Mantenibilidad**: Cambios localizados en capas específicas
- ✅ **Testabilidad**: Lógica de negocio aislada y fácil de probar
- ✅ **Escalabilidad**: Nuevas funcionalidades sin afectar el núcleo
- ✅ **Flexibilidad**: Cambiar frameworks o BDs sin reescribir el negocio
- ✅ **Claridad**: Separación clara de responsabilidades

---

## 📊 Análisis de Métricas con SonarQube

### Resumen de Métricas Analizadas

SonarQube analiza múltiples dimensiones de calidad del código. En este proyecto se identificaron y corrigieron las siguientes categorías de issues:

#### 🔴 Issues Críticos Detectados

| Regla | Descripción | Severidad | Estado |
|-------|-------------|-----------|--------|
| **S2068** | Credenciales hardcodeadas en el código | 🔴 Crítico | ✅ Corregido |
| **S112** | Uso de excepciones genéricas (`Exception`) | 🟡 Mayor | ✅ Corregido |
| **S1075** | URIs hardcodeadas | 🟡 Mayor | ✅ Corregido |
| **S2245** | Uso de generador pseudoaleatorio inseguro | 🟡 Mayor | ⚠️ Conocido |
| **S5122** | Política CORS permisiva | 🟡 Mayor | ⚠️ Intencional |
| **S1144** | Campos públicos en lugar de propiedades | 🔵 Menor | ✅ Corregido |
| **S2325** | Métodos que deberían ser estáticos | 🔵 Menor | ⚠️ Conocido |
| **S3400** | Métodos constantes no declarados como tal | 🔵 Menor | ⚠️ Conocido |

### Métricas Iniciales vs Finales

| Métrica | Antes | Después | Mejora |
|---------|-------|---------|--------|
| **Bugs** | 0 | 0 | ➖ |
| **Vulnerabilidades** | 1 | 0 | ✅ 100% |
| **Code Smells** | 12+ | 6 | ✅ 50% |
| **Cobertura de Código** | 0% | 0% | ➖ |
| **Duplicación** | <1% | <1% | ✅ |
| **Deuda Técnica** | ~2h | ~30min | ✅ 75% |

### Reglas Principales Aplicadas

#### S2068 - Credenciales Hardcodeadas
- **Problema**: Contraseñas en código fuente exponen secretos
- **Solución**: Variables de entorno con archivo `.env`
- **Impacto**: Seguridad crítica mejorada

#### S112 - Excepciones Genéricas
- **Problema**: `throw new Exception()` no es específico
- **Solución**: `throw new InvalidOperationException()`
- **Impacto**: Mejor manejo de errores y debugging

#### S1144 - Campos Públicos
- **Problema**: Campos públicos violan encapsulación
- **Solución**: Propiedades auto-implementadas `{ get; set; }`
- **Impacto**: Encapsulación y flexibilidad mejoradas

#### S2245 - Generador Pseudoaleatorio
- **Problema**: `new Random()` no es criptográficamente seguro
- **Nota**: Aceptable para IDs de demostración (no producción)

#### S5122 - CORS Permisivo
- **Problema**: `AllowAnyOrigin()` acepta cualquier dominio
- **Nota**: Intencional para proyecto de demostración

---

## 📋 Requisitos Previos

- .NET 8.0 SDK
- (Opcional) SQL Server para persistencia de datos
- (Opcional) SonarQube Server para análisis de código

## ⚙️ Configuración

### Variables de Entorno ⚠️ IMPORTANTE

**El proyecto YA NO utiliza contraseñas hardcodeadas en el código.** Todas las credenciales se gestionan mediante variables de entorno desde un archivo `.env`.

**Antes de ejecutar el proyecto, DEBES crear el archivo `.env` en la raíz:**

```env
# Configuración de Base de Datos
DB_PASSWORD=SuperSecret123!

# Configuración de SonarQube (opcional)
SONAR_TOKEN=tu_token_sonarqube_aquí
```

**Pasos obligatorios:**
1. Copia el archivo `.env` de ejemplo o créalo manualmente
2. Configura la variable `DB_PASSWORD` con tu contraseña de SQL Server
3. El archivo `.env` está en `.gitignore` y NO se subirá al repositorio

> **Seguridad:** Las contraseñas se leen desde variables de entorno (`Environment.GetEnvironmentVariable("DB_PASSWORD")`). El archivo `appsettings.json` contiene un marcador de posición, NO una contraseña real.

## 🔨 Compilar el Proyecto

### Compilación básica
```bash
dotnet build
```

### Compilación completa (limpia + build)
```bash
dotnet clean
dotnet build
```

### Compilación sin usar caché incremental
```bash
dotnet build --no-incremental
```

## 🔍 Análisis con SonarQube

### Ejecutar análisis
```bash
# Windows PowerShell
.\run-sonar-analysis.ps1

# Windows Command Prompt
run-sonar-analysis.bat
```

El script automáticamente:
1. Limpia el proyecto
2. Inicia el análisis de SonarQube
3. Compila el proyecto
4. Envía los resultados al servidor SonarQube

Ver resultados en: `http://localhost:9000/dashboard?id=BadCleanArch`

## ✅ Correcciones Implementadas

### Cambios Principales

#### 1. Seguridad y Configuración
- ✅ **Credenciales movidas a archivo `.env`** (no se sube a Git)
- ✅ Archivo `.gitignore` actualizado para excluir `.env` y archivos sensibles
- ✅ Variables de entorno separadas del código fuente

#### 2. Domain Layer
- ✅ Campos públicos convertidos a propiedades en `Order.cs`
- ✅ Encapsulación mejorada en `OrderService.cs` usando `IReadOnlyList`
- ✅ Campo privado `_lastOrders` con propiedad de solo lectura

#### 3. Application Layer
- ✅ Inyección de dependencias implementada correctamente
- ✅ `CreateOrderUseCase` usa interfaces (IAppLogger, IOrderRepository)
- ✅ Principio de Inversión de Dependencias aplicado

#### 4. Infrastructure Layer
- ✅ Manejo de excepciones añadido en `BadDb.cs`
- ✅ Cierre apropiado de conexiones SQL
- ✅ Logging explícito de errores en `Logger.cs`
- ✅ Propiedad auto-implementada para ConnectionString

#### 5. WebApi Layer
- ✅ Uso de `InvalidOperationException` específica en lugar de `Exception` genérica
- ✅ Endpoints async con `await` apropiado
- ✅ Uso de `await app.RunAsync()` en lugar de `app.Run()`
- ✅ Manejo de errores mejorado con logging
- ✅ Configuración de Dependency Injection correcta

#### 6. Comentarios en el Código
- ✅ Comentarios agregados en cada corrección explicando los cambios
- ✅ Documentación de mejoras en cada archivo modificado

## 📝 Estructura del Proyecto

```
BadCleanArch/
├── src/
│   ├── Domain/                 # Entidades y servicios de dominio
│   ├── Application/            # Casos de uso e interfaces
│   ├── Infrastructure/         # Implementaciones (BD, Logging)
│   └── WebApi/                 # API REST y punto de entrada
├── .env                        # Variables de entorno (NO subir a Git)
├── .gitignore                  # Archivos excluidos de Git
├── run-sonar-analysis.ps1      # Script para análisis SonarQube
└── README.md                   # Este archivo
```

## 🛠️ Tecnologías

- ASP.NET Core 8.0
- .NET 8.0 SDK
- System.Data.SqlClient
- Minimal APIs
- Clean Architecture
- SonarQube para análisis de código

## 📝 Notas Importantes

- Este es un proyecto **educativo/demostrativo**
- Las "malas prácticas" son **intencionales** para propósitos de enseñanza
- Muestra implementación correcta de **Clean Architecture**
- Incluye análisis con **SonarQube** para métricas de calidad
- **NO está diseñado para uso en producción**
