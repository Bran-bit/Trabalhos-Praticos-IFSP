package tp04;

/**
 * Programa de testes para a classe Data.
 * Exercita todos os construtores, setters, getters e métodos de consulta.
 */
public class DataTeste {

    public static void main(String[] args) {

        System.out.println("========== TESTE DA CLASSE DATA ==========\n");

        System.out.println("Teste com construtor parametrizado");

        System.out.println("\nData válida: 15/02/2020");
        Data data1 = new Data(15, 2, 2020);
        exibirInfoData(data1, "data1");

        System.out.println("\nData válida – bissexto: 29/02/2024");
        Data data2 = new Data(29, 2, 2024);
        exibirInfoData(data2, "data2");

        System.out.println("\nData inválida: 31/04/2021 (abril tem 30 dias)");
        Data data3 = new Data(31, 4, 2021);
        exibirInfoData(data3, "data3 → deve ser 01/01/2000");

        System.out.println("\nData inválida: 29/02/2023 (2023 não é bissexto)");
        Data data4 = new Data(29, 2, 2023);
        exibirInfoData(data4, "data4 → deve ser 01/01/2000");


        System.out.println("\n--- Getters retDia(), retMes(), retAno() ---");
        System.out.printf("data1 → dia: %d  mês: %d  ano: %d%n",
                data1.retDia(), data1.retMes(), data1.retAno());
        System.out.printf("data2 → dia: %d  mês: %d  ano: %d%n",
                data2.retDia(), data2.retMes(), data2.retAno());


        System.out.println("\n--- Setters parametrizados (ordem segura: ano → mês → dia) ---");

        System.out.println("\nPartindo de data1 = " + data1.mostra1());

        System.out.println("entraAno(2024):");
        data1.entraAno(2024);
        System.out.println("  → " + data1.mostra1());

        System.out.println("entraMes(2)  [mês já é 2, mantém]:");
        data1.entraMes(2);
        System.out.println("  → " + data1.mostra1());

        System.out.println("entraDia(29) [29/02/2024 é válido – bissexto]:");
        data1.entraDia(29);
        System.out.println("  → " + data1.mostra1());

        // Tentativas inválidas
        System.out.println("\nTentativas inválidas de setter:");
        System.out.print("entraMes(2) com dia=29: ");   // mantém pois 29/2/2024 é válido
        data1.entraMes(2);
        System.out.println("  → " + data1.mostra1());

        System.out.print("entraMes(3) com dia=29: ");   // 29/3 é válido
        data1.entraMes(3);
        System.out.println("  → " + data1.mostra1());

        System.out.print("entraMes(2) com dia=29 de volta: "); // requer bissexto
        data1.entraMes(2);
        System.out.println("  → " + data1.mostra1());

        System.out.print("entraAno(2023) [29/02/2023 inválido – não bissexto]: ");
        data1.entraAno(2023);
        System.out.println("  → " + data1.mostra1() + "  (deve permanecer 29/02/2024)");

        System.out.print("entraDia(31) em fevereiro: ");
        data1.entraDia(31);
        System.out.println("  → " + data1.mostra1() + "  (deve permanecer 29/02/2024)");


        System.out.println("\n--- Formatação mostra1() e mostra2() ---");
        System.out.println("data1 mostra1(): " + data1.mostra1());
        System.out.println("data1 mostra2(): " + data1.mostra2());
        System.out.println("data2 mostra1(): " + data2.mostra1());
        System.out.println("data2 mostra2(): " + data2.mostra2());
        System.out.println("data3 mostra1(): " + data3.mostra1());
        System.out.println("data3 mostra2(): " + data3.mostra2());

        System.out.println("\n--- Método bissexto() ---");
        testarBissexto(1900); // divisível por 100 mas não por 400 → não bissexto
        testarBissexto(2000); // divisível por 400 → bissexto
        testarBissexto(2023); // não divisível por 4 → não bissexto
        testarBissexto(2024); // divisível por 4, não por 100 → bissexto


        System.out.println("\n--- Método diasTranscorridos() ---");
        // 01/01 → 1 dia
        System.out.printf("01/01/2024 → %d dia(s) (esperado: 1)%n",
                new Data(1, 1, 2024).diasTranscorridos());
        // 31/12 de ano não-bissexto → 365
        System.out.printf("31/12/2023 → %d dia(s) (esperado: 365)%n",
                new Data(31, 12, 2023).diasTranscorridos());
        // 31/12 de ano bissexto → 366
        System.out.printf("31/12/2024 → %d dia(s) (esperado: 366)%n",
                new Data(31, 12, 2024).diasTranscorridos());
        // 29/02/2024 → 31(jan)+29 = 60
        System.out.printf("29/02/2024 → %d dia(s) (esperado: 60)%n",
                new Data(29, 2, 2024).diasTranscorridos());

        System.out.println("\n--- Método apresentaDataAtual() ---");
        data1.apresentaDataAtual();

        System.out.println("\n--- Construtor padrão Data() – entrada interativa ---");
        System.out.println("(Informe uma data quando solicitado)");
        Data dataInterativa = new Data();
        exibirInfoData(dataInterativa, "dataInterativa");

        System.out.println("\n--- Teste dos setters interativos ---");
        System.out.println("Alterando o ano:");
        dataInterativa.entraAno();
        System.out.println("Alterando o mês:");
        dataInterativa.entraMes();
        System.out.println("Alterando o dia:");
        dataInterativa.entraDia();
        exibirInfoData(dataInterativa, "dataInterativa após edição");

        System.out.println("\n========== FIM DOS TESTES ==========");
    }


    /** Exibe as quatro informações principais de uma data. */
    private static void exibirInfoData(Data d, String rotulo) {
        System.out.printf("  %-40s mostra1: %s%n", rotulo, d.mostra1());
        System.out.printf("  %-40s mostra2: %s%n", rotulo, d.mostra2());
        System.out.printf("  %-40s bissexto: %b%n", rotulo, d.bissexto());
        System.out.printf("  %-40s diasTranscorridos: %d%n", rotulo, d.diasTranscorridos());
    }

    private static void testarBissexto(int ano) {
        Data d = new Data(1, 1, ano);
        System.out.printf("  Ano %d → bissexto: %b%n", ano, d.bissexto());
    }
}
