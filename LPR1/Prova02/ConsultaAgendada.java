package consultaagendada;

import java.util.Scanner;
/*
    Dupla: Brandon Oliveira Simões e Eriel de Jesus Souza
    Enunciado:

    Crie a classe ConsultaAgendada conforme especificado abaixo (2,0):
    ConsultaAgendada
    - data: Data
    - hora: Hora
    - nomePaciente: String
    - quantidade: int (static)
    - nomeMedico: String
    + ConsultaAgendada ()
    + ConsultaAgendada (int h, int mi, int s, int d, int m, int a, String p,
    String m)
    + ConsultaAgendada (Data d, Hora h, String p, String m)
    + setData(int a, int b, int c)
    + setData()
    + setHora(int a, int b, int c)
    + setHora()
    + setNomePaciente(String p)
    + setNomePaciente()
    + setNomeMedico(String m)
    + setNomeMedico()
    + getAmostra(): int
    + getData(): String
    + getHora(): String
    + getNomePaciente (): String
    + getNomeMedico(): String
    • O construtor ConsultaAgendada deve nos permitir a digitação dos valores de data,
    hora, nome do paciente e do médico;
    • Os outros dois construtores devem receber os valores de data, hora, nome do
    paciente e do médico sob a forma de parâmetros;
    • Qualquer construtor chamado deve acrescer 1 no atributo quantidade.
    • Os métodos setData(), setHora(), setNomePaciente(), setNomeMedico() devem nos
    permitir alterar os valores das respectivas propriedades através da digitação de
    novos valores;
    • Os demais métodos “set” devem alterar os valores das propriedades a partir dos
    parâmetros recebidos;
    • O método getData() deve nos devolver a data no formato: dd/mm/aa;
    • O método getHora() deve nos devolver a hora no formato: hh:mm:ss.

*/

public class ConsultaAgendada {
    private Data data;
    private Hora hora;
    private String nomePaciente;
    private static int quantidade = 0;
    private String nomeMedico;

    private static final Scanner teclado = new Scanner(System.in);

    public ConsultaAgendada() {
        System.out.println("--- Agendando Nova Consulta (Digitação) ---");

        this.data = new Data();
        this.hora = new Hora();

        System.out.print("Digite o nome do paciente: ");
        this.nomePaciente = teclado.nextLine();

        System.out.print("Digite o nome do médico: ");
        this.nomeMedico = teclado.nextLine();

        quantidade++;
    }

    public ConsultaAgendada(int h, int mi, int s, int d, int m, int a, String p, String mDoc) {
        this.hora = new Hora(h, mi, s);
        this.data = new Data(d, m, a);
        this.nomePaciente = p;
        this.nomeMedico = mDoc;

        quantidade++;
    }

    public ConsultaAgendada(Data d, Hora h, String p, String mDoc) {
        this.data = d;
        this.hora = h;
        this.nomePaciente = p;
        this.nomeMedico = mDoc;

        quantidade++;
    }

    public void setData() {
        System.out.println("--- Alterando Data da Consulta ---");
        this.data = new Data(); // O construtor padrão de Data
    }

    public void setHora() {
        System.out.println("--- Alterando Hora da Consulta ---");
        this.hora = new Hora(); // O construtor padrão de Hora
    }

    public void setNomePaciente() {
        System.out.print("Digite o novo nome do paciente: ");
        this.nomePaciente = teclado.nextLine();
    }

    public void setNomeMedico() {
        System.out.print("Digite o novo nome do médico: ");
        this.nomeMedico = teclado.nextLine();
    }

    public void setData(int d, int m, int a) {
        this.data = new Data(d, m, a);
    }

    public void setHora(int h, int mi, int s) {
        this.hora = new Hora(h, mi, s);
    }

    public void setNomePaciente(String p) {
        this.nomePaciente = p;
    }

    public void setNomeMedico(String mDoc) {
        this.nomeMedico = mDoc;
    }

    public static int getAmostra() {
        return quantidade;
    }

    public String getData() {
        return this.data.mostra1();
    }

    public String getHora() {
        return this.hora.getHora1();
    }

    public String getNomePaciente() {
        return this.nomePaciente;
    }

    public String getNomeMedico() {
        return this.nomeMedico;
    }
}
