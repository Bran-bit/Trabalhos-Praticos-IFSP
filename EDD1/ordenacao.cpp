#include <iostream>
using namespace std;

void exibirVetor(int v[], int tamanho) {
    for (int i = 0; i < tamanho; i++) {
        cout << v[i] << " ";
    }
    cout << endl;
}

void troca(int &a, int &b) {
    int temp = a;
    a = b;
    b = temp;
}

int particiona(int v[], int inicio, int fim) {
    int pivo = v[fim];
    int i = inicio - 1;  // índice do menor elemento

    for (int j = inicio; j < fim; j++) {
        if (v[j] <= pivo) {
            i++;
            troca(v[i], v[j]);
        }
    }
    troca(v[i + 1], v[fim]);
    return i + 1; // posição final do pivô
}

void quickSort(int v[], int inicio, int fim) {
    if (inicio < fim) {
        int posPivo = particiona(v, inicio, fim);

        quickSort(v, inicio, posPivo - 1); // ordena a esquerda do pivô
        quickSort(v, posPivo + 1, fim);    // ordena a direita do pivô
    }
}

int main() {
    int v[] = {49, 38, 58, 87, 34, 93, 26, 13};
    int tamanho = sizeof(v) / sizeof(v[0]);

    cout << "Vetor antes da ordenacao: ";
    exibirVetor(v, tamanho);

    quickSort(v, 0, tamanho - 1);

    cout << "Vetor depois da ordenacao: ";
    exibirVetor(v, tamanho);

    return 0;
}
