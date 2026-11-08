# FilasConsole — Controle de Acessos

Aplicação de console em C# (.NET 8) que simula o controle de acessos de uma empresa. Cada ambiente possui um dispositivo que verifica as permissões do usuário, decide se o acesso é autorizado ou negado e registra a ocorrência em um log. Cada ambiente guarda no máximo 100 logs, descartando os mais antigos (estrutura `Queue<Log>`).


## Estrutura

```
FilasConsole/
├── FilasConsole.csproj
├── Program.cs              # menu e interação com o usuário
├── Models/
│   ├── Usuario.cs          # id, nome, permissões (ambientes)
│   ├── Ambiente.cs         # id, nome, fila de logs (máx. 100)
│   └── Log.cs              # data, usuário e tipo de acesso
└── Services/
    └── Cadastro.cs         # listas de usuários/ambientes e persistência
```

## Menu

| Opção | Ação |
|-------|------|
| 0 | Sair |
| 1 | Cadastrar ambiente |
| 2 | Consultar ambiente |
| 3 | Excluir ambiente |
| 4 | Cadastrar usuário |
| 5 | Consultar usuário |
| 6 | Excluir usuário |
| 7 | Conceder permissão de acesso ao usuário |
| 8 | Revogar permissão de acesso ao usuário |
| 9 | Registrar acesso |
| 10 | Consultar logs de acesso (todos, autorizados ou negados) |

## Regras de negócio

- Cada usuário tem no máximo uma permissão por ambiente.
- Um usuário só pode ser excluído se não tiver nenhuma permissão; é preciso revogá-las antes (opção 8).
- Ao excluir um ambiente, as permissões vinculadas a ele são revogadas automaticamente.
- Os ids de usuário e ambiente são gerados automaticamente. O id de um novo usuário nunca reaproveita o de alguém que ainda aparece nos logs.
- Nomes repetidos geram apenas um aviso, não impedem o cadastro.


## Decisões de projeto

- `Log` guarda `UsuarioId` e `UsuarioNome` além da referência `Usuario`. Assim o log continua legível mesmo que o usuário seja excluído e a referência volte `null` após recarregar os dados.
- Os nomes seguem a convenção do C# (PascalCase) em vez do camelCase do diagrama de classes.
