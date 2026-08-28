/**
 * Nome: Brandon Oliveira Simões
 * Nome: Eriel de Jesus Souza
 * Enunciado:
 * Um livro pode ser escrito por um ou muitos autores, por esta razão a classe Book deve ter um array de autores, conforme o modelo abaixo:

O construtor deve receber um array de autores.

Uma vez que o livro é instanciado o seu autor não pode ser adicionado ou removido.

O método toString() deve retornar a seguinte resposta:
"Book[name=?,authors={Author[name=?,email=?,gender=?,}],price=?,qty=?"].

Você deve:

Escrever um código para a classe Book, reutilizando o código do Author escrito no exercício 1.

Escrever uma classe de teste, chamada TestBook, para testar a classe Book.
 */
package ex02;

import java.util.Scanner;

public class TestBook {
    public static void main(String[] args) {
        Scanner scanner = new Scanner(System.in);
        
        System.out.println("=== SISTEMA DE CADASTRO DE LIVROS ===\n");
        
        // Cadastro de autores
        System.out.println("--- CADASTRO DE AUTORES ---");
        System.out.print("Quantos autores o livro terá? ");
        int numAutores = scanner.nextInt();
        scanner.nextLine();
        
        Author[] authors = new Author[numAutores];
        
        for (int i = 0; i < numAutores; i++) {
            System.out.println("\n--- Autor " + (i + 1) + " ---");
            
            System.out.print("Nome do autor: ");
            String name = scanner.nextLine();
            
            System.out.print("Email do autor: ");
            String email = scanner.nextLine();
            
            char gender;
            while (true) {
                System.out.print("Gênero (m/f): ");
                String genderInput = scanner.nextLine().toLowerCase();
                if (genderInput.length() == 1 && (genderInput.charAt(0) == 'm' || genderInput.charAt(0) == 'f')) {
                    gender = genderInput.charAt(0);
                    break;
                } else {
                    System.out.println("Gênero inválido! Digite 'm' ou 'f'.");
                }
            }
            
            authors[i] = new Author(name, email, gender);
            System.out.println("Autor cadastrado com sucesso!");
        }
        
        // Cadastro do livro
        System.out.println("\n--- CADASTRO DO LIVRO ---");
        
        System.out.print("Título do livro: ");
        String bookName = scanner.nextLine();
        
        System.out.print("Preço do livro (R$): ");
        double price = scanner.nextDouble();
        
        System.out.print("Quantidade em estoque: ");
        int qty = scanner.nextInt();
        
        Book book = new Book(bookName, authors, price, qty);
        System.out.println("\nLivro cadastrado com sucesso!");
        
        // Exibicao dos dados
        System.out.println("\n==================================================");
        System.out.println("DADOS DO LIVRO CADASTRADO");
        System.out.println("==================================================");
        System.out.println(book);
        
        // Teste de getters
        System.out.println("\n--- TESTE DE GETTERS ---");
        System.out.println("Título: " + book.getName());
        System.out.println("Preço: R$ " + book.getPrice());
        System.out.println("Quantidade: " + book.getQty());
        System.out.println("Autores: " + book.getAuthorNames());
        
        // Exibe os autores individualmente
        System.out.println("\n--- DETALHES DOS AUTORES ---");
        Author[] bookAuthors = book.getAuthors();
        for (int i = 0; i < bookAuthors.length; i++) {
            System.out.println("Autor " + (i + 1) + ": " + bookAuthors[i]);
        }
        
        // Teste de setters
        System.out.println("\n--- TESTE DE SETTERS ---");
        
        System.out.print("Novo preço do livro (R$): ");
        double newPrice = scanner.nextDouble();
        book.setPrice(newPrice);
        System.out.println("Preço atualizado para: R$ " + book.getPrice());
        
        System.out.print("Nova quantidade em estoque: ");
        int newQty = scanner.nextInt();
        book.setQty(newQty);
        System.out.println("Quantidade atualizada para: " + book.getQty());
        
        // Exibicao final
        System.out.println("\n==================================================");
        System.out.println("DADOS ATUALIZADOS DO LIVRO");
        System.out.println("==================================================");
        System.out.println(book);
        
        // Teste de validacao do construtor
        System.out.println("\n--- TESTE DE VALIDAÇÃO DO CONSTRUTOR ---");
        System.out.println("Tentando criar um autor com gênero inválido...");
        try {
            Author invalidAuthor = new Author("Teste", "teste@email.com", 'x');
            System.out.println("ERRO: O construtor deveria lançar uma exceção!");
        } catch (IllegalArgumentException e) {
            System.out.println("Sucesso: Exceção capturada - " + e.getMessage());
        }
        
        // Teste de imutabilidade
        System.out.println("\n--- TESTE DE IMUTABILIDADE DOS AUTORES ---");
        System.out.println("Tentando modificar o array de autores...");
        Author[] authorsCopy = book.getAuthors();
        try {
            authorsCopy[0] = new Author("Hacker", "hacker@email.com", 'm');
            System.out.println("O array pode ter sido modificado, mas o original permanece intacto!");
            System.out.println("Autor original ainda é: " + book.getAuthors()[0]);
        } catch (Exception e) {
            System.out.println("Erro ao modificar: " + e.getMessage());
        }
        
        scanner.close();
        System.out.println("\n=== FIM DO PROGRAMA ===");
    }
}