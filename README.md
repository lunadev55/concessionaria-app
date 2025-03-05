# Concessionaria App

Este projeto é uma Web Application desenvolvida com .NET 9.0, utilizando a arquitetura limpa (Clean Architecture) e outras boas práticas de desenvolvimento. A aplicação oferece funcionalidades de registro e login de usuários, bem como a criação de Veiculos, Concessionarias, etc.

![home-screen](https://github.com/user-attachments/assets/a1582fca-be35-4754-9d5b-82dd940fd5fb)
![home-logado](https://github.com/user-attachments/assets/1b165ac3-640e-45d7-8d0b-451e9b802f9e)
![fluxo-requisicao](https://github.com/user-attachments/assets/ea7edad9-312b-4797-b948-c6b13a9a3aef)
![diagrama-mssqlserver](https://github.com/user-attachments/assets/e339294e-f7a5-486a-a755-6a6435560c3e)

Segue links para vídeos com explicação sobre Arquitetura, Código e Aplicação funcionando:

https://www.loom.com/share/51eb6f6c38894ef2854abd08ba5d8807
https://www.loom.com/share/8f140ff409374767930a9c0d10354807
https://www.loom.com/share/cdbab0552b514fe28d3d8fb533b7e7bc

## Tecnologias Utilizadas

- **.NET 9.0**
- **Entity Framework Core** (Code First)
- **SQL Server** como banco de dados
- **AutoMapper** para mapeamento entre entidades e DTOs
- **Dependency Injection**
- **Middleware simples**

## Funcionalidades

1. **Autenticação e Autorização**
   - Registro de usuários.
   - Login de usuários com e geração de JWT Token.
   - Autorização de acesso com base no JWT.

2. **Cadastro de Veiculo**
   - Criação de veiculos, incluindo validação de dados.
   - Uso do AutoMapper para conversão de objetos DTO para entidade e vice-versa.

3. **Realização de Vendas**
   - Realização de Vendas atrelando cliente, veiculo e concessionaria.

4. **Cadastro de Concessionarias**
   - CRUD Concessionarias.

6. **Logging**
   - Implementação de logging para registrar as ações e erros do sistema.

7. **Middleware**
   - Middleware simples para log the requests e responses do sistema.

## Estrutura do Projeto

O projeto segue a arquitetura limpa, organizada da seguinte forma:

- **Application**: Contém os casos de uso, serviços e validações.
- **Domain**: Contém as entidades de domínio.
- **Data**: Contem a implementação de acesso ao banco de dados, repositórios e contextos de banco.
- **Infrastructure**
- **WebApi**: Contém os controladores e configurações de API.

## Como Rodar o Projeto

### 1. Clonar o Repositório

```bash
git clone https://github.com/lunadev55/concessionaria-app.git
cd concessionaria-app
```

### 2. Configurar o Banco de Dados 

Altere a string de conexão no arquivo `appsettings.json` (ConcessionariaApp.WEB) para a sua configuração do SQL Server:

```bash
"ConnectionStrings": {
  "DefaultConnection": "Server=SEU_SERVIDOR;Database=SEU_BANCO_DE_DADOS;User Id=SEU_USUARIO;Password=SUA_SENHA;"
}
```

### 3. Criar e Executar a Migração

Para criar as Migrations é preciso rodar os comandos no contexto (tendo como target o projeto AntecipacaoRecebivel.Data):

```bash
`dotnet ef migrations add Initial`
```

Para criar o banco de dados e as tabelas, rode o seguinte comando no terminal:

```bash
`dotnet ef database update`

```

### 4. Rodar o Projeto

Execute o comando abaixo para rodar a aplicação:

`dotnet run`

A API estará disponível em `(https://localhost:7188;http://localhost:5078)`.

## **Testes Unitários**

O projeto inclui testes unitários que validam o funcionamento das funcionalidades principais. Para rodar os testes, use o seguinte comando:

`dotnet test`
