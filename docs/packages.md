# 📦 Pacotes e Dependências

## 🔗 Abstrações
Pacotes que contém apenas contratos e interfaces, sem implementações concretas.

| Pacote | Versão | Descrição |
|--------|--------|-----------|
| `Microsoft.Extensions.Logging.Abstractions` | 9.0.6 | Abstrações para sistema de logging |
| `Mediator.Abstractions` | 3.0.0-preview.65 | Contratos do padrão Mediator com suporte a CQRS semântico |

## 🎯 Application Layer
Pacotes específicos da camada de aplicação, responsável por orquestrar as regras de negócio.

| Pacote | Versão | Descrição |
|--------|--------|-----------|
| `FluentValidation` | 12.0.0 | Biblioteca para validação fluente de objetos |
| `FluentValidation.DependencyInjectionExtensions` | 12.0.0 | Extensões para injeção de dependência do FluentValidation |
| `Mediator.SourceGenerator` | 3.0.0-preview.65 | Implementação high-performance do padrão Mediator usando Source Generators |

## 🏗️ Infrastructure Layer
Pacotes da camada de infraestrutura, responsável por persistência, logging e segurança.

| Pacote | Versão | Descrição |
|--------|--------|-----------|
| `Serilog.AspNetCore` | 9.0.0 | Framework de logging estruturado |
| `Microsoft.AspNetCore.Identity.EntityFrameworkCore` | 9.0.6 | ASP.NET Core Identity com Entity Framework |
| `Microsoft.EntityFrameworkCore` | 9.0.6 | ORM para acesso a dados |
| `Microsoft.EntityFrameworkCore.Relational` | 9.0.6 | Funcionalidades relacionais do EF Core |
| `Npgsql.EntityFrameworkCore.PostgreSQL` | 9.0.4 | Provider PostgreSQL para Entity Framework |
| `Microsoft.EntityFrameworkCore.Design` | 9.0.6 | Ferramentas de design-time do EF Core |
| `Microsoft.EntityFrameworkCore.InMemory` | 9.0.6 | Provider in-memory do EF Core para testes |
| `AspNetCore.HealthChecks.NpgSql` | 9.0.0 | Health check específico para PostgreSQL |
| `AspNetCore.HealthChecks.UI.Client` | 9.0.0 | Formatação rica de JSON para endpoints de health checks |

## 🌐 API Layer
Pacotes específicos da camada de apresentação (API Web).

| Pacote | Versão | Descrição |
|--------|--------|-----------|
| `Microsoft.AspNetCore.Authentication.JwtBearer` | 9.0.6  | Autenticação via JWT Bearer tokens |
| `Swashbuckle.AspNetCore` | 9.0.1  | Suporte para documentação Swagger/OpenAPI |

## 🧪 Testes
Ferramentas e bibliotecas para testes automatizados.

| Pacote | Versão | Descrição |
|--------|--------|-----------|
| `FluentAssertions` | 8.3.0 | Biblioteca para assertions mais legíveis em testes |
| `Moq` | 4.20.72 | Framework para criação de mocks e stubs |
| `Bogus` | 35.6.3 | Gerador de dados fake realistas para testes |
| `Microsoft.NET.Test.Sdk` | 17.14.0 | SDK base para execução de testes .NET |
| `xunit` | 2.9.3 | Framework de testes unitários |
| `xunit.runner.visualstudio` | 3.1.0 | Runner do xUnit para Visual Studio e dotnet test |
| `Microsoft.AspNetCore.Mvc.Testing` | 9.0.5 | Ferramentas para testes de integração de APIs |
| `Moq.EntityFrameworkCore` | 9.0.0.5 | Extensões do Moq específicas para Entity Framework |
| `MockQueryable.Moq` | 7.0.3 | Criação de mocks para IQueryable com Moq |

## ☁️ AWS Services
Pacotes para integração com serviços da Amazon Web Services.

| Pacote | Versão | Descrição |
|--------|--------|-----------|
| `AWSSDK.Extensions.NETCore.Setup` | 4.0.2 | Configuração e integração do AWS SDK com .NET Core |
| `AWSSDK.SimpleEmail` | 4.0.0.14 | Cliente para Amazon Simple Email Service (SES) |