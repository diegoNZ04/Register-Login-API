## Controller Scaffolding

dotnet aspnet-codegenerator controller -name UserController -async -api -m UserModel -dc UserContext -outDir Controllers

O código gerado:

- Marca a classe com o atributo [ApiController]. Esse atributo indica se o controlador responde às solicitações da API Web. Para obter informações sobre comportamentos específicos habilitados pelo atributo, confira Criar APIs Web com o ASP.NET Core.
- Usa a DI para injetar o contexto de banco de dados (UseContext) no controlador. O contexto de banco de dados é usado em cada um dos métodos CRUD no controlador.

Os modelos do ASP.NET Core para:

- Os controladores com exibições incluem [action] no modelo de rota.
- Os controladores de API não incluem [action] no modelo de rota.

## Atualizando Método PostUser

O método CreatedAtAction:

- Retorna um código de status HTTP 201 em caso de êxito. HTTP 201 é a resposta padrão para um método HTTP POST que cria um recurso no servidor.
- Adiciona um cabeçalho de Local à resposta. O cabeçalho Location especifica o URI do item de tarefas pendentes recém-criado.
- Faz referência à ação GetUser para criar o URI de Location do cabeçalho. A palavra-chave nameof do C# é usada para evitar o hard-coding do nome da ação, na chamada CreatedAtAction.
