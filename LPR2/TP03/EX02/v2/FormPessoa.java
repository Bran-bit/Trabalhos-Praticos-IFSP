/*
 * Brandon Oliveira Simões
 * Eriel de Jesus Souza
 *
 * INSTITUTO FEDERAL DE EDUCAÇÃO, CIÊNCIA E TECNOLOGIA DE SÃO PAULO
 * CAMPUS CUBATÃO
 * Matéria: CBTLPR2 (Java) – ADS 471 – Professor: Wellington Tuler Moraes
 *
 * Versão 02
 *
 * Refaça o exercício anterior, porém agora o sexo não deve ser digitado através de
 * um TextField e sim escolhido através de um componente do tipo "JComboBox".
 */
package tp.pkg03;

import java.awt.*;
import java.awt.event.*;
import javax.swing.*;

public class FormPessoa extends JFrame implements ActionListener {
    private static final Color COR_FUNDO   = new Color(245, 247, 250);
    private static final Color COR_TITULO  = new Color(41, 98, 163);
    private static final Color COR_ERRO    = new Color(192, 57, 43);
    private static final Color COR_SUCESSO = new Color(39, 134, 77);
    private static final Font  FONTE_LABEL = new Font("SansSerif", Font.BOLD, 13);
    private static final Font  FONTE_CAMPO = new Font("SansSerif", Font.PLAIN, 13);

    private JTextField txtNumero, txtNome, txtIdade;
    private JComboBox<String> cmbSexo;
    private JButton btnOk, btnMostrar, btnLimpar, btnSair, btnVoltar;
    private JLabel lblStatus;
    private JList<String> lstDados;
    private DefaultListModel<String> modeloLista;
    private CardLayout cartas;
    private JPanel pnlCartas;

    private Pessoa umaPessoa;

    public FormPessoa() {
        super("Formulário de Pessoa");
        setDefaultCloseOperation(EXIT_ON_CLOSE);
        getContentPane().setBackground(COR_FUNDO);
        setLayout(new BorderLayout());

        cartas = new CardLayout();
        pnlCartas = new JPanel(cartas);
        pnlCartas.setBackground(COR_FUNDO);
        pnlCartas.add(criarCadastro(), "cadastro");
        pnlCartas.add(criarLista(), "lista");

        lblStatus = new JLabel(" ", SwingConstants.CENTER);
        lblStatus.setFont(new Font("SansSerif", Font.ITALIC, 12));

        add(criarCabecalho(), BorderLayout.NORTH);
        add(pnlCartas, BorderLayout.CENTER);
        add(lblStatus, BorderLayout.SOUTH);

        txtNumero.setText(String.valueOf(Pessoa.getKp() + 1));

        pack();
        setSize(Math.max(getWidth(), 420), getHeight());
        setResizable(false);
        setLocationRelativeTo(null);

        addWindowListener(new WindowAdapter() {
            public void windowOpened(WindowEvent we) {
                txtNome.requestFocusInWindow();
            }
        });
    }

    private JPanel criarCabecalho() {
        JPanel p = new JPanel(new FlowLayout(FlowLayout.CENTER, 0, 12));
        p.setBackground(COR_TITULO);
        JLabel titulo = new JLabel("Cadastro de Pessoa", SwingConstants.CENTER);
        titulo.setFont(new Font("SansSerif", Font.BOLD, 18));
        titulo.setForeground(Color.WHITE);
        p.add(titulo);
        return p;
    }

    private JPanel criarCadastro() {
        JPanel p = new JPanel(new BorderLayout());
        p.setBackground(COR_FUNDO);
        p.add(criarFormulario(), BorderLayout.CENTER);

        JPanel botoes = new JPanel(new FlowLayout(FlowLayout.CENTER, 10, 10));
        botoes.setBackground(COR_FUNDO);
        btnOk = new JButton("OK");
        btnMostrar = new JButton("Mostrar");
        btnLimpar = new JButton("Limpar");
        btnSair = new JButton("Sair");
        JButton[] lista = { btnOk, btnMostrar, btnLimpar, btnSair };
        for (JButton b : lista) {
            b.setFont(FONTE_LABEL);
            b.addActionListener(this);
            botoes.add(b);
        }
        p.add(botoes, BorderLayout.SOUTH);
        return p;
    }

