#include <iostream>
using namespace std;

struct No {
    int dado;
    No *prox;
};

struct FilaLista {
    No *ini;
    No *fim;
};

FilaLista* initLista() {
    FilaLista *f = new FilaLista;
    f->ini = NULL;
    f->fim = NULL;
    return f;
}

int isEmptyLista(FilaLista *f) {
    return (f->ini == NULL);
}

int countLista(FilaLista *f) {
    int k = 0;
    No *no = f->ini;
    while (no != NULL) {
        k++;
        no = no->prox;
    }
    return k;
}

void enqueueLista(FilaLista *f, int v) {
    No *no = new No;
    no->dado = v;
    no->prox = NULL;
    if (isEmptyLista(f)) {
        f->ini = no;
    } else {
        f->fim->prox = no;
    }
    f->fim = no;
}

int dequeueLista(FilaLista *f) {
    int ret = -1;
    if (!isEmptyLista(f)) {
        No *no = f->ini;
        ret = no->dado;
        f->ini = no->prox;
        if (f->ini == NULL) f->fim = NULL;
        delete no;
    }
    return ret;
}

void printLista(FilaLista *f) {
    No *no = f->ini;
    while (no != NULL) {
        cout << no->dado;
        if (no->prox != NULL) cout << ", ";
        no = no->prox;
    }
    cout << endl;
}

void freeLista(FilaLista *f) {
    No *no = f->ini;
    while (no != NULL) {
        No *temp = no->prox;
        delete no;
        no = temp;
    }
    delete f;
}

struct Guiche {
    int id;
    FilaLista *senhasAtendidas;
    Guiche *prox;
};

Guiche* abrirGuiche(Guiche *lista, int id) {
    Guiche *novo = new Guiche;
    novo->id = id;
    novo->senhasAtendidas = initLista();
    novo->prox = lista;
    return novo;
}

Guiche* buscarGuiche(Guiche *lista, int id) {
    Guiche *g = lista;
    while (g != NULL) {
        if (g->id == id)
            return g;
        g = g->prox;
    }
    return NULL;
}

int contarGuiches(Guiche *lista) {
    int cont = 0;
    Guiche *g = lista;
    while (g != NULL) {
        cont++;
        g = g->prox;
    }
    return cont;
}

void freeGuiches(Guiche *&lista) {
    Guiche *g = lista;
    while (g != NULL) {
        Guiche *temp = g->prox;
        freeLista(g->senhasAtendidas);
        delete g;
        g = temp;
    }
    lista = NULL;
}

int main() {
    FilaLista *senhasGeradas = initLista(); // agora usando lista ligada (ponteiros)
    Guiche *guiches = NULL;

    int proximaSenha = 1;
    int opcao;

    do {
        cout << "\nSenhas aguardando: " << countLista(senhasGeradas);
        cout << " | Guiches abertos: " << contarGuiches(guiches) << endl;

        cout << "0. Sair\n";
        cout << "1. Gerar senha\n";
        cout << "2. Abrir guiche\n";
        cout << "3. Realizar atendimento\n";
        cout << "4. Listar senhas atendidas\n";
        cout << "Opcao: ";
        cin >> opcao;

        switch (opcao) {
            case 0:
                if (!isEmptyLista(senhasGeradas)) {
                    cout << "Ainda ha senhas aguardando. Impossivel sair.\n";
                    opcao = -1;
                }
                break;

            case 1:
                enqueueLista(senhasGeradas, proximaSenha);
                cout << "Senha gerada: " << proximaSenha << endl;
                proximaSenha++;
                break;

            case 2: {
                int idGuiche;
                cout << "ID do novo guiche: ";
                cin >> idGuiche;
                if (buscarGuiche(guiches, idGuiche) != NULL) {
                    cout << "Guiche " << idGuiche << " ja esta aberto.\n";
                } else {
                    guiches = abrirGuiche(guiches, idGuiche);
                    cout << "Guiche " << idGuiche << " aberto com sucesso.\n";
                }
                break;
            }

            case 3: {
                if (isEmptyLista(senhasGeradas)) {
                    cout << "Nenhuma senha aguardando.\n";
                } else if (contarGuiches(guiches) == 0) {
                    cout << "Nenhum guiche aberto. Abra um guiche antes de realizar atendimento.\n";
                } else {
                    int idGuiche;
                    cout << "ID do guiche que realizara o atendimento: ";
                    cin >> idGuiche;
                    Guiche *g = buscarGuiche(guiches, idGuiche);
                    if (g == NULL) {
                        cout << "Guiche " << idGuiche << " nao encontrado.\n";
                    } else {
                        int senha = dequeueLista(senhasGeradas);
                        enqueueLista(g->senhasAtendidas, senha);
                        cout << "Atendendo senha " << senha << " no guiche " << idGuiche << ".\n";
                    }
                }
                break;
            }

            case 4: {
                if (contarGuiches(guiches) == 0) {
                    cout << "Nenhum guiche aberto.\n";
                } else {
                    int idGuiche;
                    cout << "ID do guiche para listar atendimentos: ";
                    cin >> idGuiche;
                    Guiche *g = buscarGuiche(guiches, idGuiche);
                    if (g == NULL) {
                        cout << "Guiche " << idGuiche << " nao encontrado.\n";
                    } else if (isEmptyLista(g->senhasAtendidas)) {
                        cout << "Nenhuma senha atendida pelo guiche " << idGuiche << " ainda.\n";
                    } else {
                        cout << "Senhas atendidas pelo guiche " << idGuiche << ": ";
                        printLista(g->senhasAtendidas);
                    }
                }
                break;
            }

            default:
                cout << "Opcao invalida.\n";
        }
    } while (opcao != 0);

    // Contabiliza total de senhas atendidas
    int totalAtendidas = 0;
    Guiche *g = guiches;
    while (g != NULL) {
        totalAtendidas += countLista(g->senhasAtendidas);
        g = g->prox;
    }

    cout << "\nTotal de senhas atendidas: " << totalAtendidas << endl;

    freeLista(senhasGeradas);
    freeGuiches(guiches);

    return 0;
}
