package tp04;

import java.util.InputMismatchException;
import java.util.Scanner;
import java.util.Date;
import java.text.DateFormat;

/**
 * Classe que representa e manipula uma data (dia, mês e ano).
 * Inclui validação completa de entradas, suporte a ano bissexto
 * e cálculo de dias transcorridos no ano.
 */
public class Data {

    private int dia;
    private int mes;
    private int ano;

    // Scanner compartilhado por todos os métodos de entrada, por isso static
    private static final Scanner teclado = new Scanner(System.in);

    /**
     * Construtor padrão: solicita ao usuário os valores de ano, mês e dia
     * A ordem importa: mês depende do ano (caso bissexto), dia depende do mês (com 30, 31, 28 ou 29 dias).
     */
    public Data() {
        System.out.println("--- Inicializando data via construtor padrão ---");
        this.dia = 1;   // sentinelas neutros usados somente durante a coleta
        this.mes = 1;
        this.ano = 1;
        entraAno();  // ano primeiro: independente
        entraMes();  // mês segundo: depende somente do ano (para Fevereiro bissexto)
        entraDia();  // dia por último: depende de mês e ano
    }

    /**
     * Construtor parametrizado: atribui os valores recebidos se a data for
     * válida; caso contrário, inicializa com 01/01/2000 e exibe aviso.
     *
     * @param d dia (1-31)
     * @param m mês (1-12)
     * @param a ano (>= 1)
     */
    public Data(int d, int m, int a) {
        if (dataValida(d, m, a)) {
            this.dia = d;
            this.mes = m;
            this.ano = a;
        } else {
            System.out.println("Data inválida (" + d + "/" + m + "/" + a
                    + ") - Atribuindo 01/01/2000.");
            this.dia = 1;
            this.mes = 1;
            this.ano = 2000;
        }
    }


    public void entraAno(int a) {
        if (dataValida(this.dia, this.mes, a)) {
            this.ano = a;
        } else {
            System.out.println("Ano " + a + " inválido para a data "
                    + this.dia + "/" + this.mes + ". Nenhuma alteração realizada.");
        }
    }

    public void entraMes(int m) {
        if (dataValida(this.dia, m, this.ano)) {
            this.mes = m;
        } else {
            System.out.printf("Mês %d inválido para o dia %d/%04d. Nenhuma alteração realizada.%n",
                    m, this.dia, this.ano);
        }
    }

    public void entraDia(int d) {
        if (dataValida(d, this.mes, this.ano)) {
            this.dia = d;
        } else {
            System.out.println("Dia " + d + " inválido para "
                    + this.mes + "/" + this.ano + ". Nenhuma alteração realizada.");
        }
    }
    /**
     * Solicita ao usuário um dia válido para o mês/ano atuais,
     * repetindo até que a entrada seja correta.
     */
    public void entraDia() {
        while (true) {
            System.out.print("Digite o dia: ");
            try {
                int d = teclado.nextInt();
                if (dataValida(d, this.mes, this.ano)) {
                    this.dia = d;
                    break;
                }
                System.out.println("Dia inválido para " + this.mes
                        + "/" + this.ano + ". Tente novamente.");
            } catch (java.util.InputMismatchException e) {
                System.out.println("Entrada inválida! Digite um número inteiro.");
                teclado.next(); // descarta token inválido do buffer
            }
        }
    }

    /**
     * Solicita ao usuário um mês válido (1-12),
     * repetindo até que a entrada seja correta.
     */
    public void entraMes() {
        while (true) {
            System.out.print("Digite o mês (1-12): ");
            try {
                int m = teclado.nextInt();
                if (m < 1 || m > 12) {
                    System.out.println("Mês inválido (deve ser 1-12). Tente novamente.");
                    continue;
                }
                if (dataValida(this.dia, m, this.ano)) {
                    this.mes = m;
                } else {
                    System.out.printf(
                            "Aviso: o dia %02d é incompatível com o mês %02d/%04d. " +
                                    "Dia redefinido para 01.%n",
                            this.dia, m, this.ano);
                    this.dia = 1;
                    this.mes = m;
                }
                break;
            } catch (InputMismatchException e) {
                System.out.println("Entrada inválida! Digite um número inteiro.");
                teclado.next();
            }
        }
    }

