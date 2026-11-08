# MedicamentosConsole — Controle de Medicamentos

Aplicação de console em C# (.NET 8) que controla os medicamentos de uma farmácia por lotes, cada um com quantidade e data de vencimento. As vendas abatem a quantidade disponível sempre a partir do lote mais antigo, usando a estrutura de fila (`Queue<Lote>`).


## Estrutura

```
MedicamentosConsole/
├── MedicamentosConsole.csproj
├── Program.cs                # menu e interação com o usuário
├── Models/
│   ├── Lote.cs               # id, quantidade e vencimento
│   └── Medicamento.cs        # dados do medicamento e fila de lotes
└── Services/
    └── Medicamentos.cs       # lista de medicamentos (adicionar, deletar, pesquisar)
```

## Menu

| Opção | Ação |
|-------|------|
| 0 | Finalizar processo |
| 1 | Cadastrar medicamento |
| 2 | Consultar medicamento (sintético: id, nome, laboratório e quantidade disponível) |
| 3 | Consultar medicamento (analítico: dados sintéticos e lotes com id, quantidade e vencimento) |
| 4 | Comprar medicamento (cadastrar lote) |
| 5 | Vender medicamento (abater do lote mais antigo) |
| 6 | Listar medicamentos (dados sintéticos) |

## Regras de negócio

- Cada compra entra no fim da fila de lotes do medicamento.
- A venda só acontece se a quantidade pedida for positiva e não passar do saldo total. Caso contrário, nada é abatido e o programa informa o saldo disponível.
- Uma venda pode consumir vários lotes. O lote que chega a zero sai da fila, e o lote consumido parcialmente permanece na frente da fila com o saldo restante.
- `Deletar` remove o medicamento da lista somente quando a quantidade disponível é zero.
- `Pesquisar` procura pelo id e, se não encontrar, devolve um objeto vazio (`Medicamento` com id 0). Por isso os ids informados no cadastro devem ser maiores que zero.
- Dois medicamentos são considerados iguais quando têm o mesmo id (`Equals` e `GetHashCode`).

## Decisões de projeto

- Os ids de medicamento e de lote são informados pelo usuário. O id do medicamento não pode se repetir, e o id do lote não pode se repetir dentro do mesmo medicamento.
- A fila segue a ordem de cadastro dos lotes, que é o critério de "mais antigo" do enunciado. O programa não reordena por data de vencimento.
- O vencimento é lido e exibido no formato `dd/MM/aaaa`, então `Lote.ToString()` mostra a data nesse formato em vez do padrão do `DateTime`.
- O método `Deletar` existe na classe `Medicamentos` conforme o diagrama, mas o menu do enunciado não tem opção para excluir.
- Os dados ficam apenas em memória, pois o enunciado não pede persistência.
