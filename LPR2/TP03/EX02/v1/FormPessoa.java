/*
 * Brandon Oliveira Simões
 * Eriel de Jesus Souza
 *
 * INSTITUTO FEDERAL DE EDUCAÇÃO, CIÊNCIA E TECNOLOGIA DE SÃO PAULO
 * CAMPUS CUBATÃO
 * Matéria: CBTLPR2 (Java) – ADS 471 – Professor: Wellington Tuler Moraes
 *
 * Versão 01
 *
 * Para digitar os dados relativos a uma pessoa crie o FormPessoa conforme layout:
 * O campo "Numero" não deve estar acessível à digitação;
 * Os campos "Nome", "Sexo" e "Idade" são de preenchimento obrigatório.
 * O campo "Sexo" só deve aceitar "M" ou "F".
 * Quando se pressionar o botão "OK" os dados digitados deverão ser transferidos
 * ao objeto "UmaPessoa", uma instância da classe Pessoa.
 * Quando se pressionar o botão "Mostrar" descarregar os dados do objeto
 * "UmaPessoa" inclusive o contador "kp".
 */
package tp.pkg03;

import java.awt.*;
import java.awt.event.*;

public class FormPessoa extends Frame implements ActionListener {
    private static final Color COR_FUNDO   = new Color(245, 247, 250);
    private static final Color COR_TITULO  = new Color(41, 98, 163);
    private static final Color COR_ERRO    = new Color(192, 57, 43);
    private static final Color COR_SUCESSO = new Color(39, 134, 77);
    private static final Font  FONTE_LABEL = new Font("SansSerif", Font.BOLD, 13);
    private static final Font  FONTE_CAMPO = new Font("SansSerif", Font.PLAIN, 13);

    private TextField txtNumero, txtNome, txtSexo, txtIdade;
    private Button btnOk, btnMostrar, btnLimpar, btnSair, btnVoltar;
    private Label lblStatus;
    private List lstDados;
    private CardLayout cartas;
    private Panel pnlCartas;

    private Pessoa umaPessoa;

    public FormPessoa() {
        super("Formulário de Pessoa");
        setBackground(COR_FUNDO);
        setLayout(new BorderLayout());

        cartas = new CardLayout();
        pnlCartas = new Panel(cartas);
        pnlCartas.add(criarCadastro(), "cadastro");
        pnlCartas.add(criarLista(), "lista");

        lblStatus = new Label(" ", Label.CENTER);
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
                txtNome.requestFocus();
            }

            public void windowClosing(WindowEvent we) {
                System.exit(0);
            }
        });
    }

    private Panel criarCabecalho() {
        Panel p = new Panel(new FlowLayout(FlowLayout.CENTER, 0, 12));
        p.setBackground(COR_TITULO);
        Label titulo = new Label("Cadastro de Pessoa", Label.CENTER);
        titulo.setFont(new Font("SansSerif", Font.BOLD, 18));
        titulo.setForeground(Color.WHITE);
        p.add(titulo);
        return p;
    }

    private Panel criarCadastro() {
        Panel p = new Panel(new BorderLayout());
        p.add(criarFormulario(), BorderLayout.CENTER);

        Panel botoes = new Panel(new FlowLayout(FlowLayout.CENTER, 10, 10));
        btnOk = new Button("  OK  ");
        btnMostrar = new Button(" Mostrar ");
        btnLimpar = new Button(" Limpar ");
        btnSair = new Button("  Sair  ");
        Button[] lista = { btnOk, btnMostrar, btnLimpar, btnSair };
        for (Button b : lista) {
            b.setFont(FONTE_LABEL);
            b.addActionListener(this);
            botoes.add(b);
        }
        p.add(botoes, BorderLayout.SOUTH);
        return p;
    }

    private Panel criarFormulario() {
        Panel p = new Panel(new GridBagLayout());
        GridBagConstraints g = new GridBagConstraints();
        g.insets = new Insets(8, 15, 8, 15);
        g.anchor = GridBagConstraints.WEST;

        txtNumero = new TextField(20);
        txtNumero.setEditable(false);
        txtNumero.setFocusable(false);
        txtNumero.setBackground(new Color(225, 228, 232));

        txtNome = new TextField(20);
        txtSexo = new TextField(20);
        txtIdade = new TextField(20);

        adicionarLinha(p, g, 0, "Número:", txtNumero);
        adicionarLinha(p, g, 1, "Nome:", txtNome);
        adicionarLinha(p, g, 2, "Sexo (M/F):", txtSexo);
        adicionarLinha(p, g, 3, "Idade:", txtIdade);
        return p;
    }

    private void adicionarLinha(Panel p, GridBagConstraints g, int linha, String texto, Component campo) {
        Label lbl = new Label(texto);
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

    private Panel criarLista() {
        Panel p = new Panel(new BorderLayout(0, 10));

        Label titulo = new Label("Dados de UmaPessoa", Label.CENTER);
        titulo.setFont(FONTE_LABEL);
        p.add(titulo, BorderLayout.NORTH);

        lstDados = new List(8);
        lstDados.setFont(FONTE_CAMPO);
        lstDados.setBackground(Color.WHITE);
        Panel centro = new Panel(new BorderLayout());
        centro.add(lstDados, BorderLayout.CENTER);
        p.add(centro, BorderLayout.CENTER);

        Panel botoes = new Panel(new FlowLayout(FlowLayout.CENTER, 15, 10));
        btnVoltar = new Button(" Voltar ");
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
            String sexoStr = txtSexo.getText().trim().toUpperCase();

            if (nome.isEmpty()) {
                erro("O nome é obrigatório.");
                txtNome.requestFocus();
                return;
            }
            if (sexoStr.isEmpty()) {
                erro("O sexo é obrigatório.");
                txtSexo.requestFocus();
                return;
            }
            if (!sexoStr.equals("M") && !sexoStr.equals("F")) {
                erro("O campo Sexo deve conter apenas 'M' ou 'F'.");
                txtSexo.requestFocus();
                return;
            }

            int idade;
            try {
                idade = Integer.parseInt(txtIdade.getText().trim());
            } catch (NumberFormatException ex) {
                erro("A idade deve ser um número inteiro.");
                txtIdade.requestFocus();
                return;
            }

            umaPessoa = new Pessoa(nome, sexoStr.charAt(0), idade);

            txtNumero.setText(String.valueOf(Pessoa.getKp() + 1));
            txtNome.setText("");
            txtSexo.setText("");
            txtIdade.setText("");
            txtNome.requestFocus();
            sucesso("Pessoa cadastrada com sucesso!");
        }

        if (e.getSource() == btnMostrar) {
            if (umaPessoa != null) {
                lstDados.removeAll();
                lstDados.add("Contador (kp): " + Pessoa.getKp());
                lstDados.add("Nome: " + umaPessoa.getNome());
                lstDados.add("Sexo: " + umaPessoa.getSexo());
                lstDados.add("Idade: " + umaPessoa.getIdade());
                cartas.show(pnlCartas, "lista");
                sucesso("Dados descarregados na lista.");
            } else {
                erro("Nenhum dado foi registrado ainda.");
            }
        }

        if (e.getSource() == btnLimpar) {
            txtNome.setText("");
            txtSexo.setText("");
            txtIdade.setText("");
            lblStatus.setText(" ");
            txtNome.requestFocus();
        }

        if (e.getSource() == btnSair) {
            System.exit(0);
        }

        if (e.getSource() == btnVoltar) {
            cartas.show(pnlCartas, "cadastro");
            lblStatus.setText(" ");
            txtNome.requestFocus();
        }
    }
}