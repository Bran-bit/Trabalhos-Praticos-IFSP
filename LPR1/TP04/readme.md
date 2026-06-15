# Trabalho Prático 04 - LPR1

**Curso:** Análise e Desenvolvimento de Sistemas
**Disciplina:** Linguagem de Programação 1
**Trabalho:** TP04 - Classe Data

**Dupla:**
- Brandon Oliveira Simões
- Eriel de Jesus Souza


## Programa em Funcionamento

Abaixo está o GIF demonstrando a execução do programa:

<video src="./testeData.webm" autoplay loop muted playsinline></video>


## Estrutura da Classe Data

### Atributos
| Modificador | Tipo | Nome |
|-------------|------|------|
| `-` (private) | `int` | `dia` |
| `-` (private) | `int` | `mes` |
| `-` (private) | `int` | `ano` |

### Métodos

| Método | Descrição |
|--------|-----------|
| `+ Data()` | Construtor que permite ao usuário digitar dia, mês e ano. Realiza consistência de valores. |
| `+ Data(int d, int m, int a)` | Construtor que recebe valores e inicializa os atributos da classe. |
| `+ entraDia(int d)` | Recebe um valor e atribui ao atributo `dia`. |
| `+ entraMes(int m)` | Recebe um valor e atribui ao atributo `mes`. |
| `+ entraAno(int a)` | Recebe um valor e atribui ao atributo `ano`. |
| `+ entraDia()` | Permite entrada de dados via teclado para `dia` com consistência. |
| `+ entraMes()` | Permite entrada de dados via teclado para `mes` com consistência. |
| `+ entraAno()` | Permite entrada de dados via teclado para `ano` com consistência. |
| `+ retDia(): int` | Devolve o valor do atributo `dia`. |
| `+ retMes(): int` | Devolve o valor do atributo `mes`. |
| `+ retAno(): int` | Devolve o valor do atributo `ano`. |
| `+ mostral(): String` | Devolve a data no formato `dd/mm/aaaa`. |
| `+ mostra2(): String` | Devolve a data no formato `dd/mesPorExtenso/ano`. |
| `+ bissexto(): boolean` | Informa se o ano é bissexto. |
| `+ diasTranscorridos(): int` | Retorna a quantidade de dias transcorridos no ano até a data. |
| `+ apresentaDataAtual(): void` | Imprime a data atual usando `Date` e `DateFormat.FULL`. |


## Exercício 01

Criar a classe `Data.java` conforme especificação acima, incluindo tratamento de exceções para entrada de dados.

## Exercício 02

Desenvolver um programa `testarData.java` capaz de testar a classe e todos os métodos desenvolvidos.
