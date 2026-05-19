/**
 * Dupla:
 * Brandon Oliveira Simões
 * Eriel de Jesus Souza
 * 
 *  Enunciado:
 *  Agora, desenvolva um programa capaz de testar a classe e os métodos desenvolvidos no
exercício anterior.
 *  */

package trab;

public class testarHora {
	public static void main(String[] args) {
		Hora hora = new Hora(0, 0, 0);
        int opcao = -1; 
        
        System.out.println("######################## BEM VINDO ########################");
        System.out.println("");
        System.out.println("Vamos utilizar o programa para definir as horas, e exibir, ate mesmo em segundos.");
        
        do {    
        	System.out.println("\n0 -ENCERRAR PROGRAMA");
            System.out.println("1 - DEFINIR NOVO HORARIO");
            System.out.println("2 - MOSTRAR HORARIO");
            
            System.out.print("Escolha uma opcao: ");
            
            try {
                opcao = Integer.parseInt(Hora.scanner.nextLine().trim());
                //se pega um formato inválido, faz ir para a opção default
            } catch (NumberFormatException e) {
                opcao = -1; 
            }

            switch (opcao) {
                case 0:
                    System.out.println("Encerrando programa...");
                    break;

                case 1:
                    hora.setHor();
                    hora.setMin();
                    hora.setSeg();
                    System.out.println("Horário definido com sucesso!");
                    System.out.println("Pressione ENTER para continuar...");
                    Hora.scanner.nextLine();   
                    break;

                case 2:
                        System.out.println("Hora inserida: " + hora.getHoral());
                        System.out.println("Hora formatada: " + hora.getHora2());
                        System.out.println("Hora em segundos: " + hora.getSegundos() + " segundos");
                        System.out.println("Pressione ENTER para continuar...");
                        Hora.scanner.nextLine();                    
                    break;

                default:
                    System.out.println("Opcao invalida, tente novamente.");
                    System.out.println("Pressione ENTER para continuar...");
                    Hora.scanner.nextLine();
                    break;
            }  
            
        } while (opcao != 0);
        
        Hora.scanner.close();
	}
	
}

