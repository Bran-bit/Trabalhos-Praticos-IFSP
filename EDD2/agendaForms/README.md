# Agenda de Contatos — Windows Forms

Windows Forms Application em C# (.NET 8) para gerenciamento de contatos pessoais. É a reimplementação, com interface gráfica, do projeto Agenda, mantendo o mesmo diagrama de classes.

## 📋 Contexto

O projeto **Agenda** tem como objetivo o gerenciamento de contatos pessoais, contemplando dados de identificação, telefones e data de nascimento. Nesta etapa, a aplicação utiliza interface gráfica Windows Forms, mantendo a lógica de negócio do diagrama de classes.

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

## 🖥 Adaptação do seletor para Windows Forms

| Opção do seletor | Na interface gráfica |
|---|---|
| 0. Sair | Botão **Sair** (fecha a aplicação) |
| 1. Adicionar contato | Botão **Adicionar** → abre o formulário de contato (nome, e-mail, data de nascimento e lista de telefones) |
| 2. Pesquisar contato | Campo **E-mail do contato** + botão **Pesquisar** → a lista passa a mostrar apenas o contato encontrado |
| 3. Alterar contato | Selecionar uma linha da lista e clicar em **Alterar** (ou dar duplo clique na linha) → abre o formulário de contato preenchido |
| 4. Remover contato | Selecionar uma linha da lista e clicar em **Remover** (com confirmação Sim/Não) |
| 5. Listar contatos | A lista (`DataGridView`) exibe os contatos com nome, e-mail, nascimento, idade e telefone principal; o botão **Listar todos** restaura a lista completa depois de uma pesquisa |

### Formulário de contato

- **Nome** e **e-mail** (obrigatórios; o e-mail precisa conter `@`).
- **Data de nascimento** escolhida em um `DateTimePicker` (não permite datas futuras).
- **Telefones**: escolher o tipo, digitar o número, marcar (ou não) como **Principal** e clicar em **Adicionar telefone**. Os telefones aparecem em uma lista e podem ser removidos com **Remover selecionado**.
- Ao **alterar** um contato, o e-mail fica bloqueado (somente leitura), pois é ele que identifica o contato na agenda.
- **Cancelar** (ou a tecla Esc) descarta as alterações.

## 🛠 Requisitos

- **Windows** (Windows Forms só executa no Windows).
- [.NET SDK 8.0](https://dotnet.microsoft.com/download) ou superior, ou Visual Studio 2022 com a carga de trabalho "Desenvolvimento para desktop com .NET".

## ▶️ Como executar

**Pelo terminal** (na pasta do projeto):

```bash
dotnet run
```

**Pelo Visual Studio**: abra o arquivo `AgendaWinForms.csproj` e pressione `F5`.

## 🧱 Estrutura do projeto

```
AgendaWinForms/
├── Data.cs                    # Classe Data
├── Telefone.cs                # Classe Telefone
├── Contato.cs                 # Classe Contato
├── Contatos.cs                # Classe Contatos (agenda)
├── Program.cs                 # Ponto de entrada da aplicação
├── MainForm.cs                # Formulário principal (lógica)
├── MainForm.Designer.cs       # Formulário principal (controles e layout)
├── ContatoForm.cs             # Formulário de contato (lógica)
├── ContatoForm.Designer.cs    # Formulário de contato (controles e layout)
├── AgendaWinForms.csproj      # Arquivo de projeto (.NET 8, Windows Forms)
└── README.md                  # Este arquivo
```

## 📐 Decisões de implementação

- **Separação entre domínio e interface**: as classes `Data`, `Telefone`, `Contato` e `Contatos` não conhecem a interface gráfica; os formulários apenas as utilizam.
- **Identidade do contato pelo e-mail**: `Contato.Equals()` compara apenas o e-mail (ignorando maiúsculas/minúsculas), e `GetHashCode()` foi implementado em conjunto. `Contatos` usa essa igualdade em `adicionar`, `pesquisar`, `alterar` e `remover`.
- **Construtor auxiliar**: `Contato(string email)` cria um contato apenas com o e-mail, usado para localizar um contato existente (por exemplo, na pesquisa), já que a igualdade depende só do e-mail.
- **`removerTelefone` (acréscimo ao diagrama)**: `Contato` possui o método `removerTelefone(Telefone t)`, usado na tela para corrigir um telefone digitado errado antes de salvar.
- **Edição sem efeito colateral**: o formulário de alteração trabalha em uma cópia do contato; só ao clicar em **Salvar** o contato é substituído na agenda (`Contatos.alterar`).
- **No máximo um telefone principal**: `adicionarTelefone()` desmarca os demais ao inserir um telefone principal. Se nenhum for marcado, `getTelefonePrincipal()` usa o primeiro cadastrado.
- **Validação de datas**: `Data.setData()` valida mês, ano e dia (inclusive anos bissextos), lançando `ArgumentException` em caso de data inválida.
- **`agenda` somente-leitura**: `Contatos.Agenda` expõe a lista como `IReadOnlyList<Contato>`.
