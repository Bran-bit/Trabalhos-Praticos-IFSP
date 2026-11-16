/**
 * Matéria: Linguagem de Programação 2 (CBTLPR2) - ADS 471
 * Dupla: Brandon Oliveira e Eriel de Souza
 * Professor: Wellington Tuler Moraes
 */

import java.awt.BorderLayout;
import java.awt.FlowLayout;
import java.awt.Font;
import java.awt.GridLayout;
import java.awt.event.WindowAdapter;
import java.awt.event.WindowEvent;
import java.math.BigDecimal;
import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import javax.swing.JButton;
import javax.swing.JFrame;
import javax.swing.JLabel;
import javax.swing.JOptionPane;
import javax.swing.JPanel;
import javax.swing.JTextField;
import javax.swing.SwingUtilities;

public class TelaPesquisa extends JFrame {

    private static final String SQL =
            "SELECT f.nome_func, f.sal_func, c.ds_cargo "
          + "FROM tbfuncs f LEFT JOIN tbcargos c ON c.cod_cargo = f.cod_cargo "
          + "WHERE f.nome_func LIKE ? ORDER BY f.nome_func";

    private final JTextField txtBusca = new JTextField(20);
    private final JTextField txtNome = new JTextField();
    private final JTextField txtSalario = new JTextField();
    private final JTextField txtCargo = new JTextField();
    private final JButton btnPesquisar = new JButton("Pesquisar");
    private final JButton btnAnterior = new JButton("Anterior");
    private final JButton btnProximo = new JButton("Próximo");

    private Connection conexao;
    private PreparedStatement stmt;
    private ResultSet rs; // o "recordset"

    public TelaPesquisa() {
        super("TRABALHO PRATICO 04");
        montarTela();
        configurarEventos();
        atualizarBotoes();
        setDefaultCloseOperation(EXIT_ON_CLOSE);
        setSize(420, 240);
        setLocationRelativeTo(null);
    }

    private void montarTela() {
        setLayout(new BorderLayout(5, 5));

        // Topo: campo de busca + botão Pesquisar
        JPanel topo = new JPanel(new GridLayout(2, 1));
        JPanel linhaBusca = new JPanel(new FlowLayout());
        JLabel lblBusca = new JLabel("Nome:");
        lblBusca.setFont(lblBusca.getFont().deriveFont(Font.BOLD));
        linhaBusca.add(lblBusca);
        linhaBusca.add(txtBusca);
        JPanel linhaBotao = new JPanel(new FlowLayout());
        linhaBotao.add(btnPesquisar);
        topo.add(linhaBusca);
        topo.add(linhaBotao);
        add(topo, BorderLayout.NORTH);

        // Centro: dados do funcionário
        JPanel dados = new JPanel(new GridLayout(3, 2, 5, 5));
        dados.add(rotulo("Nome:"));    dados.add(txtNome);
        dados.add(rotulo("Salário:")); dados.add(txtSalario);
        dados.add(rotulo("Cargo:"));   dados.add(txtCargo);
        txtNome.setEditable(false);
        txtSalario.setEditable(false);
        txtCargo.setEditable(false);
        add(dados, BorderLayout.CENTER);

        // Rodapé: navegação
        JPanel navegacao = new JPanel(new GridLayout(1, 2));
        navegacao.add(btnAnterior);
        navegacao.add(btnProximo);
        add(navegacao, BorderLayout.SOUTH);
    }

    private JLabel rotulo(String texto) {
        JLabel l = new JLabel(texto);
        l.setFont(l.getFont().deriveFont(Font.BOLD));
        return l;
    }

    private void configurarEventos() {
        btnPesquisar.addActionListener(e -> pesquisar());
        btnProximo.addActionListener(e -> mover(true));
        btnAnterior.addActionListener(e -> mover(false));
        addWindowListener(new WindowAdapter() {
            @Override
            public void windowClosing(WindowEvent e) {
                fecharRecursos();
            }
        });
    }

    /** Executa o SELECT com LIKE usando PreparedStatement e preenche o recordset. */
    private void pesquisar() {
        try {
            fecharRecursos();
            conexao = Conexao.getConexao();
            // ResultSet rolável para permitir ir e voltar (Próximo / Anterior)
            stmt = conexao.prepareStatement(SQL,
                    ResultSet.TYPE_SCROLL_INSENSITIVE, ResultSet.CONCUR_READ_ONLY);
            stmt.setString(1, "%" + txtBusca.getText().trim() + "%");
            rs = stmt.executeQuery();

            if (rs.next()) {
                exibirRegistroAtual();
            } else {
                limparCampos();
                JOptionPane.showMessageDialog(this, "Nenhum funcionário encontrado.");
            }
        } catch (SQLException ex) {
            limparCampos();
            JOptionPane.showMessageDialog(this, "Erro ao pesquisar: " + ex.getMessage(),
                    "Erro", JOptionPane.ERROR_MESSAGE);
        }
        atualizarBotoes();
    }

    /** Navega pelo recordset respeitando os limites inicial e final. */
    private void mover(boolean avancar) {
        try {
            if (rs == null) return;
            boolean moveu = avancar ? rs.next() : rs.previous();
            if (moveu) {
                exibirRegistroAtual();
            } else {
                // passou do limite: volta para o registro válido mais próximo
                if (avancar) rs.last(); else rs.first();
            }
        } catch (SQLException ex) {
            JOptionPane.showMessageDialog(this, "Erro ao navegar: " + ex.getMessage(),
                    "Erro", JOptionPane.ERROR_MESSAGE);
        }
        atualizarBotoes();
    }

    private void exibirRegistroAtual() throws SQLException {
        String nome = rs.getString("nome_func");
        BigDecimal salario = rs.getBigDecimal("sal_func");
        String cargo = rs.getString("ds_cargo");
        txtNome.setText(nome == null ? "" : nome.trim());
        txtSalario.setText(salario == null ? "" : salario.setScale(2, java.math.RoundingMode.HALF_UP).toPlainString());
        txtCargo.setText(cargo == null ? "" : cargo.trim());
    }

    private void atualizarBotoes() {
        boolean temDados = false;
        boolean primeiro = true;
        boolean ultimo = true;
        try {
            if (rs != null && !rs.isClosed() && rs.getRow() > 0) {
                temDados = true;
                primeiro = rs.isFirst();
                ultimo = rs.isLast();
            }
        } catch (SQLException ignorado) {
            temDados = false;
        }
        btnAnterior.setEnabled(temDados && !primeiro);
        btnProximo.setEnabled(temDados && !ultimo);
    }

    private void limparCampos() {
        txtNome.setText("");
        txtSalario.setText("");
        txtCargo.setText("");
    }

    private void fecharRecursos() {
        try { if (rs != null) rs.close(); } catch (SQLException ignorado) { }
        try { if (stmt != null) stmt.close(); } catch (SQLException ignorado) { }
        try { if (conexao != null) conexao.close(); } catch (SQLException ignorado) { }
        rs = null;
        stmt = null;
        conexao = null;
    }

    public static void main(String[] args) {
        SwingUtilities.invokeLater(() -> new TelaPesquisa().setVisible(true));
    }
}
