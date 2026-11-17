using ProjetoTransporte.Services;
using ProjetoTransporte.UI;

Console.OutputEncoding = System.Text.Encoding.UTF8;

var empresa = new Transportadora();

// Dados iniciais do enunciado: 2 garagens e 8 vans (numeradas de 1 a 8).
// Novos aeroportos/veículos entram apenas pelo menu (opções 1 e 2).
empresa.CadastrarGaragem("Congonhas");
empresa.CadastrarGaragem("Guarulhos");
for (int i = 0; i < 8; i++)
    empresa.CadastrarVeiculo(capacidade: 15);

new Menu(empresa).Executar();
