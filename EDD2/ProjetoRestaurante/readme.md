# Cozinha Industrial — Sistema de Pedidos

Console Application em C# (.NET) para controle de pedidos de uma cozinha industrial.

## 📋 Contexto

Uma cozinha industrial atende diariamente até **50 pedidos** feitos por clientes, numerados sequencialmente. Cada pedido pode ter, no máximo, **10 itens**, cada um com um preço no cardápio. O sistema calcula o valor total de cada pedido e permite consultar o histórico do dia.

## 🗂 Diagrama de Classes

```
+---------------------+
| Item                |
+---------------------+
| - id: int           |
| - descricao: string |
| - preco: double     |
+---------------------+

+----------------------------------+
| Pedido                           |
+----------------------------------+
| - id: int                        |
| - cliente: string                |
| - itens: Item[10]                |
+----------------------------------+
| + adicionarItem(Item item): bool |
| + removerItem(Item item): bool   |
| + dadosDoPedido(): string        |
| + calcularTotal(): double        |
+----------------------------------+

+---------------------------------------+
| Restaurante                           |
+---------------------------------------+
| - proxPedido: int                     |
| - pedidos: Pedido[50]                 |
+---------------------------------------+
| + novoPedido(Pedido pedido): bool     |
| + buscarPedido(Pedido pedido): Pedido |
| + cancelarPedido(Pedido pedido): bool |
+---------------------------------------+
```

> As assinaturas acima (do enunciado original) foram mantidas integralmente. Por baixo dos panos, `Restaurante` também oferece sobrecargas `buscarPedido(int id)` e `cancelarPedido(int id)`, usadas internamente para simplificar a busca pelo número do pedido digitado no menu — sem quebrar o contrato do diagrama.

## 🚀 Menu / Funcionalidades

| Opção | Descrição |
|-------|-----------|
| `0` | Sair |
| `1` | Criar novo pedido |
| `2` | Adicionar item ao pedido |
| `3` | Remover item do pedido |
| `4` | Consultar pedido (id, cliente, itens e valor total) |
| `5` | Cancelar pedido |
| `6` | Listar todos os pedidos (id, valores totais e soma geral do dia) |

### Fluxo de criação de pedido (opção 1)

Ao escolher **"1. Criar novo pedido"**, o sistema:

1. Pede o nome do cliente e gera o número sequencial do pedido.
2. Solicita os itens **um a um** (descrição + preço), até o limite de 10.
3. Digitando `fim` na descrição, a coleta de itens é encerrada.
4. Se **nenhum item** for adicionado, o pedido é **cancelado automaticamente** — evitando pedidos "fantasmas" sem nenhum item registrado.
5. Se o limite de 10 itens for atingido, o pedido é finalizado automaticamente, sem precisar digitar `fim`.

## 🛠 Requisitos

- [.NET SDK 8.0](https://dotnet.microsoft.com/download) ou superior instalado.
- Verifique com:
  ```bash
  dotnet --version
  ```

## ▶️ Como executar

1. Coloque `Program.cs`, `CozinhaIndustrial.csproj` e este `README.md` na mesma pasta.
2. Abra um terminal nessa pasta.
3. Execute:
   ```bash
   dotnet run
   ```
4. O menu será exibido no console — digite o número da opção desejada e pressione Enter.

## 🧱 Estrutura do projeto

```
CozinhaIndustrial/
├── Program.cs               # Classes Item, Pedido, Restaurante + menu (Main)
├── CozinhaIndustrial.csproj # Arquivo de projeto (.NET 8, console)
└── README.md                # Este arquivo
```

## 📐 Decisões de implementação

- **Encapsulamento**: `Pedido.Itens` e `Restaurante.Pedidos` retornam uma **cópia somente-leitura** (`IReadOnlyList<T>`) em vez do array interno, evitando que código externo altere a lista diretamente.
- **Numeração sequencial sem reaproveitamento**: cancelar um pedido não devolve seu número — `proxPedido` sempre avança, então os IDs nunca se repetem no mesmo dia de execução.
- **IDs de item globais**: o contador de `Id` de `Item` é compartilhado entre todos os pedidos (não reinicia a cada pedido novo), garantindo que cada item tenha um identificador único no sistema.
- **Validações defensivas**: métodos como `adicionarItem`, `novoPedido`, `buscarPedido` e `cancelarPedido` verificam entradas nulas e limites (10 itens / 50 pedidos) antes de agir.

