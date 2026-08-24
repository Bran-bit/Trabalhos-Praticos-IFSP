using System;
using System.Collections.Generic;

namespace projetoCurso
{
    internal class Program
    {
        static List<Aluno> alunos = new List<Aluno>();

        static void Main(string[] args)
        {
            Escola escola = new Escola();
            int opcao;

            do
            {
                Console.Clear();
                Console.WriteLine("=== Sistema Escolar ===");
                Console.WriteLine("0. Sair");
                Console.WriteLine("1. Cadastrar aluno");
                Console.WriteLine("2. Cadastrar curso");
                Console.WriteLine("3. Adicionar disciplina a curso");
                Console.WriteLine("4. Matricular aluno em disciplina");
                Console.WriteLine("5. Desmatricular aluno de disciplina");
                Console.WriteLine("6. Listar dados");
                Console.Write("Opção: ");

                if (!int.TryParse(Console.ReadLine(), out opcao))
                {
                    opcao = -1;
                    Console.WriteLine("Opção inválida.");
                }
                else
                {
                    switch (opcao)
                    {
                        case 1: CadastrarAluno(); break;
                        case 2: CadastrarCurso(escola); break;
                        case 3: AdicionarDisciplina(escola); break;
                        case 4: MatricularAluno(escola); break;
                        case 5: DesmatricularAluno(escola); break;
                        case 6: ListarDados(escola); break;
                        case 0: Console.WriteLine("Encerrando..."); break;
                        default: Console.WriteLine("Opção inválida."); break;
                    }
                }

                Console.WriteLine("\nPressione qualquer tecla para continuar...");
                Console.ReadKey();

            } while (opcao != 0);
        }

        static void CadastrarAluno()
        {
            Console.WriteLine("Cadastro de Aluno");

            int id = LerInteiro("ID do aluno: ", 1, int.MaxValue);

            foreach (Aluno a in alunos)
            {
                if (a.Id == id)
                {
                    Console.WriteLine("Já existe aluno com esse ID.");
                    return;
                }
            }

            string nome = LerString("Nome: ");
            alunos.Add(new Aluno(id, nome));
            Console.WriteLine("Aluno cadastrado com sucesso.");
        }

        static void CadastrarCurso(Escola escola)
        {
            Console.WriteLine("Cadastro de Curso");

            int id = LerInteiro("ID do curso: ", 1, int.MaxValue);
            if (escola.pesquisarCurso(new Curso(id, "Temp")) != null)
            {
                Console.WriteLine("Já existe curso com esse ID.");
                return;
            }

            string descricao = LerString("Descrição do curso: ");

            Curso curso = new Curso(id, descricao);
            if (escola.adicionarCurso(curso))
                Console.WriteLine("Curso cadastrado com sucesso.");
            else
                Console.WriteLine("Limite de cursos atingido.");
        }

        static void AdicionarDisciplina(Escola escola)
        {
            Console.WriteLine("Adicionar Disciplina a Curso");

            int idCurso = LerInteiro("ID do curso: ", 1, int.MaxValue);
            Curso curso = escola.pesquisarCurso(new Curso(idCurso, "Temp"));

            if (curso == null)
            {
                Console.WriteLine("Curso não encontrado.");
                return;
            }

            int idDisciplina = LerInteiro("ID da disciplina: ", 1, int.MaxValue);

            if (curso.pesquisarDisciplina(new Disciplina(idDisciplina, "Temp")) != null)
            {
                Console.WriteLine("Já existe disciplina com esse ID neste curso.");
                return;
            }

            string descricao = LerString("Descrição da disciplina: ");

            Disciplina disciplina = new Disciplina(idDisciplina, descricao);
            if (curso.adicionarDisciplina(disciplina))
                Console.WriteLine("Disciplina adicionada com sucesso.");
            else
                Console.WriteLine("Limite de disciplinas do curso atingido.");
        }

