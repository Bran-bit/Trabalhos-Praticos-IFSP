#include <iostream>
using namespace std;

#define MAX 100

struct FilaVetor {
    int ini;
    int fim;
    int nos[MAX];
};

FilaVetor* initVetor() {
    FilaVetor *f = new FilaVetor;
    f->ini = 0;
    f->fim = 0;
    return f;
}

int isEmptyVetor(FilaVetor *f) {
    return (f->ini == f->fim);
}

int incrementa(int i) {
    return (i == MAX - 1 ? 0 : i + 1);
}

int countVetor(FilaVetor *f) {
    int k = 0;
    int i = f->ini;
    while (i != f->fim) {
        k++;
        i = incrementa(i);
    }
    return k;
}

int enqueueVetor(FilaVetor *f, int v) {
    int pode = (incrementa(f->fim) != f->ini);
    if (pode) {
        f->nos[f->fim] = v;
        f->fim = incrementa(f->fim);
    }
    return pode;
}

int dequeueVetor(FilaVetor *f) {
    int ret = -1;
    if (!isEmptyVetor(f)) {
        ret = f->nos[f->ini];
        f->ini = incrementa(f->ini);
    }
    return ret;
}

void freeVetor(FilaVetor *f) {
    delete f;
}


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

void freeLista(FilaLista *f) {
    No *no = f->ini;
    while (no != NULL) {
        No *temp = no->prox;
        delete no;
        no = temp;
    }
    delete f;
}


int main() {
    FilaVetor *senhasGeradas = initVetor();
    FilaLista *senhasAtendidas = initLista();

    int proximaSenha = 1;
    int opcao;

    do {
        cout << "\nSenhas aguardando: " << countVetor(senhasGeradas) << endl;
        cout << "0. Sair\n1. Gerar senha\n2. Realizar atendimento\nOpção: ";
        cin >> opcao;

        switch (opcao) {
            case 0:
                if (!isEmptyVetor(senhasGeradas)) {
                    cout << "Ainda há senhas aguardando. Impossível sair.\n";
                    opcao = -1; // força continuar no loop
                }
                break;

            case 1:
                if (!enqueueVetor(senhasGeradas, proximaSenha)) {
                    cout << "Fila de espera cheia (limite: " << MAX << ").\n";
                } else {
                    cout << "Senha gerada: " << proximaSenha << endl;
                    proximaSenha++;
                }
                break;

            case 2:
                if (isEmptyVetor(senhasGeradas)) {
                    cout << "Nenhuma senha aguardando.\n";
                } else {
                    int atendida = dequeueVetor(senhasGeradas);
                    cout << "Atendendo senha: " << atendida << endl;
                    enqueueLista(senhasAtendidas, atendida);
                }
                break;

            default:
                cout << "Opção inválida.\n";
        }
    } while (opcao != 0);

    int totalAtendidas = countLista(senhasAtendidas);
    cout << "\nTotal de senhas atendidas: " << totalAtendidas << endl;

    freeVetor(senhasGeradas);
    freeLista(senhasAtendidas);
    return 0;
}
