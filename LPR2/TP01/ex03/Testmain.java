package EX03;
/**
 * Dupla:
 * Brandon Oliveira Simões
 * Eriel de Jesus Souza
 */

import java.util.Scanner;

class TestMain {
    public static void main(String[] args) {
        Scanner scanner = new Scanner(System.in);

        System.out.println("=== SISTEMA DE TESTE - EXERCÍCIO 03 ===\n");

        System.out.println("--- CADASTRO DE ESTUDANTE ---");
        System.out.print("Nome: ");
        String studentName = scanner.nextLine();
        System.out.print("Endereço: ");
        String studentAddress = scanner.nextLine();
        System.out.print("Programa/Curso: ");
        String program = scanner.nextLine();
        System.out.print("Ano: ");
        int year = scanner.nextInt();
        System.out.print("Mensalidade (R$): ");
        double fee = scanner.nextDouble();
        scanner.nextLine();

        Student student = null;
        try {
            student = new Student(studentName, studentAddress, program, year, fee);
            System.out.println("Estudante cadastrado com sucesso!\n");
        } catch (IllegalArgumentException e) {
            System.out.println("Erro ao cadastrar estudante: " + e.getMessage());
            student = new Student("Padrão", "Endereço Padrão", "Curso Padrão", 1, 0);
            System.out.println("Criado estudante padrão para continuar.\n");
        }

        System.out.println("--- CADASTRO DE STAFF ---");
        System.out.print("Nome: ");
        String staffName = scanner.nextLine();
        System.out.print("Endereço: ");
        String staffAddress = scanner.nextLine();
        System.out.print("Escola: ");
        String school = scanner.nextLine();
        System.out.print("Salário (R$): ");
        double pay = scanner.nextDouble();
        scanner.nextLine();

        Staff staff = null;
        try {
            staff = new Staff(staffName, staffAddress, school, pay);
            System.out.println("Staff cadastrado com sucesso!\n");
        } catch (IllegalArgumentException e) {
            System.out.println("Erro ao cadastrar Staff: " + e.getMessage());
            staff = new Staff("Padrão", "Endereço Padrão", "Escola Padrão", 0);
            System.out.println("Criado Staff padrão para continuar.\n");
        }

        System.out.println("=== DADOS CADASTRADOS ===");
        System.out.println("Estudante: " + student);
        System.out.println("Staff: " + staff);
        System.out.println();

        System.out.println("=== MODIFICANDO DADOS ===");
        System.out.println("--- ALTERANDO ESTUDANTE ---");
        System.out.print("Novo endereço: ");
        String newAddress = scanner.nextLine();
        try {
            student.setAddress(newAddress);
        } catch (IllegalArgumentException e) {
            System.out.println("Erro: " + e.getMessage() + ". Endereço não alterado.");
        }

        System.out.print("Novo programa: ");
        String newProgram = scanner.nextLine();
        try {
            student.setProgram(newProgram);
        } catch (IllegalArgumentException e) {
            System.out.println("Erro: " + e.getMessage() + ". Programa não alterado.");
        }

        System.out.print("Novo ano: ");
        int newYear = scanner.nextInt();
        try {
            student.setYear(newYear);
        } catch (IllegalArgumentException e) {
            System.out.println("Erro: " + e.getMessage() + ". Ano não alterado.");
        }

        System.out.print("Nova mensalidade (R$): ");
        double newFee = scanner.nextDouble();
        scanner.nextLine();
        try {
            student.setFee(newFee);
        } catch (IllegalArgumentException e) {
            System.out.println("Erro: " + e.getMessage() + ". Mensalidade não alterada.");
        }

        System.out.println("--- ALTERANDO STAFF ---");
        System.out.print("Novo endereço: ");
        String newStaffAddress = scanner.nextLine();
        try {
            staff.setAddress(newStaffAddress);
        } catch (IllegalArgumentException e) {
            System.out.println("Erro: " + e.getMessage() + ". Endereço não alterado.");
        }

        System.out.print("Nova escola: ");
        String newSchool = scanner.nextLine();
        try {
            staff.setSchool(newSchool);
        } catch (IllegalArgumentException e) {
            System.out.println("Erro: " + e.getMessage() + ". Escola não alterada.");
        }

        System.out.print("Novo salário (R$): ");
        double newPay = scanner.nextDouble();
        scanner.nextLine();
        try {
            staff.setPay(newPay);
        } catch (IllegalArgumentException e) {
            System.out.println("Erro: " + e.getMessage() + ". Salário não alterado.");
        }

        System.out.println("\n=== TESTE COMPLETO ===");
        System.out.println("\n--- TESTE ESTUDANTE ---");
        System.out.println("toString(): " + student);
        System.out.println("Nome: " + student.getName());
        System.out.println("Endereço: " + student.getAddress());
        System.out.println("Programa: " + student.getProgram());
        System.out.println("Ano: " + student.getYear());
        System.out.println("Mensalidade: R$ " + student.getFee());

        System.out.println("\n--- TESTE STAFF ---");
        System.out.println("toString(): " + staff);
        System.out.println("Nome: " + staff.getName());
        System.out.println("Endereço: " + staff.getAddress());
        System.out.println("Escola: " + staff.getSchool());
        System.out.println("Salário: R$ " + staff.getPay());

        System.out.println("\n--- TESTE MÉTODOS HERDADOS ---");
        System.out.println("Estudante.getName(): " + student.getName());
        System.out.println("Estudante.getAddress(): " + student.getAddress());
        System.out.println("Staff.getName(): " + staff.getName());
        System.out.println("Staff.getAddress(): " + staff.getAddress());

        // ===== TESTE DE VALIDAÇÃO =====
        System.out.println("\n=== TESTE DE VALIDAÇÃO ===");
        System.out.println("Testando validações com dados inválidos (automático):\n");

        System.out.println("1. Tentando criar estudante com nome vazio...");
        try {
            Student invalidStudent = new Student("", "Rua X", "ADS", 2026, 1000);
            System.out.println("ERRO: Deveria ter lançado exceção para nome vazio!");
        } catch (IllegalArgumentException e) {
            System.out.println("Sucesso: " + e.getMessage());
        }

        System.out.println("\n2. Tentando criar staff com salário negativo...");
        try {
            Staff invalidStaff = new Staff("João", "Rua Y", "IFSP", -100);
            System.out.println("ERRO: Deveria ter lançado exceção para salário negativo!");
        } catch (IllegalArgumentException e) {
            System.out.println("Sucesso: " + e.getMessage());
        }

        System.out.println("\n3. Tentando criar estudante com ano 0...");
        try {
            Student invalidStudent2 = new Student("Ana", "Rua Z", "Computação", 0, 500);
            System.out.println("ERRO: Deveria ter lançado exceção para ano inválido!");
        } catch (IllegalArgumentException e) {
            System.out.println("Sucesso: " + e.getMessage());
        }

        System.out.println("\n4. Tentando alterar endereço para vazio...");
        Student s = new Student("Válido", "Endereço Válido", "Curso", 1, 0);
        try {
            s.setAddress("");
            System.out.println("ERRO: Deveria ter lançado exceção para endereço vazio!");
        } catch (IllegalArgumentException e) {
            System.out.println("Sucesso: " + e.getMessage());
        }

        System.out.println("\n5. Tentando alterar programa para vazio...");
        Student s2 = new Student("Válido", "Endereço", "Curso", 1, 0);
        try {
            s2.setProgram("   ");
            System.out.println("ERRO: Deveria ter lançado exceção para programa vazio!");
        } catch (IllegalArgumentException e) {
            System.out.println("Sucesso: " + e.getMessage());
        }

        System.out.println("\n6. Tentando alterar escola para vazia...");
        Staff st = new Staff("Válido", "Endereço", "Escola Válida", 0);
        try {
            st.setSchool("");
            System.out.println("ERRO: Deveria ter lançado exceção para escola vazia!");
        } catch (IllegalArgumentException e) {
            System.out.println("Sucesso: " + e.getMessage());
        }

        System.out.println("\n7. Tentando alterar mensalidade para negativa...");
        Student s3 = new Student("Válido", "Endereço", "Curso", 1, 100);
        try {
            s3.setFee(-50);
            System.out.println("ERRO: Deveria ter lançado exceção para mensalidade negativa!");
        } catch (IllegalArgumentException e) {
            System.out.println("Sucesso: " + e.getMessage());
        }

        scanner.close();
        System.out.println("\n=== FIM DO PROGRAMA ===");
    }
}