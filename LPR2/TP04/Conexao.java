/**
 * Matéria: Linguagem de Programação 2 (CBTLPR2) - ADS 471
 * Dupla: Brandon Oliveira e Eriel de Souza
 * Professor: Wellington Tuler Moraes
 */

import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.SQLException;

public class Conexao {

    private static final String URL =
            "jdbc:sqlserver://localhost:1433;databaseName=aulajava;encrypt=false;trustServerCertificate=true";
    private static final String USUARIO = "sa";
    private static final String SENHA = "Aula@12345Java";

    public static Connection getConexao() throws SQLException {
        return DriverManager.getConnection(URL, USUARIO, SENHA);
    }
}
