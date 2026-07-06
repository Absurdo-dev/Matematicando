using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class ColecaoCartas : MonoBehaviour
{
    [SerializeField] private List<DadosCartaSO> cartasDisponiveis;

    [SerializeField] private Transform[] slotCarta;

    [SerializeField] private GameObject prefabCarta;

    private void Start()
    {
        for (int i = 0; i < cartasDisponiveis.Count; i++)
        {
            AdicionarCartaNaColecao(i);
        }
    }

    private void AdicionarCartaNaColecao(int idCarta) { 
        GameObject carta = Instantiate(prefabCarta, slotCarta[idCarta].position, Quaternion.identity);
        Carta componenteCarta = carta.GetComponent<Carta>();
        componenteCarta.CarregarDadosCarta(cartasDisponiveis[idCarta]);
        carta.transform.SetParent(slotCarta[idCarta].transform);
    }
}
