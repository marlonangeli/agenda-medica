# agenda-medica
Este é um projeto de sistema de agendamento de consultas médicas utilizando ASP.NET Razor Pages e EF Core.

## Requisitos

- .NET 6.0 SDK

## Executando o projeto

- Clone o repositório
```bash
git clone https://github.com/marlonangeli/agenda-medica.git
```

- Acesse a pasta do projeto
```bash
cd agenda-medica
```

- Execute o projeto
```bash
cd src\Healthy.Web
dotnet run
```

- Acesse o projeto no navegador
```
https://localhost:5001
```

## Executando os testes

- Acesse a pasta do projeto de testes
```bash
cd tests\Healthy.Tests
```

- Execute os testes
```bash
dotnet test
```

## Refazer o banco de dados

- Acesse a pasta do projeto
```bash
cd src\Healthy.Data
```

- Execute o comando para refazer o banco de dados
```bash
dotnet ef database update
```

> No comando de criação o banco já é populado com alguns dados de exemplo.
