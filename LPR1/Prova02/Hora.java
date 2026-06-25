package consultaagendada;
import java.util.Scanner;

public class Hora {
	private int hora;
	private int min;
	private int seg;
	
	static Scanner scanner = new Scanner(System.in);

    public Hora() {
        this.setHor();
        this.setMin();
        this.setSeg();
    }
    
    public Hora(int h, int m, int s) {
    	setHor(h);
    	setMin(m);
    	setSeg(s);
    }
    
    //Setters sem parâmetro
    public void setHor() {
    	int hora = -1;
    	do {
    		 try {
    	            System.out.println("Insira o valor das horas (0-23):");
    	            hora = Integer.parseInt(scanner.nextLine().trim());
    	            if (hora < 0 || hora > 23) {
    	                System.out.println("Valor inválido! A hora deve estar entre 0 e 23.");
    	            }
    	        } catch (NumberFormatException e) {
    	            System.out.println("Entrada inválida! Digite apenas números inteiros.");
    	        }
    	} while(hora < 0 || hora > 23);
    	this.hora = hora;
    }
    
    public void setMin() {
    	int minuto = -1;
        do {
            try {
                System.out.println("Insira o valor dos minutos (0-59):");
                minuto = Integer.parseInt(scanner.nextLine().trim());
                if (minuto < 0 || minuto > 59) {
                    System.out.println("Valor invalido! Os minutos devem estar entre 0 e 59.");
                }
            } catch (NumberFormatException e) {
                System.out.println("Entrada invalida! Digite apenas numeros inteiros.");
            }
        } while (minuto < 0 || minuto > 59);
        this.min = minuto;

    }
    
    public void setSeg() {
    	int segundo = -1;
        do {
            try {
                System.out.println("Insira o valor dos segundos (0-59):");
                segundo = Integer.parseInt(scanner.nextLine().trim());
                if (segundo < 0 || segundo > 59) {
                    System.out.println("Valor invalido! Os segundos devem estar entre 0 e 59.");
                }
            } catch (NumberFormatException e) {
                System.out.println("Entrada invalida! Digite apenas numeros inteiros.");
            }
        } while (segundo < 0 || segundo > 59);
        this.seg = segundo;   	
    }
    
    
    //Setters com parâmetro
    public void setHor(int h) {
        this.hora = h;
    }

    public void setMin(int m) {
      this.min = m;
    }

    public void setSeg(int s) {
    	this.seg = s;
    }
    
    //Getters
    public int getHor() {
        return this.hora;
    }

    public int getMin() {
        return this.min;
    }

    public int getSeg() {
        return this.seg;
    }
    
    public String getHoral() {
        return String.format("%02d:%02d:%02d", this.hora, this.min, this.seg);
    }
    
    public String getHora2() {
        String periodo = (this.hora < 12) ? "AM" : "PM";
        int hFormatado = (this.hora % 12 == 0) ? 12 : (this.hora % 12);
        return String.format("%02d:%02d:%02d (%s)", hFormatado, this.min, this.seg, periodo);
    }
    
    public int getSegundos() {
        return (this.hora * 3600) + (this.min * 60) + this.seg;
    }

}