    public void entraAno() {
        while (true) {
            System.out.print("Digite o ano: ");
            try {
                int a = teclado.nextInt();
                if (a < 1) {
                    System.out.println("Ano inválido (deve ser >= 1). Tente novamente.");
                    continue;
                }
                if (dataValida(this.dia, this.mes, a)) {
                    this.ano = a;
                } else {
                    // Conflito: aceita o ano mas reseta dia e mês para 01/01
                    System.out.printf(
                            "Aviso: o dia %02d/%02d é incompatível com o ano %d. " +
                                    "Dia e mês foram redefinidos para 01/01.%n",
                            this.dia, this.mes, a);
                    this.dia = 1;
                    this.mes = 1;
                    this.ano = a;
                }
                break;
            } catch (InputMismatchException e) {
                System.out.println("Entrada inválida! Digite um número inteiro.");
                teclado.next();
            }
        }
    }

    /** @return dia armazenado */
    public int retDia() { return this.dia; }

    /** @return mês armazenado */
    public int retMes() { return this.mes; }

    /** @return ano armazenado */
    public int retAno() { return this.ano; }


    /**
     * @return data no formato dd/mm/aaaa (ex.: 29/02/2024)
     */
    public String mostra1() {
        return String.format("%02d/%02d/%04d", dia, mes, ano);
    }

    /**
     * @return data no formato "dd de MesPorExtenso de aaaa" (ex.: 29 de Fevereiro de 2024)
     */
    public String mostra2() {
        final String[] nomesMes = {
                "Janeiro", "Fevereiro", "Março", "Abril", "Maio", "Junho",
                "Julho",   "Agosto",   "Setembro", "Outubro", "Novembro", "Dezembro"
        };
        return String.format("%02d de %s de %04d", dia, nomesMes[mes - 1], ano);
    }

    /**
     * Verifica se o ano armazenado é bissexto.
     * Um ano é bissexto se divisível por 4, exceto centenários,
     * salvo os divisíveis por 400.
     *
     * @return true se o ano for bissexto
     */
    public boolean bissexto() {
        return (ano % 4 == 0 && ano % 100 != 0) || (ano % 400 == 0);
    }

    /**
     * Calcula quantos dias já se passaram no ano até a data armazenada,
     * incluindo o próprio dia.
     *
     * @return número de dias transcorridos no ano (1 a 365/366)
     */
    public int diasTranscorridos() {
        int[] diasPorMes = {31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31};
        if (bissexto()) diasPorMes[1] = 29;

        int total = 0;
        for (int i = 0; i < mes - 1; i++) {
            total += diasPorMes[i];
        }
        return total + dia;
    }

    /**
     * Imprime no console a data atual do sistema utilizando
     * {@link Date} e {@link DateFormat#FULL}.
     */
    public void apresentaDataAtual() {
        Date hoje = new Date();
        DateFormat formatador = DateFormat.getDateInstance(DateFormat.FULL);
        System.out.println("Data atual do sistema: " + formatador.format(hoje));
    }

    /**
     * Verifica se a combinação (d, m, a) representa uma data existente no calendário.
     *
     * @param d dia
     * @param m mês
     * @param a ano
     * @return true se a data for válida
     */
    private static boolean dataValida(int d, int m, int a) {
        if (a < 1) return false;
        if (m < 1 || m > 12) return false;

        int[] diasPorMes = {31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31};
        if (m == 2 && ((a % 4 == 0 && a % 100 != 0) || (a % 400 == 0))) {
            diasPorMes[1] = 29; // Fevereiro em ano bissexto
        }
        return d >= 1 && d <= diasPorMes[m - 1];
    }
}
