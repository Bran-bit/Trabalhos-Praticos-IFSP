import javax.swing.*;
import java.awt.*;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;

public class CalculadoraForm extends JFrame implements ActionListener {

    private JTextField txtVisor;
    private double operando1 = 0;
    private String operador = "";
    private boolean novoNumero = true;

    public CalculadoraForm() {
        super("Calcul...");
        setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
        setSize(250, 300);
        setLocationRelativeTo(null);
        setLayout(new BorderLayout(5, 5));

        // Visor no topo (NORTH)
        txtVisor = new JTextField("0");
        txtVisor.setHorizontalAlignment(JTextField.RIGHT);
        txtVisor.setEditable(false);
        txtVisor.setFont(new Font("Arial", Font.BOLD, 18));
        add(txtVisor, BorderLayout.NORTH);

        // Grid com os botões (5 linhas x 4 colunas)
        JPanel painelBotoes = new JPanel(new GridLayout(5, 4, 3, 3));

        String[] botoes = {
            "7", "8", "9", "/",
            "4", "5", "6", "*",
            "1", "2", "3", "-",
            "0", ".", "=", "+",
            "C", "",  "",  ""
        };

        for (String texto : botoes) {
            if (texto.isEmpty()) {
                painelBotoes.add(new JLabel("")); // Célula vazia para alinhar o botão C
            } else {
                JButton btn = new JButton(texto);
                btn.setFont(new Font("Arial", Font.BOLD, 14));
                btn.addActionListener(this);
                painelBotoes.add(btn);
            }
        }

        add(painelBotoes, BorderLayout.CENTER);
    }

    @Override
    public void actionPerformed(ActionEvent e) {
        String comando = e.getActionCommand();

        // Limpar memória e visor (Botão C)
        if (comando.equals("C")) {
            limpar();
            return;
        }

        // Entrada de dígitos e ponto decimal
        if ((comando.compareTo("0") >= 0 && comando.compareTo("9") <= 0) || comando.equals(".")) {
            if (novoNumero) {
                txtVisor.setText(comando.equals(".") ? "0." : comando);
                novoNumero = false;
            } else {
                if (comando.equals(".") && txtVisor.getText().contains(".")) {
                    return; // Impede múltiplos pontos no mesmo número
                }
                txtVisor.setText(txtVisor.getText() + comando);
            }
            return;
        }

        // Seleção do operador (+, -, *, /)
        if (comando.equals("+") || comando.equals("-") || comando.equals("*") || comando.equals("/")) {
            try {
                operando1 = Double.parseDouble(txtVisor.getText());
                operador = comando;
                novoNumero = true;
            } catch (NumberFormatException ex) {
                txtVisor.setText("Erro");
            }
            return;
        }

        // Processamento do resultado (Botão =)
        if (comando.equals("=")) {
            if (operador.isEmpty()) {
                return;
            }

            double operando2 = 0;
            double resultado = 0;

            try {
                operando2 = Double.parseDouble(txtVisor.getText());

                switch (operador) {
                    case "+":
                        resultado = operando1 + operando2;
                        break;
                    case "-":
                        resultado = operando1 - operando2;
                        break;
                    case "*":
                        resultado = operando1 * operando2;
                        break;
                    case "/":
                        if (operando2 == 0) {
                            throw new ArithmeticException("Divisão por zero");
                        }
                        resultado = operando1 / operando2;
                        break;
                }

                // Se o resultado for número inteiro, remove as casas decimais zeradas
                if (resultado == (long) resultado) {
                    txtVisor.setText(String.format("%d", (long) resultado));
                } else {
                    txtVisor.setText(String.valueOf(resultado));
                }

            } catch (NumberFormatException ex) {
                JOptionPane.showMessageDialog(this, "Entrada numérica inválida!", "Erro", JOptionPane.ERROR_MESSAGE);
                limpar();
            } catch (ArithmeticException ex) {
                JOptionPane.showMessageDialog(this, ex.getMessage(), "Erro Aritmético", JOptionPane.ERROR_MESSAGE);
                limpar();
            } finally {
                // Bloco finalmente para redefinir o estado da operação
                operador = "";
                novoNumero = true;
            }
        }
    }

    private void limpar() {
        txtVisor.setText("0");
        operando1 = 0;
        operador = "";
        novoNumero = true;
    }

    public static void main(String[] args) {
        SwingUtilities.invokeLater(() -> new CalculadoraForm().setVisible(true));
    }
}