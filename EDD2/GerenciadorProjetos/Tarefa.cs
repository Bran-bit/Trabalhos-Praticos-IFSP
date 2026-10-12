using System;
using System.Text;

namespace GerenciadorProjetos
{
    public class Tarefa
    {
        public const string StatusAberta = "Aberta";
        public const string StatusFechada = "Fechada";
        public const string StatusCancelada = "Cancelada";

        private static int proximoId = 1;

        private int id;
        private string titulo;
        private string descricao;
        private int prioridade;
        private string status;
        private DateTime dataCriacao;
        private DateTime dataConclusao;

        public Tarefa(string titulo, string descricao, int prioridade)
        {
            Titulo = titulo;
            Descricao = descricao;
            Prioridade = prioridade;

            this.id = proximoId++;
            this.status = StatusAberta;
            this.dataCriacao = DateTime.Now;
            this.dataConclusao = DateTime.MinValue;
        }

        public Tarefa(int id)
        {
            this.id = id;
        }

        public int Id => id;

        public string Titulo
        {
            get => titulo;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("O título da tarefa é obrigatório.");
                titulo = value.Trim();
            }
        }

        public string Descricao
        {
            get => descricao;
            set => descricao = value == null ? string.Empty : value.Trim();
        }

        public int Prioridade
        {
            get => prioridade;
            set
            {
                if (value < 1 || value > 3)
                    throw new ArgumentException("A prioridade deve ser 1 (alta), 2 (média) ou 3 (baixa).");
                prioridade = value;
            }
        }

        public string Status => status;
        public DateTime DataCriacao => dataCriacao;
        public DateTime DataConclusao => dataConclusao;

        public string PrioridadeDescricao => DescricaoPrioridade(prioridade);

        public static string DescricaoPrioridade(int prioridade)
        {
            switch (prioridade)
            {
                case 1: return "Alta";
                case 2: return "Média";
                case 3: return "Baixa";
                default: return "Indefinida";
            }
        }

        public void concluir()
        {
            if (status != StatusAberta)
                throw new InvalidOperationException($"Não é possível concluir uma tarefa {status.ToLower()}.");

            status = StatusFechada;
            dataConclusao = DateTime.Now;
        }

        public void cancelar()
        {
            if (status != StatusAberta)
                throw new InvalidOperationException($"Não é possível cancelar uma tarefa {status.ToLower()}.");

            status = StatusCancelada;
        }

        public void reabrir()
        {
            if (status == StatusAberta)
                throw new InvalidOperationException("A tarefa já está aberta.");

            status = StatusAberta;
            dataConclusao = DateTime.MinValue;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"[{id}] {titulo} | Prioridade: {PrioridadeDescricao} | {status} | Criada: {dataCriacao:dd/MM/yyyy HH:mm}");

            if (status == StatusFechada)
                sb.Append($" | Concluída: {dataConclusao:dd/MM/yyyy HH:mm}");

            if (!string.IsNullOrEmpty(descricao))
            {
                sb.AppendLine();
                sb.Append($"    Descrição: {descricao}");
            }

            return sb.ToString();
        }

        public override bool Equals(object obj)
        {
            Tarefa outra = obj as Tarefa;
            if (outra == null)
                return false;

            return id == outra.id;
        }

        public override int GetHashCode()
        {
            return id.GetHashCode();
        }
    }
}
