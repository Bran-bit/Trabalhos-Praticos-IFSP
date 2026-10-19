using System;

namespace ProjetoAtendimento
{
    public class Senha
    {
        private int id;
        private DateTime dataGerac;
        private DateTime horaGerac;
        private DateTime dataAtend;
        private DateTime horaAtend;

        public int Id { get => id; }
        public DateTime DataAtend { get => dataAtend; set => dataAtend = value; }
        public DateTime HoraAtend { get => horaAtend; set => horaAtend = value; }

        public Senha(int id)
        {
            this.id = id;
            this.dataGerac = DateTime.Now.Date;
            this.horaGerac = DateTime.Now;
        }

        public string dadosParciais()
        {
            return $"{id} - {dataGerac.ToShortDateString()} - {horaGerac.ToShortTimeString()}";
        }

        public string dadosCompletos()
        {
            return $"{id} - {dataGerac.ToShortDateString()} - {horaGerac.ToShortTimeString()} - {dataAtend.ToShortDateString()} - {horaAtend.ToShortTimeString()}";
        }
    }
}