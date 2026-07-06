using System.Collections.Generic;
using UnityEngine;

public class GerenteDeBaralho : Singleton<GerenteDeBaralho>
{
    private List<DadosCartaSO> baralhoAtual = new List<DadosCartaSO>();
    [SerializeField] private int tamanhoMaxBaralho = 9;
    [SerializeField] private BaralhoInicialSO baralhoInicial;

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);

        baralhoAtual = new List<DadosCartaSO>(baralhoInicial.cartas);
    }

    private void OnEnable()
    {
        EventosBaralho.AoAdicionarCartaNoBaralho += AdicionarCarta;
        EventosBaralho.AoRemoverCartaDoBaralho += RemoverCarta;
    }

    private void OnDisable()
    {
        EventosBaralho.AoAdicionarCartaNoBaralho -= AdicionarCarta;
        EventosBaralho.AoRemoverCartaDoBaralho -= RemoverCarta;
    }

    private void AdicionarCarta(DadosCartaSO carta) {
        if (baralhoAtual.Count >= tamanhoMaxBaralho) {
            Debug.Log("Baralho Cheio!");
            return;
        }

        baralhoAtual.Add(carta);
        EventosBaralho.ProcessarBaralho();
    }

    private void RemoverCarta(DadosCartaSO carta) { 
        baralhoAtual.Remove(carta);
        EventosBaralho.ProcessarBaralho();
    }

    public List<DadosCartaSO> BuscarBaralho() { 
        return new List<DadosCartaSO>(baralhoAtual);
    }
}
