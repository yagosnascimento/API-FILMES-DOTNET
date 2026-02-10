
# FilmesAPI

Este é um projeto de estudo de uma API REST desenvolvida em .NET 8 para gerenciamento de filmes. O sistema permite criar, ler, atualizar e deletar registros de filmes, utilizando o Entity Framework Core para persistência de dados em um banco MySQL.

## Tecnologias

* C# e .NET 8
* Entity Framework Core 9
* Pomelo.EntityFrameworkCore.MySql
* Swagger (para documentação e testes da API)

## Como configurar e executar

1. **Banco de Dados**: Certifique-se de ter o MySQL instalado e rodando.
2. **Configuração**: Abra o arquivo `appsettings.json` e ajuste a `ConnectionStrings` com seu usuário e senha do MySQL.
3. **Migrações**: No terminal, dentro da pasta do projeto, execute o comando abaixo para criar o banco de dados e as tabelas:
```bash
dotnet ef database update

```


4. **Execução**: Rode o projeto com o comando:
```bash
dotnet run

```



---

## Guia de Estudo: Como funciona a conexão com o Banco de Dados

Esta seção serve como uma "cola" para entender como o .NET conecta a API ao banco de dados MySQL. O processo acontece em três partes principais interligadas.

### 1. O Endereço (appsettings.json)

No arquivo `appsettings.json`, definimos onde o banco está e como entrar nele.

```json
"ConnectionStrings": {
  "FilmeConnection": "server=localhost;database=filme;user=root;password=root"
}

```

* **server=localhost**: Indica que o banco está na sua própria máquina.
* **database=filme**: O nome do banco de dados que será criado.
* **user** e **password**: Suas credenciais de acesso ao MySQL.

### 2. A Configuração (Program.cs)

No arquivo `Program.cs`, nós pegamos o endereço definido acima e ensinamos a API a usar o Entity Framework.

```csharp
builder.Services.AddDbContext<FilmeContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("FilmeConnection"),
        ServerVersion.AutoDetect(
            builder.Configuration.GetConnectionString("FilmeConnection")
        )
    )
);

```

**Explicação linha a linha:**

* `builder.Services.AddDbContext<FilmeContext>`: Adiciona o nosso contexto de banco de dados (`FilmeContext`) ao contêiner de injeção de dependência. Isso permite que o `FilmeContext` seja usado nos Controllers.
* `options.UseMySql(...)`: Configura o Entity Framework para "falar" a língua do MySQL. Se fosse SQL Server, usaríamos `UseSqlServer`.
* `builder.Configuration.GetConnectionString("FilmeConnection")`: Vai até o `appsettings.json`, procura pela chave chamada "FilmeConnection" e pega aquela string com login e senha.
* `ServerVersion.AutoDetect(...)`: O sistema conecta rapidinho no banco apenas para descobrir qual a versão do MySQL (ex: 8.0, 5.7) e se auto-configurar corretamente.

### 3. O Contexto (Data/FilmeContext.cs)

O arquivo `FilmeContext.cs` é a classe que representa o banco de dados dentro do código C#.

```csharp
public class FilmeContext : DbContext
{
    public FilmeContext(DbContextOptions<FilmeContext> options) : base(options) 
    {
    }

    public DbSet<Filme> Filmes { get; set; }
}

```

* **Herança (: DbContext)**: Ao herdar de `DbContext`, nossa classe ganha todos os poderes do Entity Framework para salvar, buscar e deletar dados.
* **Construtor**: O construtor recebe as `options` (aquelas configurações de conexão que fizemos no `Program.cs`) e passa para a classe base (`base(options)`), garantindo que a conexão seja estabelecida.
* **DbSet<Filme> Filmes**: Isso diz ao banco: "Crie uma tabela chamada `Filmes` baseada na classe `Filme`". Cada propriedade da classe `Filme` (Id, Titulo, Genero) virará uma coluna nessa tabela.
