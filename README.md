# Sistema de Gestão de Consultas UVV

Projeto prático desenvolvido para a disciplina de **Desenvolvimento Web Back-end** da Universidade Vila Velha (UVV).

Feito por: Natan Pasolini Oliveira Bezerra

## Arquitetura do Projeto
- **Framework:** ASP.NET Core MVC (.NET 8.0)
- **ORM:** Entity Framework Core (Abordagem Code First com Migrations)
- **Banco de Dados:** Microsoft SQL Server / LocalDB
- **Segurança e Autenticação:** ASP.NET Core Cookie Authentication com hashing SHA-256 para senhas
- **Validação de Entrada:** Data Annotations no servidor e exibição amigável via TagHelpers

---

## Instruções de Configuração e Execução

### 1. Pré-requisitos
- [.NET 8.0 SDK](https://dotnet.microsoft.com/download)
- SQL Server ou LocalDB
- Ferramenta de linha de comando `dotnet-ef`:
  ```bash
  dotnet tool install --global dotnet-ef
  ```

### 2. Configurar a String de Conexão
No arquivo `appsettings.json`, a connection string padrão está configurada para o SQL Server LocalDB:
```json
"ConnectionStrings": {
  "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=SistemaConsultasUVV_DB;Trusted_Connection=True;MultipleActiveResultSets=true"
}
```
*Caso utilize outra instância de SQL Server (como SQL Server Express ou Docker), altere o valor acima.*

### 3. Criar o Banco de Dados via Migrations
Abra o terminal na pasta do projeto e execute:
```bash
# 0. Restaurar
dotnet restore

# 1. Gerar os arquivos de migração
dotnet ef migrations add InitialCreate

# 2. Aplicar as tabelas e relacionamentos ao banco de dados
dotnet ef database update
```

### 4. Executar a Aplicação
```bash
dotnet run
```
Acesse a aplicação no navegador em: `https://localhost:5001` ou `http://localhost:5000` (conforme exibido no terminal).