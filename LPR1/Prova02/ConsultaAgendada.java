package consultaagendada;

import java.util.Scanner;

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
