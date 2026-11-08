using System.Collections.Generic;
using System.Linq;
using MedicamentosConsole.Models;

namespace MedicamentosConsole.Services;

public class Medicamentos
{
    private readonly List<Medicamento> listaMedicamentos = new();

    public IReadOnlyList<Medicamento> ListaMedicamentos => listaMedicamentos;

    public Medicamentos() { }

    public void Adicionar(Medicamento medicamento)
    {
        if (medicamento is null) return;

        listaMedicamentos.Add(medicamento);
    }

    public bool Deletar(Medicamento medicamento)
    {
        if (medicamento is null) return false;

        var existente = listaMedicamentos.FirstOrDefault(m => m.Equals(medicamento));
        if (existente is null) return false;

        if (existente.QtdeDisponivel() != 0) return false;

        return listaMedicamentos.Remove(existente);
    }

    public Medicamento Pesquisar(Medicamento medicamento)
    {
        if (medicamento is null) return new Medicamento();

        return listaMedicamentos.FirstOrDefault(m => m.Equals(medicamento)) ?? new Medicamento();
    }
}
