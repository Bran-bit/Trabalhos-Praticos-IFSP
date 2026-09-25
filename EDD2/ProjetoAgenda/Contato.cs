using System;
using System.Collections.Generic;
using System.Text;

namespace Agenda
{
    public class Contato
    {
        private string email;
        private string nome;
        private Data dtNasc;
        private List<Telefone> telefones = new List<Telefone>();

        public Contato(string email, string nome, Data dtNasc)
        {
            this.email = email;
            this.nome = nome;
            this.dtNasc = dtNasc;
        }

        public Contato(string email)
        {
            this.email = email;
        }

        public string Email { get => email; set => email = value; }
        public string Nome { get => nome; set => nome = value; }
        public Data DtNasc { get => dtNasc; set => dtNasc = value; }

        public IReadOnlyList<Telefone> Telefones => telefones;

        public int getIdade()
        {
            DateTime nascimento = dtNasc.ParaDateTime();
            DateTime hoje = DateTime.Today;

            int idade = hoje.Year - nascimento.Year;
            if (nascimento.Date > hoje.AddYears(-idade))
                idade--;

            return idade;
        }

        public void adicionarTelefone(Telefone t)
        {
            if (t == null) return;

            if (t.Principal)
            {
                foreach (Telefone existente in telefones)
                    existente.Principal = false;
            }

            telefones.Add(t);
        }

        public string getTelefonePrincipal()
        {
            foreach (Telefone t in telefones)
            {
                if (t.Principal)
                    return t.Numero;
            }

            if (telefones.Count > 0)
                return telefones[0].Numero;

            return "Sem telefone cadastrado";
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Nome: {nome}");
            sb.AppendLine($"E-mail: {email}");
            sb.AppendLine($"Data de Nascimento: {dtNasc} (Idade: {getIdade()} anos)");
            sb.Append($"Telefone principal: {getTelefonePrincipal()}");
            return sb.ToString();
        }

        public override bool Equals(object obj)
        {
            Contato outro = obj as Contato;
            if (outro == null)
                return false;

            return string.Equals(email, outro.email, StringComparison.OrdinalIgnoreCase);
        }

        public override int GetHashCode()
        {
            return email == null ? 0 : email.ToLowerInvariant().GetHashCode();
        }
    }
}