    private JPanel criarFormulario() {
        JPanel p = new JPanel(new GridBagLayout());
        p.setBackground(COR_FUNDO);
        GridBagConstraints g = new GridBagConstraints();
        g.insets = new Insets(8, 15, 8, 15);
        g.anchor = GridBagConstraints.WEST;

        txtNumero = new JTextField(20);
        txtNumero.setEditable(false);
        txtNumero.setFocusable(false);
        txtNumero.setBackground(new Color(225, 228, 232));

        txtNome = new JTextField(20);
        txtIdade = new JTextField(20);

        cmbSexo = new JComboBox<>(new String[] { "M", "F" });
        cmbSexo.setSelectedIndex(-1);

        adicionarLinha(p, g, 0, "Número:", txtNumero);
        adicionarLinha(p, g, 1, "Nome:", txtNome);
        adicionarLinha(p, g, 2, "Sexo:", cmbSexo);
        adicionarLinha(p, g, 3, "Idade:", txtIdade);
        return p;
    }

    private void adicionarLinha(JPanel p, GridBagConstraints g, int linha, String texto, JComponent campo) {
        JLabel lbl = new JLabel(texto);
        lbl.setFont(FONTE_LABEL);
        campo.setFont(FONTE_CAMPO);

        g.gridy = linha;
        g.gridx = 0;
        g.weightx = 0;
        g.fill = GridBagConstraints.NONE;
        p.add(lbl, g);

        g.gridx = 1;
        g.weightx = 1;
        g.fill = GridBagConstraints.HORIZONTAL;
        p.add(campo, g);
    }

    private JPanel criarLista() {
        JPanel p = new JPanel(new BorderLayout(0, 10));
        p.setBackground(COR_FUNDO);
        p.setBorder(BorderFactory.createEmptyBorder(10, 15, 0, 15));

        JLabel titulo = new JLabel("Dados de UmaPessoa", SwingConstants.CENTER);
        titulo.setFont(FONTE_LABEL);
        p.add(titulo, BorderLayout.NORTH);

        modeloLista = new DefaultListModel<>();
        lstDados = new JList<>(modeloLista);
        lstDados.setFont(FONTE_CAMPO);
        lstDados.setVisibleRowCount(8);
        p.add(new JScrollPane(lstDados), BorderLayout.CENTER);

        JPanel botoes = new JPanel(new FlowLayout(FlowLayout.CENTER, 15, 10));
        botoes.setBackground(COR_FUNDO);
        btnVoltar = new JButton("Voltar");
        btnVoltar.setFont(FONTE_LABEL);
        btnVoltar.addActionListener(this);
        botoes.add(btnVoltar);
        p.add(botoes, BorderLayout.SOUTH);
        return p;
    }

    private void erro(String msg) {
        lblStatus.setForeground(COR_ERRO);
        lblStatus.setText(msg);
    }

    private void sucesso(String msg) {
        lblStatus.setForeground(COR_SUCESSO);
        lblStatus.setText(msg);
    }

    public void actionPerformed(ActionEvent e) {
        if (e.getSource() == btnOk) {
            String nome = txtNome.getText().trim();

            if (nome.isEmpty()) {
                erro("O nome é obrigatório.");
                txtNome.requestFocusInWindow();
                return;
            }
            if (cmbSexo.getSelectedIndex() == -1) {
                erro("Selecione o sexo.");
                cmbSexo.requestFocusInWindow();
                return;
            }

            int idade;
            try {
                idade = Integer.parseInt(txtIdade.getText().trim());
            } catch (NumberFormatException ex) {
                erro("A idade deve ser um número inteiro.");
                txtIdade.requestFocusInWindow();
                return;
            }

            char sexo = cmbSexo.getSelectedItem().toString().charAt(0);
            umaPessoa = new Pessoa(nome, sexo, idade);

            txtNumero.setText(String.valueOf(Pessoa.getKp() + 1));
            txtNome.setText("");
            cmbSexo.setSelectedIndex(-1);
            txtIdade.setText("");
            txtNome.requestFocusInWindow();
            sucesso("Pessoa cadastrada com sucesso!");
        }

        if (e.getSource() == btnMostrar) {
            if (umaPessoa != null) {
                modeloLista.clear();
                modeloLista.addElement("Contador (kp): " + Pessoa.getKp());
                modeloLista.addElement("Nome: " + umaPessoa.getNome());
                modeloLista.addElement("Sexo: " + umaPessoa.getSexo());
                modeloLista.addElement("Idade: " + umaPessoa.getIdade());
                cartas.show(pnlCartas, "lista");
                sucesso("Dados descarregados na lista.");
            } else {
                erro("Nenhum dado foi registrado ainda.");
            }
        }

        if (e.getSource() == btnLimpar) {
            txtNome.setText("");
            cmbSexo.setSelectedIndex(-1);
            txtIdade.setText("");
            lblStatus.setText(" ");
            txtNome.requestFocusInWindow();
        }

        if (e.getSource() == btnSair) {
            System.exit(0);
        }

        if (e.getSource() == btnVoltar) {
            cartas.show(pnlCartas, "cadastro");
            lblStatus.setText(" ");
            txtNome.requestFocusInWindow();
        }
    }
}