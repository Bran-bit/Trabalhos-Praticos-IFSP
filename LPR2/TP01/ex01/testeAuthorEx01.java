/**
 * Nome: Brandon Oliveira Simões
 * Nome: Eriel de Jesus Souza
 * 
 * Enunciado: 
 * Uma classe chamada Author é desenhada para servir de modelo para autores de livros,
veja abaixo:

- 3 atributos privados name (String), email (String), and gender (char contendo 'm' or
'f');
- Um construtor para inicializar os atributos com base nos 3 parâmetros.
- Não existe um construtor default para Author [Author()].
- Criar métodos públicos: getName(), getEmail(), setEmail(), and getGender().
- Não existe setter para name e gender, estes atributos não podem ser alterados.
- Um método chamado toString() que retorna "Author[name=?,email=?,gender=?]",
exemplo "Author[name=Wellington Tuler,email=tulermoraes@yahoo.com,gender=m]".
- Escreva a classe Author e desenvolva uma de teste, com os seguintes itens:

- Testar construtor.
- Verificar o método toString().
- Testar o Setter
- Testar os Getters
 */

package ex01;

import java.util.Scanner;

public class testeAuthorEx01 {

    public static void main(String[] args) {
        Scanner scanner = new Scanner(System.in);
        
        System.out.println("=== Teste interativo da classe Author ===\n");

        // 1) Coletar dados do usuário e testar construtor
        System.out.print("Digite o nome do autor: ");
        String nome = scanner.nextLine();

        System.out.print("Digite o email do autor: ");
        String email = scanner.nextLine();

        // Loop para garantir gênero válido antes de criar o objeto
        char genero;
        while (true) {
            System.out.print("Digite o gênero (m/f): ");
            genero = scanner.next().toLowerCase().charAt(0);
            if (genero == 'm' || genero == 'f') {
                break;
            }
            System.out.println("Valor inválido! Digite apenas 'm' ou 'f'.");
        }

        Author autor = new Author(nome, email, genero);
        System.out.println("\nConstrutor -> objeto criado com sucesso.");

        // Verificar toString()
        System.out.println("toString() -> " + autor);

        // Testar os Getters
        System.out.println("\n--- Getters ---");
        System.out.println("getName()   -> " + autor.getName());
        System.out.println("getEmail()  -> " + autor.getEmail());
        System.out.println("getGender() -> " + autor.getGender());

        // 4) Testar o Setter de email
        System.out.println("\n--- Setter ---");
        scanner.nextLine();
        System.out.print("Digite o novo email: ");
        String novoEmail = scanner.nextLine();
        autor.setEmail(novoEmail);
        System.out.println("Após setEmail() -> " + autor.getEmail());
        System.out.println("toString() atualizado -> " + autor);

        // Teste da validação de gênero inválido 
        System.out.println("\n--- Validação de gênero inválido (teste automático) ---");
        try {
            Author autorInvalido = new Author("Fulano", "fulano@teste.com", 'x');
            System.out.println("Não deveria chegar aqui: " + autorInvalido);
        } catch (IllegalArgumentException e) {
            System.out.println("Exceção capturada com sucesso: " + e.getMessage());
        }

        scanner.close();
    }
}
