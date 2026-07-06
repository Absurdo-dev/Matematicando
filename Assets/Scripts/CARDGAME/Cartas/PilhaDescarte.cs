using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PilhaDescarte : MonoBehaviour
{
    [SerializeField] private List<DadosCartaSO> pilhaDescarte = new List<DadosCartaSO>();
    [SerializeField] private GameObject prefabCarta;
    [SerializeField] private Baralho baralho;
    private const float OFFSET = 0.25f;

    public void DescartarCarta(DadosCartaSO dadosCarta) {
        pilhaDescarte.Add(dadosCarta);

        GameObject cartaDescartada = Instantiate(prefabCarta, transform);

        cartaDescartada.GetComponent<Carta>().CarregarDadosCarta(dadosCarta);

        cartaDescartada.GetComponent<Carta>().DefinirInteragir(false);

        SortingGroup sortingGroup = cartaDescartada.GetComponent<SortingGroup>();
        sortingGroup.sortingOrder = pilhaDescarte.Count - 1;

        cartaDescartada.transform.SetParent(transform);

        cartaDescartada.transform.localPosition = new Vector3(0f, (pilhaDescarte.Count - 1) * - OFFSET, 0f); //offset de posição
    }

    public void MoverCartasParaBaralho(List<DadosCartaSO> pilhaSaque) {
        if (pilhaSaque == null || pilhaDescarte.Count == 0) {
            return;
        }
        pilhaSaque.AddRange(pilhaDescarte);
        LimparPilha();
    }

    private void LimparPilha() {
        pilhaDescarte.Clear();
        foreach (Transform cartaDescartada in transform)
        {
            Destroy(cartaDescartada.gameObject);
        }
    }

    private void OnMouseDown()
    {
        if (!SistemaDeTurnos.Instance.TemAcoes() || pilhaDescarte.Count <= 0) {
            return;
        }
        EventosJogador.TentarReembaralhar();
        baralho.ReembaralharDaPilhaDeDescarte();
    }
}
