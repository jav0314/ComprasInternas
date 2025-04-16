## 🛠 Requisitos Técnicos

- **Node.js** 20+
- **Angular CLI** 17
- **.NET 8 SDK**
- **SQL Server Express**

### 🔌 Puertos utilizados

- **Frontend**: [`http://localhost:4200`](http://localhost:4200)
- **Backend**: [`https://localhost:44327`](https://localhost:44327)

---

## 📁 Estructura del Proyecto


├── backend (.NET API)
│   ├── Controllers
│   ├── DAO
│   ├── Models
│   ├── Helpers
│   └── Program.cs

├── frontend (Angular)
│   ├── app
│   │   ├── principal.component.ts
│   │   ├── supervisor.component.ts
│   │   ├── aprobar-rechazar.component.ts
│   │   ├── services
│   │   └── Models


├── script.sql
└── README.md


---
📦 Repositorio Frontend: [github.com/jav0314/ComprasInternas](https://github.com/jav0314/ComprasInternas)
## 🚀 Pasos para Iniciar

1. **Restaurar la base de datos** usando `script.sql`.
2. **Ejecutar el backend** desde Visual Studio (usar perfil `IIS Express`).
3. **Navegar al frontend**:

   ```bash
   cd frontend
   npm install
   ng serve -o
