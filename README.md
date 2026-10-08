# FirstDBApp

Aplicação de consola em **C# / .NET** para gerir clientes, com persistência em **SQL Server** através do **Entity Framework Core** (abordagem *code first*).

Projeto de aprendizagem baseado na ideia [Your First DB App](https://github.com/florinpop17/app-ideas/blob/master/Projects/1-Beginner/First-DB-App.md), do repositório *app-ideas*.

## Objetivos de aprendizagem

- Modelar uma entidade e mapeá-la para uma tabela (`DbContext`, `DbSet`, chave primária)
- Criar e aplicar *migrations*
- Implementar CRUD com EF Core (`SaveChanges`, *tracking*)
- Escrever consultas LINQ traduzidas para SQL
- Separar responsabilidades por camadas (apresentação, serviço, acesso a dados)
- Manter a *connection string* fora do código-fonte

## Funcionalidades

- [x] Adicionar cliente
- [x] Listar todos os clientes
- [x] Obter cliente por ID
- [x] Atualizar cliente
- [x] Eliminar cliente
- [x] Pesquisar cliente por nome


## Entidade

**Cliente**

| Campo      | Tipo       | Notas                          |
|------------|------------|--------------------------------|
| ClienteId  | int        | Chave primária                 |
| Nome       | string     | Obrigatório                    |
| Email      | string     | Obrigatório, formato validado  |
| Telefone   | string?    | Opcional                       |
| CriadoEm   | DateOnly   | Data de criação                |
| EstaAtivo  | bool       | Indica se o cliente está ativo |

## Regras de negócio

- Nome e email são obrigatórios.
- O email deve ter um formato aceitável (validação simples).
- Não são permitidos emails duplicados.
- Cliente inexistente é tratado com mensagem clara ao utilizador.

> Preenche aqui as decisões que tomaste: normalização do email (por exemplo, `Trim` + minúsculas), comprimentos máximos e se a exclusão é física ou lógica.

## Estrutura do projeto

```
FirstDBApp
|-- Modelos/Entidades        (Cliente)
|-- Infraestrutura           (DbContext, configuração, repositório, migrations)
|-- Servico                  (ClienteServico: operações e validação)
|-- Apresentacao/GestaoMenu  (ClienteMenu: menus e interação de consola)
|-- Program.cs               (configuração e arranque)
```

Fluxo entre camadas:

```
ClienteMenu  -->  ClienteServico  -->  Repositório  -->  DbContext  -->  SQL Server
(consola)         (regras)             (consultas)       (EF Core)
```

## Requisitos

- [.NET SDK](https://dotnet.microsoft.com/download) (indica aqui a versão que usas)
- Uma instância de **SQL Server** acessível (SQL Server Express, LocalDB ou Docker)
- Ferramenta do EF Core:

```bash
dotnet tool install --global dotnet-ef
```

## Configuração

A *connection string* **não** está no repositório. Configura-a localmente com *user secrets*:

```bash
cd FirstDBApp
dotnet user-secrets init
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Server=localhost;Database=FirstDBApp;User Id=<utilizador>;Password=<password>;TrustServerCertificate=True"
```

Ajusta o nome da chave ao que o teu `Program.cs` lê. Nunca publiques passwords ou tokens no Git.

## Base de dados

Aplica as migrations para criar a base de dados e a tabela:

```bash
dotnet ef database update
```

Depois de aplicar, confirma no SQL Server que a tabela `Clientes`, os tipos e o índice único do email correspondem ao modelo.

## Executar

```bash
dotnet run
```

## Menu

```
1 - Adicionar cliente
2 - Listar todos os clientes
3 - Obter cliente por ID
4 - Deletar cliente
5 - Atualizar cliente
6 - Pesquisar cliente por nome
0 - Sair
```

## Dados de exemplo

Os dados de exemplo (seed) são **fictícios**. Não são guardados dados pessoais reais no repositório.

## Testes

Planeado: testes básicos para campos obrigatórios, email duplicado, ID inexistente, edição e paginação.

## Evolução possível

- Moradas como entidade separada
- Importação de clientes a partir de CSV com resumo de erros
- Soft delete e auditoria
- Exportação de resultados
