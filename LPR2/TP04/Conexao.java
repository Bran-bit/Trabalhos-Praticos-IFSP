/**
 * Matéria: Linguagem de Programação 2 (CBTLPR2) - ADS 471
 * Dupla: Brandon Oliveira e Eriel de Souza
 * Professor: Wellington Tuler Moraes
 */

import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.SQLException;

/** Responsável por estabelecer a conexão JDBC com o SQL Server. */
public class Conexao {

    // AJUSTE aqui conforme o seu SQL Server
    private static final String URL =
            "jdbc:sqlserver://localhost:1433;databaseName=aulajava;encrypt=false;trustServerCertificate=true";
    private static final String USUARIO = "sa";
    private static final String SENHA = "sua_senha_aqui";

    public static Connection getConexao() throws SQLException {
        return DriverManager.getConnection(URL, USUARIO, SENHA);
    }
}
