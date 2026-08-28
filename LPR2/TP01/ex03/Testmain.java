
class TestMain {
    public static void main(String[] args) {
        // --- Testando a classe Student ---
        System.out.println("=== TESTES DA CLASSE STUDENT ===");
        Student student1 = new Student("Carlos Silva", "Rua A, 123", "ADS", 2026, 1500.00);

        // Teste de getters e toString inicial
        System.out.println("toString(): " + student1);
        System.out.println("Nome (herdado): " + student1.getName());
        System.out.println("Endereço (herdado): " + student1.getAddress());
        System.out.println("Curso: " + student1.getProgram());
        System.out.println("Ano: " + student1.getYear());
        System.out.println("Mensalidade: " + student1.getFee());

        // Teste de setters
        student1.setAddress("Rua B, 456");
        student1.setProgram("Engenharia de Software");
        student1.setYear(2027);
        student1.setFee(1800.00);

        System.out.println("\nApós atualização de dados:");
        System.out.println("Novo toString(): " + student1);

        // --- Testando a classe Staff ---
        System.out.println("\n=== TESTES DA CLASSE STAFF ===");
        Staff staff1 = new Staff("Maria Oliveira", "Av. Paulista, 1000", "IFSP", 4500.50);

        // Teste de getters e toString inicial
        System.out.println("toString(): " + staff1);
        System.out.println("Nome (herdado): " + staff1.getName());
        System.out.println("Endereço (herdado): " + staff1.getAddress());
        System.out.println("Escola: " + staff1.getSchool());
        System.out.println("Salário: " + staff1.getPay());

        // Teste de setters
        staff1.setAddress("Av. Central, 500");
        staff1.setSchool("IFSP Campus Cubatão");
        staff1.setPay(5200.00);

        System.out.println("\nApós atualização de dados:");
        System.out.println("Novo toString(): " + staff1);
    }
}