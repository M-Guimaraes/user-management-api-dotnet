# User Management API

REST API desenvolvida com **ASP.NET Core 8** para gerenciamento de usuários, com foco em aprendizado de arquitetura backend moderna no ecossistema .NET.

O projeto implementa operações CRUD completas utilizando:

- ASP.NET Core
- Entity Framework Core
- PostgreSQL
- Docker
- Swagger/OpenAPI

---

# Funcionalidades

- Criar usuários
- Listar usuários
- Buscar usuário por ID
- Atualizar usuários
- Remover usuários
- Persistência com PostgreSQL
- Documentação automática com Swagger
- Validação de DTOs com Data Annotations

---

# Tecnologias Utilizadas

- .NET 8
- ASP.NET Core Web API
- Entity Framework Core
- PostgreSQL
- Docker Compose
- Swagger / OpenAPI

---

# Arquitetura

O projeto segue uma estrutura inspirada em boas práticas de APIs REST:

- Controllers
- Services
- DTOs
- Models
- Entity Framework Core
- Dependency Injection

---

# Estrutura do Projeto

```txt
.
├── Controllers/
├── DTOs/
├── Data/
├── Models/
├── Services/
├── Migrations/
├── Dockerfile
├── docker-compose.yml
├── appsettings.json
├── appsettings.Development.json
├── UserManagementApi.csproj
└── README.md
```

---

# Executando o Projeto

## 1. Configurar variáveis de ambiente

Crie os arquivos:

```txt
.env.dev
.env.prod
```

---

## Exemplo de `.env.dev`

```env
POSTGRES_DB=user_management_dev
POSTGRES_USER=postgres
POSTGRES_PASSWORD=postgres
POSTGRES_PORT=5432
```

---

## Exemplo de `.env.prod`

```env
POSTGRES_DB=user_management_prod
POSTGRES_USER=postgres
POSTGRES_PASSWORD=your_secure_password
POSTGRES_PORT=5432
```

---

# 2. Subir banco PostgreSQL

## Ambiente de desenvolvimento

```bash
docker compose --env-file .env.dev up -d
```

---

## Ambiente de produção

```bash
docker compose --env-file .env.prod up -d
```

---

# 3. Executar migrations

```bash
dotnet ef database update
```

---

# 4. Rodar aplicação

## Desenvolvimento

```bash
dotnet watch run
```

---

## Build da aplicação

```bash
dotnet build
```

---

## Publicar aplicação

```bash
dotnet publish -c Release
```

---

# Configuração do ASP.NET Core

O ASP.NET Core utiliza a variável:

```bash
ASPNETCORE_ENVIRONMENT
```

para definir o ambiente da aplicação.

---

## Desenvolvimento

```bash
ASPNETCORE_ENVIRONMENT=Development
```

---

## Produção

```bash
ASPNETCORE_ENVIRONMENT=Production
```

---

# Arquivos carregados automaticamente

| Ambiente | Arquivo |
|---|---|
| Development | `appsettings.Development.json` |
| Production | `appsettings.Production.json` |

---

# Swagger

Após iniciar a aplicação:

```txt
http://localhost:5139/swagger
```

---

# Git Ignore

Recomenda-se adicionar ao `.gitignore`:

```gitignore
bin/
obj/
.idea/

.env*
!.env.example

appsettings.Development.json

*.DotSettings
*.DotSettings.user
```

---

# Próximos passos do projeto

- Autenticação JWT
- Password Hashing
- Async/Await com EF Core
- Dockerização completa da API
- Deploy em cloud gratuita
- Testes automatizados
- CI/CD
- Repository Pattern
- FluentValidation
- Logging estruturado
- Health Checks

---

# Objetivo do Projeto

Este projeto foi criado com o objetivo de aprofundar conhecimentos em:

- ASP.NET Core
- APIs REST
- Entity Framework Core
- PostgreSQL
- Docker
- Arquitetura backend
- Boas práticas no ecossistema .NET
- Desenvolvimento backend enterprise