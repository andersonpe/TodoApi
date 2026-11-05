# TodoApi: API de Gestão de Tarefas

API RESTful em **.NET 10** para gerenciar tarefas (ToDo): criar, listar, atualizar e remover, com filtros por status e data de vencimento.

## Tecnologias

- .NET 10 (ASP.NET Core, Controllers)
- Entity Framework Core 10 + SQLite
- Swagger / OpenAPI (Swashbuckle)
- xUnit + Moq (testes unitários)
- Docker e Docker Compose

## Arquitetura e padrões

```
src/
├── TodoApi.Domain          # Entidades e contratos (interfaces)
├── TodoApi.Application     # DTOs e serviços (regras de negócio)
├── TodoApi.Infrastructure  # EF Core, DbContext, Repository e Unit of Work
└── TodoApi.Api             # Controllers, Program.cs e configuração
tests/
└── TodoApi.Tests           # Testes unitários
```

- **Repository** e **Unit of Work** isolam o acesso a dados.
- **Injeção de Dependência** configurada no `Program.cs`.
- Camadas com dependências apontando para dentro: o `Domain` não depende de ninguém.
- **Migrations** aplicadas automaticamente ao iniciar a aplicação.

**Por que SQLite?** Não exige instalar nem configurar um servidor de banco, então qualquer pessoa clona o projeto e executa. Por usar o EF Core, trocar para SQL Server ou PostgreSQL exige apenas mudar o provider e a connection string.

## Modelo da tarefa

| Campo | Tipo | Descrição |
|---|---|---|
| `id` | inteiro | Identificador (gerado automaticamente) |
| `titulo` | texto (até 200) | Obrigatório |
| `descricao` | texto (até 1000) | Opcional |
| `status` | enum | `Pendente`, `EmAndamento` ou `Concluido` |
| `dataDeVencimento` | data/hora | Data de vencimento da tarefa |

## Pré-requisitos

- [.NET SDK 10](https://dotnet.microsoft.com/download)
- [Docker Desktop](https://www.docker.com/products/docker-desktop/) (opcional, para rodar em container)
- Git

## Como executar localmente

```bash
git clone https://github.com/andersonpe/todo-api.git
cd todo-api

dotnet restore
dotnet run --project src/TodoApi.Api
```

O banco `todo.db` é criado automaticamente na primeira execução. O endereço aparece no console; por padrão:

- HTTP: `http://localhost:5064`

**Swagger:** acesse `/swagger` no endereço exibido, por exemplo `http://localhost:5064/swagger`.

### Scripts PowerShell (Windows)

Na raiz do projeto:

```powershell
.\buildar-e-rodar.ps1                   # compila e inicia a API (perfil http)
.\migrar-e-compilar.ps1 -Nome MinhaMigration   # cria uma nova migration e compila
```

Se o PowerShell bloquear a execução de scripts:

```powershell
Set-ExecutionPolicy -Scope Process -ExecutionPolicy Bypass
```

## Como executar com Docker

```bash
docker compose up --build
```

- Swagger: `http://localhost:8080/swagger`
- Os dados ficam no volume `todo-data` e sobrevivem a reinícios do container.
- Para apagar o container e **todos os dados**: `docker compose down -v`

Sem o Compose:

```bash
docker build -t andersonpdasilva/todo-api:1.0 .
docker run -p 8080:8080 andersonpdasilva/todo-api:1.0
```

Imagem publicada no Docker Hub: `https://hub.docker.com/r/andersonpdasilva/todo-api`

## Testes

```bash
dotnet test
```

## Endpoints

Base: `/api/Todos`

| Método | Rota | Descrição | Respostas |
|---|---|---|---|
| `GET` | `/api/Todos` | Lista tarefas (aceita filtros) | 200 |
| `GET` | `/api/Todos/{id}` | Busca uma tarefa por ID | 200, 404 |
| `POST` | `/api/Todos` | Cria uma tarefa | 201, 400 |
| `PUT` | `/api/Todos/{id}` | Atualiza uma tarefa | 204, 400, 404 |
| `DELETE` | `/api/Todos/{id}` | Remove uma tarefa | 204, 404 |

### Filtros (`GET /api/Todos`)

| Parâmetro | Exemplo | Descrição |
|---|---|---|
| `status` | `Pendente` | Filtra pelo status |
| `dataDeVencimento` | `2026-11-10` | Filtra pela data de vencimento |

Exemplo:

```
GET /api/Todos?status=Pendente&dataDeVencimento=2026-11-10
```

### Exemplo de corpo (`POST` / `PUT`)

```json
{
  "titulo": "Entregar desafio",
  "descricao": "Subir o projeto no GitHub",
  "status": "Pendente",
  "dataDeVencimento": "2026-11-10T00:00:00"
}
```
