using TMPro;
using UnityEngine;

public class AbaCarta : MonoBehaviour
{
    [SerializeField] private SpriteRenderer baseCarta;
    [SerializeField] private SpriteRenderer imagem;
    [SerializeField] private TextMeshPro nomeCartaTXT;
    [SerializeField] private TextMeshPro acaoTXT;

    private DadosCartaSO dadosCarta;

    private Color corOriginal;

    private void Start()
    {
        corOriginal = baseCarta.color;
    }

    public void CarregarDadosAbaCarta(DadosCartaSO dadosCarta) {
        this.dadosCarta = dadosCarta;
        imagem.sprite = dadosCarta.imagem;
        nomeCartaTXT.text = dadosCarta.nomeCarta;
        acaoTXT.text = dadosCarta.custo.ToString();
    }

    private void OnMouseDown()
    {
        EventosBaralho.RemoverCartaDoDeck(dadosCarta);
    }

    private void OnMouseEnter()
    {
        baseCarta.color = Color.yellowGreen;
    }

    private void OnMouseExit()
    {
        baseCarta.color = corOriginal;
    }
}
