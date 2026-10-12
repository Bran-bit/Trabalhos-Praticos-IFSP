using System;

namespace Agenda
{
    public class Data
    {
        private int dia;
        private int mes;
        private int ano;

        public Data(int dia, int mes, int ano)
        {
            setData(dia, mes, ano);
        }

        public int Dia => dia;
        public int Mes => mes;
        public int Ano => ano;

        public void setData(int dia, int mes, int ano)
        {
            if (mes < 1 || mes > 12)
                throw new ArgumentException("O mês deve estar entre 1 e 12.");

            if (ano < 1 || ano > 9999)
                throw new ArgumentException("O ano deve estar entre 1 e 9999.");

            int diasNoMes = DateTime.DaysInMonth(ano, mes);
            if (dia < 1 || dia > diasNoMes)
                throw new ArgumentException($"O dia deve estar entre 1 e {diasNoMes} para o mês/ano informados.");

            this.dia = dia;
            this.mes = mes;
            this.ano = ano;
        }

        public DateTime ParaDateTime()
        {
            return new DateTime(ano, mes, dia);
        }

        public override string ToString()
        {
            return $"{dia:D2}/{mes:D2}/{ano:D4}";
        }
    }
}
