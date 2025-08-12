# ☕ Colombian Coffee - Aplicación de Escritorio

![SOLID](https://img.shields.io/badge/SOLID-black.svg?style=for-the-badge)![Arquitectura Hexagonal](https://img.shields.io/badge/Hexagonal_Architecture-blue.svg?style=for-the-badge)![Vertical Slicing](https://img.shields.io/badge/Vertical_Slicing-purple.svg?style=for-the-badge)![Git Flow](https://img.shields.io/badge/Git_Flow-F05032.svg?style=for-the-badge&logo=git&logoColor=white)![Conventional Commits](https://img.shields.io/badge/Conventional%20Commits-FE5196.svg?style=for-the-badge&logo=conventional-commits&logoColor=white)![.NET 9](https://img.shields.io/badge/.NET_9-512BD4.svg?style=for-the-badge&logo=.net&logoColor=white)![C#](https://img.shields.io/badge/C%23-239120.svg?style=for-the-badge&logo=c-sharp&logoColor=white)![Entity Framework Core](https://img.shields.io/badge/Entity_Framework_Core-0078D7.svg?style=for-the-badge&logo=nuget&logoColor=white)![MySQL](https://img.shields.io/badge/MySQL-4479A1.svg?style=for-the-badge&logo=mysql&logoColor=white)![Spectre.Console](https://img.shields.io/badge/Spectre.Console-212121.svg?style=for-the-badge&logo=nuget&logoColor=white)![BCrypt.Net](https://img.shields.io/badge/BCrypt.Net-orange.svg?style=for-the-badge&logo=nuget&logoColor=white)![QuestPDF](https://img.shields.io/badge/QuestPDF-56687A.svg?style=for-the-badge&logo=nuget&logoColor=white)[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg?style=for-the-badge)](https://opensource.org/licenses/MIT)

---
## 📖 Descripción del Proyecto

Colombian Coffee es una aplicación de escritorio desarrollada en **C# (.NET)** con **Entity Framework** y **MySQL**, diseñada para catalogar, filtrar y gestionar las principales variedades de café cultivadas en Colombia.

La aplicación sigue principios **SOLID** y arquitectura **Puertos y Adaptadores (Vertical Slicing)**, con módulos independientes para autenticación, gestión de variedades y exportación a PDF.

---

## 📂 Estructura del proyecto
```bash
ColombianCoffee/
├── ColombianCoffee.csproj
├── Docs
├── Program.cs
└── Src
    ├── Modules
    │   ├── Auth
    │   │   ├── Application
    │   │   │   ├── Interfaces
    │   │   │   ├── Services
    │   │   │   └── UI
    │   │   ├── Domain
    │   │   │   └── Entities
    │   │   └── Infraestructure
    │   │       └── Repositories
    │   ├── PDFExport
    │   │   ├── Application
    │   │   │   ├── Interfaces
    │   │   │   ├── Services
    │   │   │   └── UI
    │   │   ├── Domain
    │   │   │   └── Entities
    │   │   └── Infraestructure
    │   │       └── Repositories
    |   ├── MainMenu
    |   |       └── UI
    │   └── Varieties
    │       ├── Application
    │       │   ├── Interfaces
    │       │   ├── Services
    │       │   └── UI
    │       ├── Domain
    │       │   └── Entities
    │       └── Infraestructure
    │           └── Repositories
    └── Shared
        ├── Contexts
        ├── Helpers
        ├── Utils
        └── Validators
```

##  🛠️ Instalación

```bash
# Clonar el repositorio
git clone https://github.com/jcristancho2/ColombianCoffee.git
cd ColombianCoffee

# Instalar dependencias de .NET
dotnet restore
```

## 🚀 Ejecución
```bash
dotnet run
```

## 📦 Tecnologías utilizadas

- C# (.NET 9)
- Entity Framework Core
- MySQL
- Spectre.Console (UI consola avanzada)
- BCrypt.Net (hash de contraseñas)
- QuestPDF (generación de PDF)
- Git Flow + Conventional Commits


## 🤝 Contribuciones

¡Las contribuciones son bienvenidas! Si deseas mejorar el proyecto, sigue estas directrices:

    Haz un fork del repositorio.

    Crea una nueva rama (git checkout -b feature/nueva-caracteristica).

    Realiza tus cambios y asegúrate de que el código pase las pruebas.

    Realiza un commit usando Conventional Commits.

    Envía tus cambios (git push origin feature/nueva-caracteristica).

    Abre un Pull Request.

## 🧑‍💻 Autor

- [**Jorge Andrés Cristancho**](https://github.com/jcristancho2) - Lider de proyecto
- [**Luis Felipe Díaz Correa**](https://github.com/LFDIAZDEV2209) - Desarrollador Fullstack
- [**Sheyla Esther Samur Rojas**](https://github.com/sheyla08samur) - Desarrollo Base de Datos /UX
- [**Leidy Johana Niño Villegas**](https://github.com/LeidyJohanaVillegas) - Desarrollador Fullstack /Ux



## 📝 Licencia
Este proyecto está bajo la licencia [MIT](LICENSE).