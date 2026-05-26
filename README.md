# User Management API

REST API desenvolvida com **ASP.NET Core 8** para gerenciamento de usuários, com foco em aprendizado de arquitetura backend moderna no ecossistema .NET.

O projeto implementa operações CRUD completas utilizando:
- ASP.NET Core
- Entity Framework Core
- PostgreSQL
- Docker
- Swagger/OpenAPI

## Funcionalidades

- Criar usuários
- Listar usuários
- Buscar usuário por ID
- Atualizar usuários
- Remover usuários
- Persistência com PostgreSQL
- Documentação automática com Swagger
- Validação de DTOs com Data Annotations

## Tecnologias Utilizadas

- .NET 8
- ASP.NET Core Web API
- Entity Framework Core
- PostgreSQL
- Docker Compose
- Swagger / OpenAPI

## Arquitetura

O projeto segue uma estrutura inspirada em boas práticas de APIs REST:

- Controllers
- Services
- DTOs
- Models
- Entity Framework Core
- Dependency Injection

## Executando o projeto

### Subir banco PostgreSQL

```bash
docker compose up -d
```

### Executar migrations

```bash
dotnet ef database update
```

### Rodar aplicação

```bash
dotnet run
```

## Swagger

Após iniciar a aplicação:

```txt
http://localhost:5139/swagger
```

## Próximos passos do projeto

- Autenticação JWT
- Password Hashing
- Async/Await com EF Core
- Dockerização da API
- Deploy em cloud gratuita
- Testes automatizados
- CI/CD
