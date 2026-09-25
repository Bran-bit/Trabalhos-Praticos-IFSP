# Agenda de Contatos
 
Console Application em C# (.NET) para gerenciamento de contatos pessoais.
 
## 📋 Contexto
 
O projeto **Agenda** tem como objetivo o gerenciamento de contatos pessoais, contemplando dados de identificação, telefones e data de nascimento.
 
## 🗂 Diagrama de Classes
 
```
+--------------------------------------------+
| Data                                       |
+--------------------------------------------+
| - dia: int                                 |
| - mes: int                                 |
| - ano: int                                 |
+--------------------------------------------+
| + setData(int dia, int mes, int ano): void |
| + ToString(): String (override)            |
|    -> retornando "dd/mm/aaaa"              |
+--------------------------------------------+
 
+-------------------+
| Telefone          |
+-------------------+
| - tipo: string    |
| - numero: string  |
| - principal: bool |
+-------------------+
 
+------------------------------------------+
| Contato                                  |
+------------------------------------------+
| - email: string                          |
| - nome: string                           |
| - dtNasc: Data                           |
| - telefones: List<Telefone>              |
+------------------------------------------+
| + getIdade(): int                        |
| + adicionarTelefone(Telefone t): void    |
| + getTelefonePrincipal(): string         |
| + ToString(): String (override)          |
|    -> retornando uma string com todos os |
|       dados do contato (considerando o   |
|       telefone principal)                |
| + Equals(object obj): bool (override)    |
+------------------------------------------+
 
+------------------------------------+
| Contatos                           |
+------------------------------------+
| - agenda: List<Contato> (readOnly) |
+------------------------------------+
| + adicionar(Contato c): bool       |
| + pesquisar(Contato c): Contato    |
| + alterar(Contato c): bool         |
| + remover(Contato c): bool         |
+------------------------------------+
```
 
## 🚀 Menu / Funcionalidades
 
| Opção | Descrição |
|-------|-----------|
| `0` | Sair |
| `1` | Adicionar contato |
| `2` | Pesquisar contato |
| `3` | Alterar contato |
| `4` | Remover contato |
| `5` | Listar contatos |
 
### Adicionar contato (opção 1)
 
Pede nome, e-mail e data de nascimento (dia/mês/ano — validados de verdade, inclusive ano bissexto). Em seguida, entra em um cadastro de telefones em loop: tipo, número e se é o principal, repetindo até o usuário digitar `fim` no campo tipo. É possível cadastrar um contato sem nenhum telefone. Se o e-mail já existir na agenda, o cadastro é recusado.
 
### Pesquisar / Remover contato (opções 2 e 4)
 
Pedem apenas o e-mail do contato — que é o identificador usado por `Equals()` para localizar o registro na agenda.
 
### Alterar contato (opção 3)
 
Localiza o contato pelo e-mail (que **não muda** durante a alteração, pois é a chave de identidade do contato) e permite:
- manter ou trocar o nome (Enter mantém o atual);
- manter ou trocar a data de nascimento;
- manter os telefones já cadastrados e, opcionalmente, adicionar novos.
### Listar contatos (opção 5)
 
Mostra todos os contatos cadastrados (nome, e-mail, data de nascimento, idade calculada e telefone principal) e o total de contatos na agenda.
