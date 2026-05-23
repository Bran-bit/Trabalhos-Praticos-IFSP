#include <iostream>
#include <climits>
using namespace std;

#define MAX 30

struct PilhaVetor {
    int qtde;
    int elementos[MAX];
};

PilhaVetor* initVetor() {
    PilhaVetor *p = new PilhaVetor();
    p->qtde = 0;
    return p;
}

int isEmptyVetor(PilhaVetor *p) {
    return (p->qtde == 0);
}

int isFullVetor(PilhaVetor *p) {
    return (p->qtde == MAX);
}

int pushVetor(PilhaVetor *p, int v) {
    if (isFullVetor(p)) return 0;
    p->elementos[p->qtde++] = v;
    return 1;
}

// Retorna true e preenche 'out' com o valor desempilhado.
// Retorna false se a pilha estiver vazia.
bool popVetor(PilhaVetor *p, int &out) {
    if (isEmptyVetor(p)) return false;
    out = p->elementos[p->qtde - 1];
    p->qtde--;
    return true;
}

void freeVetor(PilhaVetor *p) {
    delete p;
}

// Pilha em lista encadeada
struct No {
    int dado;
    No *ant;
};

struct PilhaLista {
    No *topo;
};

PilhaLista* initLista() {
    PilhaLista *p = new PilhaLista;
    p->topo = NULL;
    return p;
}

int isEmptyLista(PilhaLista *p) {
    return (p->topo == NULL);
}

void pushLista(PilhaLista *p, int v) {
    No *no = new No;
    no->dado = v;
    no->ant = p->topo;
    p->topo = no;
}

// Retorna true e preenche 'out' com o valor desempilhado.
// Retorna false se a pilha estiver vazia.
bool popLista(PilhaLista *p, int &out) {
    if (isEmptyLista(p)) return false;
    No *no = p->topo;
    out = no->dado;
    p->topo = no->ant;
    delete no;
    return true;
}

void freeLista(PilhaLista *p) {
    No *no = p->topo;
    while (no != NULL) {
        No *temp = no->ant;
        delete no;
        no = temp;
    }
    delete p;
}


int main() {
    // Pares → pilha em vetor | Ímpares → pilha em lista encadeada
    PilhaVetor *pilhaPar   = initVetor();
    PilhaLista *pilhaImpar = initLista();

    int numero, anterior = 0;
    bool primeiro = true;

    cout << "Digite 30 numeros inteiros em ordem crescente:\n";

    for (int i = 0; i < 30; ++i) {
        while (true) {
            cout << "Numero " << (i + 1) << ": ";
            cin >> numero;

            if (primeiro) {
                // Primeiro número aceito sem comparação
                anterior = numero;
                primeiro = false;
                break;
            } else if (numero > anterior) {
                anterior = numero;
                break;
            } else {
                cout << "Erro: digite um numero maior que " << anterior << "\n";
            }
        }

        if (numero % 2 == 0) {
            pushVetor(pilhaPar, numero);   // par   → vetor
        } else {
            pushLista(pilhaImpar, numero); // impar → lista
        }
    }

    cout << "\n=== VALORES DESEMPILHADOS (ordem decrescente) ===\n";

    while (!isEmptyVetor(pilhaPar) || !isEmptyLista(pilhaImpar)) {
        int topoPar   = !isEmptyVetor(pilhaPar)  ? pilhaPar->elementos[pilhaPar->qtde - 1] : INT_MIN;
        int topoImpar = !isEmptyLista(pilhaImpar) ? pilhaImpar->topo->dado                  : INT_MIN;

        int val;
        if (topoPar >= topoImpar) {
            popVetor(pilhaPar, val);
        } else {
            popLista(pilhaImpar, val);
        }
        cout << val << "\n";
    }

    freeVetor(pilhaPar);
    freeLista(pilhaImpar);

    return 0;
}
