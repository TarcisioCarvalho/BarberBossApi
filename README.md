# 💈 BarberBoss API

> Sistema de gestão financeira para barbearias, permitindo controle completo de atendimentos, faturamento e geração de relatórios.

![.NET](https://img.shields.io/badge/.NET-8.0-512BD4?logo=dotnet)
![PostgreSQL](https://img.shields.io/badge/PostgreSQL-316192?logo=postgresql&logoColor=white)
![License](https://img.shields.io/badge/license-MIT-green)

---

## 📋 Sobre o Projeto

O **BarberBoss** é uma API REST desenvolvida para gerenciar o controle financeiro de barbearias. O sistema permite cadastrar atendimentos (billings), consultar histórico com filtros avançados, e gerar relatórios profissionais em Excel e PDF.

### ✨ Funcionalidades Principais

- ✅ **CRUD Completo** de atendimentos/faturas
- 🔍 **Paginação e Filtros** por período (data inicial/final)
- 📊 **Export para Excel** com formatação profissional
- 📄 **Export para PDF** com layout customizado
- ✔️ **Validações robustas** de dados
- 🧪 **Testes unitários** com xUnit e Moq
- 🏗️ **Clean Architecture** para manutenibilidade

---

## 🛠️ Tecnologias Utilizadas

### **Backend**
- **.NET 8** - Framework principal
- **ASP.NET Core** - Web API
- **Dapper** - Micro-ORM para acesso a dados
- **PostgreSQL** - Banco de dados relacional
- **FluentValidation** - Validações customizadas

### **Relatórios**
- **ClosedXML** - Geração de arquivos Excel (.xlsx)
- **PuppeteerSharp** - Geração de PDFs via Chromium headless

### **Testes**
- **xUnit** - Framework de testes
- **Moq** - Biblioteca para mocks

### **Arquitetura**
- **Clean Architecture** - Separação em camadas
- **Repository Pattern** - Abstração de acesso a dados
- **Dependency Injection** - Inversão de controle

---

## 📁 Estrutura do Projeto
```
BarberBoss/
├── BarberBoss.Api/              # Camada de apresentação (Controllers)
├── BarberBoss.Application/      # Casos de uso e lógica de negócio
├── BarberBoss.Domain/           # Entidades, DTOs e interfaces
├── BarberBoss.Infrastructure/   # Acesso a dados (Dapper + PostgreSQL)
├── BarberBoss.Communication/    # Contratos de Request/Response
├── BarberBoss.Exception/        # Exceções customizadas
└── BarberBoss.Tests/            # Testes unitários
```

---

## 🚀 Como Rodar o Projeto

### **Pré-requisitos**

- [.NET SDK 8.0+](https://dotnet.microsoft.com/download)
- [PostgreSQL 14+](https://www.postgresql.org/download/)
- IDE: Visual Studio 2022, Rider ou VS Code

---

### **1️⃣ Configurar o Banco de Dados**

**Criar o banco:**
```sql
CREATE DATABASE barberboss;
```

**Criar a tabela `billings`:**
```sql
CREATE TABLE billings (
    id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    date DATE NOT NULL,
    barber_name VARCHAR(100) NOT NULL,
    client_name VARCHAR(100) NOT NULL,
    service_name VARCHAR(100) NOT NULL,
    amount DECIMAL(10,2) NOT NULL,
    payment_method VARCHAR(50) NOT NULL,
    status VARCHAR(50) NOT NULL,
    notes TEXT,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);
```

**Criar tipos ENUM (PostgreSQL):**
```sql
CREATE TYPE payment_method AS ENUM ('Credit_Card', 'Cash', 'Pix', 'Other');
CREATE TYPE billing_status AS ENUM ('Pending', 'Paid', 'Overdue');
```

---

### **2️⃣ Configurar Connection String**

Edite o arquivo `appsettings.json` na camada **BarberBoss.Api**:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=barberboss;Username=seu_usuario;Password=sua_senha"
  }
}
```

---

### **3️⃣ Restaurar Dependências e Rodar**
```bash
# Navegar até a pasta da API
cd BarberBoss.Api

# Restaurar pacotes
dotnet restore

# Rodar a aplicação
dotnet run
```

A API estará disponível em: **`https://localhost:5001`**

Documentação Swagger: **`https://localhost:5001/swagger`**

---

### **4️⃣ Rodar os Testes**
```bash
# Na raiz da solution
dotnet test
```

---

## 📡 Endpoints da API

### **Billings (Atendimentos)**

| Método | Endpoint | Descrição |
|--------|----------|-----------|
| `GET` | `/api/billings` | Lista atendimentos (paginado) |
| `GET` | `/api/billings/{id}` | Busca atendimento por ID |
| `POST` | `/api/billings` | Cria novo atendimento |
| `PUT` | `/api/billings/{id}` | Atualiza atendimento |
| `DELETE` | `/api/billings/{id}` | Remove atendimento |

### **Relatórios**

| Método | Endpoint | Descrição |
|--------|----------|-----------|
| `GET` | `/api/billings/excel` | Gera relatório Excel |
| `GET` | `/api/billings/pdf` | Gera relatório PDF |

---

## 📊 Exemplos de Requisições

### **Listar Atendimentos (Paginado)**
```http
GET /api/billings?page=1&pageSize=10&startDate=2025-01-01&endDate=2025-01-31
```

**Response:**
```json
{
  "billings": [
    {
      "id": "123e4567-e89b-12d3-a456-426614174000",
      "date": "2025-01-15",
      "status": "Paid"
    }
  ],
  "currentPage": 1,
  "pageSize": 10,
  "totalPages": 5,
  "totalItems": 47,
  "totalAmount": 1500.00,
  "hasNextPage": true,
  "hasPreviousPage": false
}
```

---

### **Criar Atendimento**
```http
POST /api/billings
Content-Type: application/json

{
  "date": "2025-01-27",
  "barberName": "João Silva",
  "clientName": "Carlos Souza",
  "serviceName": "Corte de cabelo",
  "amount": 50.00,
  "paymentMethod": "Credit_Card",
  "status": "Paid",
  "notes": "Cliente parcelou em 2x"
}
```

---

### **Gerar Relatório PDF**
```http
GET /api/billings/pdf?startDate=2025-01-01&endDate=2025-01-31
```

Retorna arquivo PDF para download.

---

### **Gerar Relatório Excel**
```http
GET /api/billings/excel?startDate=2025-01-01&endDate=2025-01-31
```

Retorna arquivo `.xlsx` para download.

---

## 🧪 Testes

O projeto inclui testes unitários cobrindo:

- ✅ Validações de entrada
- ✅ Lógica de negócio (UseCases)
- ✅ Mocks de repositórios

**Executar testes:**
```bash
dotnet test
```

**Exemplo de teste:**
```csharp
[Fact]
public async Task Execute_ComDoisBillings_DeveRetornarDoisBillings()
{
    // Arrange
    var mockRepository = new Mock<IBillingReadOnlyRepository>();
    mockRepository.Setup(repo => repo.GetAll(...))
                  .ReturnsAsync((billings, 2, 200m));
    
    var useCase = new GetAllBillingsUseCase(mockRepository.Object);
    
    // Act
    var result = await useCase.Execute(request);
    
    // Assert
    Assert.Equal(2, result.Billings.Count());
}
```

---

## 🏗️ Arquitetura

O projeto segue os princípios de **Clean Architecture**:
```
┌──────────────────────────────────────────────────────┐
│              API (Controllers)                       │
│  ► Recebe requisições HTTP                          │
│  ► Retorna respostas JSON                           │
│  ► Validação inicial de entrada                     │
└──────────────────────────────────────────────────────┘
                         ↓
┌──────────────────────────────────────────────────────┐
│          Communication (Contracts)                   │
│  ► Request DTOs (RequestBillingJson)                │
│  ► Response DTOs (ResponseBillingsJson)             │
│  ► Contratos de entrada e saída da API              │
└──────────────────────────────────────────────────────┘
                         ↓
┌──────────────────────────────────────────────────────┐
│          Application (UseCases)                      │
│  ► Lógica de negócio (CreateBillingUseCase)        │
│  ► Validações de regras (BillingValidator)          │
│  ► Orquestração de operações                        │
└──────────────────────────────────────────────────────┘
                         ↓
┌──────────────────────────────────────────────────────┐
│          Domain (Core)                               │
│  ► Entidades (Billing)                              │
│  ► Enums (Status, PaymentMethod)                    │
│  ► Interfaces de Repositórios                       │
│  ► DTOs internos (BillingShort, BillingExport)     │
└──────────────────────────────────────────────────────┘
                         ↓
┌──────────────────────────────────────────────────────┐
│          Infrastructure (Data Access)                │
│  ► Implementação de Repositórios                    │
│  ► Dapper (queries SQL)                             │
│  ► Conexão com PostgreSQL                           │
└──────────────────────────────────────────────────────┘
                         
┌──────────────────────────────────────────────────────┐
│          Exception (Error Handling)                  │
│  ► Exceções customizadas                            │
│  ► ErrorOnValidationException                        │
│  ► NotFoundException                                 │
│  ► Tratamento centralizado de erros                 │
└──────────────────────────────────────────────────────┘
```

**Benefícios:**
- ✅ Separação de responsabilidades
- ✅ Testabilidade
- ✅ Fácil manutenção
- ✅ Independência de frameworks

---

## 📝 Próximos Passos / Melhorias Futuras

- [ ] Autenticação e Autorização (JWT)
- [ ] Logging estruturado (Serilog)
- [ ] Cache (Redis)
- [ ] Docker e Docker Compose
- [ ] CI/CD (GitHub Actions)
- [ ] Migrations automáticas (FluentMigrator/EF)

---

## 👨‍💻 Autor

**[Tarcísio José de Amorim Carvalho]**
- LinkedIn: (https://www.linkedin.com/in/tarcisio-jose-de-amorim-carvalho/)
- GitHub: (https://github.com/TarcisioCarvalho)

---

## 📄 Licença

Este projeto está sob a licença MIT. Veja o arquivo [LICENSE](LICENSE) para mais detalhes.

---

## 🙏 Agradecimentos

Projeto desenvolvido como parte de estudos em **.NET**, **Clean Architecture** e **boas práticas de desenvolvimento**.

---

⭐ **Se este projeto te ajudou, deixe uma estrela no repositório!**
