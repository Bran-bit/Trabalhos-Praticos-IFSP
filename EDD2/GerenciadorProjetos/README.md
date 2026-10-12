# Gerenciador de Projetos

Console Application em C# (.NET) para gerenciamento de projetos e de suas tarefas.

## 📋 Enunciado

Uma empresa gerencia vários projetos. Cada projeto possui uma lista de tarefas, com prioridade e status. O sistema permite cadastrar projetos, incluir tarefas, concluir/cancelar/reabrir tarefas, além de listar e filtrar por prioridade/status.

## 🗂 Diagrama de Classes

```
+---------------------------------------------------+
| Tarefa                                            |
+---------------------------------------------------+
| - id: int                                         |
| - titulo: string                                  |
| - descricao: string                               |
| - prioridade: int (1=alta, 2=média, 3=baixa)      |
| - status: string ("Aberta","Fechada","Cancelada") |
| - dataCriacao: DateTime                           |
| - dataConclusao: DateTime                         |
+---------------------------------------------------+
| + concluir(): void                                |
| + cancelar(): void                                |
| + reabrir(): void                                 |
+---------------------------------------------------+

+---------------------------------------------+
| Projeto                                     |
+---------------------------------------------+
| - id: int                                   |
| - nome: string                              |
| - tarefas: List<Tarefa>                     |
+---------------------------------------------+
| + adicionarTarefa(Tarefa t): void           |
| + removerTarefa(Tarefa t): bool             |
| + buscarTarefa(Tarefa t): Tarefa            |
| + tarefasPorStatus(string s): List<Tarefa>  |
| + tarefasPorPrioridade(int p): List<Tarefa> |
| + totalAbertas(): int                       |
| + totalFechadas(): int                      |
+---------------------------------------------+

+------------------------------+
| Projetos                     |
+------------------------------+
| - itens: List<Projeto>       |
+------------------------------+
| + adicionar(Projeto p): bool |
| + remover(Projeto p): bool   |
| + buscar(Projeto p): Projeto |
| + listar(): List<Projeto>    |
+------------------------------+
```

## 🚀 Menu / Funcionalidades

| Opção | Descrição |
|-------|-----------|
| `0` | Sair |
| `1` | Adicionar projeto |
| `2` | Pesquisar projeto (mostra as tarefas agrupadas por status e os totais) |
| `3` | Remover projeto (apenas se não tiver tarefas) |
| `4` | Adicionar tarefa em projeto |
| `5` | Concluir tarefa |
| `6` | Cancelar tarefa |
| `7` | Reabrir tarefa |
| `8` | Listar tarefas de um projeto |
| `9` | Filtrar tarefas por status ou prioridade em um projeto |
| `10` | Filtrar tarefas por status ou prioridade em todos os projetos |
| `11` | Resumo geral: quantidade de projetos, tarefas abertas/fechadas/canceladas e % de concluídas em relação às abertas |

Sempre que uma opção precisa de um projeto (ou de uma tarefa), o programa lista os existentes com seus ids para facilitar a escolha.

### Regras de status das tarefas

| Ação | Permitida quando a tarefa está | Resultado |
|------|-------------------------------|-----------|
| `concluir()` | Aberta | Fechada (registra `dataConclusao`) |
| `cancelar()` | Aberta | Cancelada |
| `reabrir()` | Fechada ou Cancelada | Aberta (limpa `dataConclusao`) |

Qualquer outra combinação é recusada com uma mensagem (por exemplo, "Não é possível concluir uma tarefa fechada."). Para cancelar uma tarefa já fechada, é preciso reabri-la antes.

