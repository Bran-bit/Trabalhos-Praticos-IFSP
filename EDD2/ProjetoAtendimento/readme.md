# Projeto Atendimento

Projeto acadêmico desenvolvido em C# (Windows Forms) para gerenciamento de filas de atendimento e guichês.

## Contexto

Uma central de atendimento gera senhas para os clientes e distribui o atendimento entre os guichês disponíveis. O sistema mantém o controle da fila de espera (senhas geradas) e do histórico de atendimentos realizados em cada guichê.

## Funcionalidades

- Gerar novas senhas para a fila de espera.
- Adicionar novos guichês de atendimento dinamicamente.
- Chamar a próxima senha da fila para um guichê específico.
- Listar todas as senhas geradas que aguardam atendimento.
- Listar o histórico completo de atendimentos realizados por um guichê específico.

## Estrutura do Projeto

O projeto foi dividido em arquivos separados, respeitando a estrutura do diagrama de classes:

- **Senha.cs**: Modelo para os dados da senha (ID, datas e horas de geração e atendimento).
- **Senhas.cs**: Controle da fila global de espera (utilizando a estrutura de dados `Queue<Senha>`).
- **Guiche.cs**: Modelo do guichê, contendo seu ID e sua fila local de histórico de atendimentos.
- **Guiches.cs**: Repositório central (`List<Guiche>`) que armazena os guichês ativos.
- **Form1.cs**: Interface gráfica e regras de integração entre a interface visual e as classes de domínio.
