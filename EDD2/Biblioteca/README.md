# 06 — Projeto Biblioteca

**Semana 8 — 21/09/2026 | Listas | Console Application**

## Contexto

O projeto Biblioteca tem como objetivo controlar o acervo de livros, seus exemplares físicos e os respectivos empréstimos.

## Diagrama de Classes

```
+----------------------------------------------+
| Livro                                        |
+----------------------------------------------+
| - isbn: int                                  |
| - titulo: string                             |
| - autor: string                              |
| - editora: string                            |
| - exemplares: List<Exemplar>                 |
+----------------------------------------------+
| + adicionarExemplar(Exemplar exemplar): void |
| + qtdeExemplares(): int                      |
| + qtdeDisponiveis(): int                     |
| + qtdeEmprestimos(): int                     |
| + percDisponibilidade(): double              |
+----------------------------------------------+

+---------------------------------+
| Exemplar                        |
+---------------------------------+
| - tombo: int                    |
| - emprestimos: List<Emprestimo> |
+---------------------------------+
| + emprestar(): bool             |
| + devolver(): bool              |
| + disponivel(): bool            |
| + qtdeEmprestimos(): int        |
+---------------------------------+

+--------------------------+
| Emprestimo               |
+--------------------------+
| - dtEmprestimo: DateTime |
| - dtDevolucao: DateTime  |
+--------------------------+

+---------------------------------+
| Livros                          |
+---------------------------------+
| - acervo: List<Livro>           |
+---------------------------------+
| + adicionar(Livro livro): void  |
| + pesquisar(Livro livro): Livro |
+---------------------------------+
```

## Opções do Seletor

| Opção | Descrição |
|-------|-----------|
| 1 | Adicionar livro |
| 2 | Pesquisar livro (sintético)* |
| 3 | Pesquisar livro (analítico)** |
| 4 | Adicionar exemplar |
| 5 | Registrar empréstimo |
| 6 | Registrar devolução |
| 0 | Sair |

\* Informa os dados básicos do livro com as quantidades: total de exemplares, de exemplares disponíveis, de empréstimos e o respectivo percentual de disponibilidade do título.

\*\* Informa, além dos dados acima, os detalhes dos seus exemplares e respectivos empréstimos.

## Observações

- O projeto é desenvolvido em C# Console Application.
