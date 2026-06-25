package consultaagendada;

/*
    Dupla: Brandon Oliveira Simões e Eriel de Jesus Souza
    Enunciado:
        Para testar a classe criada siga os passos abaixo respeitando a ordem em que eles
    serão solicitados:
    • Usando a classe ConsultaAgendada instancie o objeto p1 inicializado-o com o
    construtor ConsultaAgendada (int h, int mi, int s, int d, int m, int a, String p, String
    m);
    • Exiba todas as propriedades de p1;
    • Agora instancie o objeto p2 usando o construtor ConsultaAgendada ();
    • Exiba todas as propriedades de p2;
    • Usando os métodos setData(), setHora(), setNomePaciente(), setNomeMedico altere
    as propriedades de p1;
    • Exiba todas as propriedades de p1 novamente.
    • Exiba a quantidade final de consultas.
*/

public class TesteConsulta {

    public static void main(String[] args) {

        // 1. Instanciar p1 com o construtor parametrizado
        System.out.println("--- Criando objeto p1 ---");
        ConsultaAgendada p1 = new ConsultaAgendada(
            14, 30, 0,          // hora, minuto, segundo
            22, 6, 2026,        // dia, mês, ano
            "Eriel Souza",
            "Dr. Silva"
        );
        System.out.println();

        // 2. Exibir todas as propriedades de p1
        System.out.println("--- Propriedades de p1 ---");
        System.out.println("Paciente: " + p1.getNomePaciente());
        System.out.println("Médico:   " + p1.getNomeMedico());
        System.out.println("Data:     " + p1.getData());
        System.out.println("Hora:     " + p1.getHora());
        System.out.println();

        // 3. Instanciar p2 com o construtor padrão (digitando)
        System.out.println("--- Criando objeto p2 ---");
        ConsultaAgendada p2 = new ConsultaAgendada();
        System.out.println();

        // 4. Exibir todas as propriedades de p2
        System.out.println("--- Propriedades de p2 ---");
        System.out.println("Paciente: " + p2.getNomePaciente());
        System.out.println("Médico:   " + p2.getNomeMedico());
        System.out.println("Data:     " + p2.getData());
        System.out.println("Hora:     " + p2.getHora());
        System.out.println();

        // 5. Alterar as propriedades de p1 usando os métodos set de digitação
        System.out.println("--- Alterando propriedades de p1 ---");
        p1.setData();
        p1.setHora();
        p1.setNomePaciente();
        p1.setNomeMedico();
        System.out.println();

        // 6. Exibir novamente as propriedades de p1
        System.out.println("--- Novas propriedades de p1 ---");
        System.out.println("Paciente: " + p1.getNomePaciente());
        System.out.println("Médico:   " + p1.getNomeMedico());
        System.out.println("Data:     " + p1.getData());
        System.out.println("Hora:     " + p1.getHora());
        System.out.println();

        // 7. Exibir a quantidade final de consultas
        System.out.println("--- Quantidade de consultas ---");
        System.out.println("Total: " + ConsultaAgendada.getAmostra());
    }
}
