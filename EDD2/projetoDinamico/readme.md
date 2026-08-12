# Projeto Bilheteria — Windows Forms

**Disciplina:** Nivelamento / Lógica de Programação
**Tecnologias:** C#, .NET Framework, Windows Forms
**Interface:** Totalmente criada em código (sem uso do Designer para os componentes principais).

---

## Enunciado da Atividade

Um teatro possui **600 lugares**, organizados em **15 fileiras** com **40 poltronas** cada.

Cada poltrona pode estar em um dos três estados:

* **Vaga**
* **Ocupada (Inteira)**
* **Ocupada (Meia Entrada)**

### Regras de Precificação

| Fileiras | Valor (Inteira) |
| :------: | --------------: |
|   1 a 5  |        R$ 50,00 |
|  6 a 10  |        R$ 30,00 |
|  11 a 15 |        R$ 15,00 |

> **Meia entrada** corresponde a 50% do valor cheio da fileira.

---

## Funcionalidades Exigidas

### 1. Mapa de poltronas

* Exibir uma grade com **600 botões (15 × 40)** criados dinamicamente.
* Cada botão representa visualmente o estado da poltrona (cor e texto).

### 2. Reservar poltrona

* Ao clicar em uma poltrona **vaga**, o sistema pergunta o tipo de reserva (**Inteira** ou **Meia**) e atualiza o estado e a aparência do botão.

### 3. Alertar ocupação

* Ao clicar em uma poltrona **já ocupada**, exibe uma mensagem informando o estado atual, sem alterar a reserva.

### 4. Faturamento

* Um botão **"Faturamento"** (também criado em código) calcula e exibe em uma `Label`:

  * Quantidade de lugares ocupados (inteira e meia).
  * Valor total arrecadado, respeitando os preços das fileiras e o tipo de entrada.

**Formato:**

```text
Qtde de lugares ocupados: 999
Valor da bilheteria: R$ 9999,99
```

---

## Observações Técnicas

* **Toda a interface** (poltronas, botão Faturamento e label de resultado) deve ser criada **dinamicamente em código**, sem utilizar o Designer.
* Os dados devem ser **consistidos**, garantindo que apenas coordenadas válidas de fileira/poltrona sejam manipuladas.
* O estado das poltronas pode ser representado da forma que o aluno preferir (ex.: matriz de `enum`, `int`, etc.).

---

## Estrutura do Projeto

```text
projBilheteria/
├── Form1.cs
│   └── Código principal: interface dinâmica, lógica de reserva e faturamento.
│
├── Form1.Designer.cs
│   └── Código gerado pelo Designer (mantido vazio ou com controles de exemplo removidos).
│
├── TipoReservaDialog.cs
│   └── Janela de diálogo customizada para escolha do tipo de reserva (Inteira/Meia).
│
└── Program.cs
    └── Ponto de entrada da aplicação.
```

### Arquivos para consulta

| Arquivo                | Conteúdo relevante                                                                                                                                                          |
| ---------------------- | --------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| `Form1.cs`             | Criação da grade de poltronas, botão Faturamento, label de resultado, toda a lógica de clique, validação de coordenadas e cálculo do faturamento.                           |
| `TipoReservaDialog.cs` | Implementação de um diálogo personalizado (herda de `Form`) com dois botões ("Inteira" e "Meia Entrada"), substituindo o `MessageBox` tradicional.                          |
| `Form1.Designer.cs`    | Pode ser mantido como gerado pelo Visual Studio, mas todos os seus controles são removidos em tempo de execução (`Controls.Clear()`) para garantir interface 100% dinâmica. |

---

## Como Executar

1. Abra a solução no **Visual Studio** (versão com suporte a .NET Framework).
2. Compile o projeto (`Ctrl+Shift+B`).
3. Execute (`F5`).
4. Interaja com o mapa de poltronas e utilize o botão **"Faturamento"** para visualizar os resultados.

---

## Decisões de Implementação

* Uso de `enum EstadoPoltrona` para maior legibilidade do código.
* Validação de coordenadas em todos os pontos de acesso à matriz de estados.
* Diálogo de reserva customizado (`TipoReservaDialog`) para melhor experiência do usuário.
* Estilização do botão Faturamento (cores, efeito hover, cursor) para uma interface mais moderna.
* Separação das responsabilidades em métodos (`CriarPoltronas`, `ReservarPoltrona`, `CalcularFaturamento`).

---

**Desenvolvido como atividade de nivelamento para a disciplina.**