        static void MatricularAluno(Escola escola)
        {
            Console.WriteLine("Matricular Aluno em Disciplina");

            int idAluno = LerInteiro("ID do aluno: ", 1, int.MaxValue);
            Aluno aluno = BuscarAluno(idAluno);
            if (aluno == null)
            {
                Console.WriteLine("Aluno não encontrado.");
                return;
            }

            int idCurso = LerInteiro("ID do curso: ", 1, int.MaxValue);
            Curso curso = escola.pesquisarCurso(new Curso(idCurso, "Temp"));
            if (curso == null)
            {
                Console.WriteLine("Curso não encontrado.");
                return;
            }

            int idDisciplina = LerInteiro("ID da disciplina: ", 1, int.MaxValue);
            Disciplina disciplina = curso.pesquisarDisciplina(new Disciplina(idDisciplina, "Temp"));
            if (disciplina == null)
            {
                Console.WriteLine("Disciplina não encontrada neste curso.");
                return;
            }

            Curso cursoAtual = CursoDoAluno(aluno, escola);
            if (cursoAtual != null && cursoAtual.Id != curso.Id)
            {
                Console.WriteLine("Aluno já está matriculado em outro curso.");
                return;
            }

            // Verifica limite de 6 disciplinas
            if (!aluno.podeMatricular(escola.Cursos))
            {
                Console.WriteLine("Aluno já está inscrito em 6 disciplinas ou em situação irregular.");
                return;
            }

            if (disciplina.matricularAluno(aluno))
                Console.WriteLine("Aluno matriculado com sucesso.");
            else
                Console.WriteLine("Não foi possível matricular (disciplina cheia ou já matriculado).");
        }

        static void DesmatricularAluno(Escola escola)
        {
            Console.WriteLine("Desmatricular Aluno de Disciplina");

            int idAluno = LerInteiro("ID do aluno: ", 1, int.MaxValue);
            Aluno aluno = BuscarAluno(idAluno);
            if (aluno == null)
            {
                Console.WriteLine("Aluno não encontrado.");
                return;
            }

            int idCurso = LerInteiro("ID do curso: ", 1, int.MaxValue);
            Curso curso = escola.pesquisarCurso(new Curso(idCurso, "Temp"));
            if (curso == null)
            {
                Console.WriteLine("Curso não encontrado.");
                return;
            }

            int idDisciplina = LerInteiro("ID da disciplina: ", 1, int.MaxValue);
            Disciplina disciplina = curso.pesquisarDisciplina(new Disciplina(idDisciplina, "Temp"));
            if (disciplina == null)
            {
                Console.WriteLine("Disciplina não encontrada neste curso.");
                return;
            }

            if (disciplina.desmatricularAluno(aluno))
                Console.WriteLine("Aluno desmatriculado com sucesso.");
            else
                Console.WriteLine("Aluno não está matriculado nesta disciplina.");
        }

        static void ListarDados(Escola escola)
        {
            Console.WriteLine("=== Alunos Cadastrados ===");
            foreach (Aluno a in alunos)
            {
                Console.WriteLine(a);
            }

            Console.WriteLine("\n=== Cursos e Disciplinas ===");
            foreach (Curso c in escola.Cursos)
            {
                if (c == null) continue;
                Console.WriteLine($"\nCurso: {c}");

                if (c.Disciplinas == null) continue;
                foreach (Disciplina d in c.Disciplinas)
                {
                    if (d == null) continue;
                    Console.WriteLine($"  Disciplina: {d}");
                    Console.WriteLine("    Alunos:");
                    foreach (Aluno a in d.Alunos)
                    {
                        if (a != null)
                            Console.WriteLine($"      {a}");
                    }
                }
            }
        }

        static Aluno BuscarAluno(int id)
        {
            foreach (Aluno a in alunos)
            {
                if (a.Id == id)
                    return a;
            }
            return null;
        }

        static Curso CursoDoAluno(Aluno aluno, Escola escola)
        {
            foreach (Curso c in escola.Cursos)
            {
                if (c == null) continue;
                foreach (Disciplina d in c.Disciplinas)
                {
                    if (d == null) continue;
                    foreach (Aluno a in d.Alunos)
                    {
                        if (a != null && a.Id == aluno.Id)
                            return c;
                    }
                }
            }
            return null;
        }

        static int LerInteiro(string mensagem, int min, int max)
        {
            int valor;
            do
            {
                Console.Write(mensagem);
                if (int.TryParse(Console.ReadLine(), out valor) && valor >= min && valor <= max)
                {
                    return valor;
                }
                Console.WriteLine($"Valor inválido. Deve estar entre {min} e {max}.");
            } while (true);
        }

        static string LerString(string mensagem)
        {
            string valor;
            do
            {
                Console.Write(mensagem);
                valor = Console.ReadLine() ?? "";
                if (!string.IsNullOrWhiteSpace(valor))
                {
                    return valor;
                }
                Console.WriteLine("Valor inválido. Não pode ser vazio.");
            } while (true);
        }
    }
}
