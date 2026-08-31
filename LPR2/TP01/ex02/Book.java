package ex02;
/**
 * Dupla:
 * Brandon Oliveira Simões
 * Eriel de Jesus Souza
 */


public class Book {
    private final String name;
    private final Author[] authors;
    private double price;
    private int qty = 0;

    public Book(String name, Author[] authors, double price) {
        this.name = name;
        this.authors = authors;
        this.price = price;
    }

    public Book(String name, Author[] authors, double price, int qty) {
        this.name = name;
        this.authors = authors;
        this.price = price;
        this.qty = qty;
    }

    public String getName() {
        return name;
    }

    public Author[] getAuthors() {
        //retorna uma cópia pra não retornar a referência
        //o que permitiria modificar os autores
        return authors.clone();
    }

    public double getPrice() {
        return price;
    }

    public void setPrice(double price) {
        this.price = price;
    }

    public int getQty() {
        return qty;
    }

    public void setQty(int qty) {
        this.qty = qty;
    }

    public String getAuthorNames() {
        StringBuilder names = new StringBuilder();
        for (int i = 0; i < authors.length; i++) {
            names.append(authors[i].getName());
            if (i < authors.length - 1) {
                names.append(", ");
            }
        }
        return names.toString();
    }

    @Override
    public String toString() {
        StringBuilder authorsString = new StringBuilder("{");
        for (int i = 0; i < authors.length; i++) {
            authorsString.append(authors[i].toString());
            if (i < authors.length - 1) {
                authorsString.append(",");
            }
        }
        authorsString.append("}");

        return "Book[name=" + name + 
               ",authors=" + authorsString.toString() + 
               ",price=" + price + 
               ",qty=" + qty + "]";
    }
}
