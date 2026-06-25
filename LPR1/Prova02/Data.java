package consultaagendada;

import java.util.Date;
import java.text.DateFormat;
import java.util.Scanner;

public class Data {

    private int dia;
    private int mes;
    private int ano;

    private static final Scanner teclado = new Scanner(System.in);

    public Data() {
        System.out.println("--- Inicializando data via construtor padrão ---");
        this.dia = 1;
        this.mes = 1;
        this.ano = 1;
        setAno();
        setMes();
        setDia();
    }

    public Data(int d, int m, int a) {
        if (dataValida(d, m, a)) {
            this.dia = d;
            this.mes = m;
            this.ano = a;
        } else {
            System.out.println("Data inválida. Atribuindo 01/01/2000.");
            this.dia = 1;
            this.mes = 1;
            this.ano = 2000;
        }
    }

    public void setAno(int a) {
        if (dataValida(this.dia, this.mes, a)) {
            this.ano = a;
        } else {
            System.out.println("Ano inválido. Nenhuma alteração realizada.");
        }
    }

    public void setMes(int m) {
        if (dataValida(this.dia, m, this.ano)) {
            this.mes = m;
        } else {
            System.out.println("Mês inválido. Nenhuma alteração realizada.");
        }
    }

    public void setDia(int d) {
        if (dataValida(d, this.mes, this.ano)) {
            this.dia = d;
        } else {
            System.out.println("Dia inválido. Nenhuma alteração realizada.");
        }
    }

    public void setDia() {
        while (true) {
            System.out.print("Digite o dia: ");
            try {
                int d = Integer.parseInt(teclado.nextLine().trim());
                if (dataValida(d, this.mes, this.ano)) {
                    this.dia = d;
                    break;
                }
                System.out.println("Dia inválido para " + this.mes + "/" + this.ano + ". Tente novamente.");
            } catch (NumberFormatException e) {
                System.out.println("Entrada inválida! Digite um número inteiro.");
            }
        }
    }

    public void setMes() {
        while (true) {
            System.out.print("Digite o mês (1-12): ");
            try {
                int m = Integer.parseInt(teclado.nextLine().trim());
                if (m < 1 || m > 12) {
                    System.out.println("Mês inválido (deve ser 1-12). Tente novamente.");
                    continue;
                }
                if (dataValida(this.dia, m, this.ano)) {
                    this.mes = m;
                } else {
                    System.out.printf("Aviso: o dia %02d é incompatível com o mês %02d/%04d. Dia redefinido para 01.%n", this.dia, m, this.ano);
                    this.dia = 1;
                    this.mes = m;
                }
                break;
            } catch (NumberFormatException e) {
                System.out.println("Entrada inválida! Digite um número inteiro.");
            }
        }
    }

    public void setAno() {
        while (true) {
            System.out.print("Digite o ano: ");
            try {
                int a = Integer.parseInt(teclado.nextLine().trim());
                if (a < 1) {
                    System.out.println("Ano inválido (deve ser >= 1). Tente novamente.");
                    continue;
                }
                if (dataValida(this.dia, this.mes, a)) {
                    this.ano = a;
                } else {
                    System.out.printf("Aviso: o dia %02d/%02d é incompatível com o ano %d. Dia e mês foram redefinidos para 01/01.%n", this.dia, this.mes, a);
                    this.dia = 1;
                    this.mes = 1;
                    this.ano = a;
                }
                break;
            } catch (NumberFormatException e) {
                System.out.println("Entrada inválida! Digite um número inteiro.");
            }
        }
    }

    public int getDia() { return this.dia; }
    public int getMes() { return this.mes; }
    public int getAno() { return this.ano; }

    public String mostra1() {
        return String.format("%02d/%02d/%02d", dia, mes, (ano % 100));
    }

    public String mostra2() {
        final String[] nomesMes = {
                "Janeiro", "Fevereiro", "Março", "Abril", "Maio", "Junho",
                "Julho",   "Agosto",   "Setembro", "Outubro", "Novembro", "Dezembro"
        };
        return String.format("%02d de %s de %04d", dia, nomesMes[mes - 1], ano);
    }

    public boolean bissexto() {
        return (ano % 4 == 0 && ano % 100 != 0) || (ano % 400 == 0);
    }

    public int diasTranscorridos() {
        int[] diasPorMes = {31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31};
        if (bissexto()) diasPorMes[1] = 29;

        int total = 0;
        for (int i = 0; i < mes - 1; i++) {
            total += diasPorMes[i];
        }
        return total + dia;
    }

    public void apresentaDataAtual() {
        Date hoje = new Date();
        DateFormat formatador = DateFormat.getDateInstance(DateFormat.FULL);
        System.out.println("Data atual do sistema: " + formatador.format(hoje));
    }

    private static boolean dataValida(int d, int m, int a) {
        if (a < 1) return false;
        if (m < 1 || m > 12) return false;

        int[] diasPorMes = {31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31};
        if (m == 2 && ((a % 4 == 0 && a % 100 != 0) || (a % 400 == 0))) {
            diasPorMes[1] = 29;
        }
        return d >= 1 && d <= diasPorMes[m - 1];
    }
}
