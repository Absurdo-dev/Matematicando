using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class Carta : MonoBehaviour
{   //Informações passadas pra representação visual
    [Header("Dados visuais da carta")]
    [SerializeField] private SpriteRenderer imagemRenderer;
    [SerializeField] private TextMeshPro nomeCartaTexto;
    [SerializeField] private TextMeshPro descricaoTexto;
    [SerializeField] private TextMeshPro custoTexto;

    [Header("Movimentação da carta")]
    private Vector3 escalaOriginal;
    private Vector3 posOriginal;
    [SerializeField] private float escalaMouseEnter = 1.5f;
    [SerializeField] private float escalaMouseEnterOffset = 1.5f;
    private int sortingOrderOriginal;
    private static bool cartaSendoCarregada = false; // todas as outras cartas conseguem ver que está sendo carregada

    private DadosCartaSO dadosCarta;
    private Collider2D colliderCarta;
    private SortingGroup sortingGroup;

    private bool sendoJogada = false;

    [SerializeField] private float duracaoBrilho = .5f;

    [SerializeField] private SpriteRenderer overlayDoBrilho;
    private void Awake()
    {
        sortingGroup = GetComponent<SortingGroup>();
        colliderCarta = GetComponent<Collider2D>();
    }
    private void Start()
    {
        escalaOriginal = transform.localScale; // Escala atual do objeto
        posOriginal = transform.localPosition; // Pos atual do objeto
        sortingOrderOriginal = sortingGroup.sortingOrder; // Ordem atual do objeto
    }
    //Preenche os dados visuais da carta + o valor de DadosCata
    public void CarregarDadosCarta(DadosCartaSO dadosCarta) {
        this.dadosCarta = dadosCarta;
        imagemRenderer.sprite = dadosCarta.imagem;
        nomeCartaTexto.text = dadosCarta.nomeCarta;
        descricaoTexto.text = dadosCarta.descricao;
        custoTexto.text = dadosCarta.custo.ToString();
    }
    //Função para quando o mouse passa em cima
    private void OnMouseEnter()
    {   // se qualquer carta tiver sendo carregada, não funciona
        if (cartaSendoCarregada)
        {
            return;
        }
        transform.localScale = escalaOriginal * escalaMouseEnter; // Aumenta o tamanho da carta
        transform.localPosition += new Vector3(0, escalaMouseEnterOffset, 0); // Eleva a carta no eixo y
        sortingGroup.sortingOrder += 1; // Eleva a carta na sorting order
    }
    // Quando o mouse sai de cima
    private void OnMouseExit()
    {   //Se qualquer carta estiver sendo carregada, não funciona
        if (cartaSendoCarregada) {
            return;
        }
        transform.localScale = escalaOriginal; // retorna a escala original quando o mouse sai
        transform.localPosition = posOriginal; // retorna à pos original quando o mouse sia
        sortingGroup.sortingOrder = sortingOrderOriginal; // retorna a sorting order original
    }

    private void OnMouseDrag()
    {   // Carrega a carta de acordo com a direção do mouse
        cartaSendoCarregada = true;
        gameObject.transform.position = BuscarPosMouse();
    }

    private Vector3 BuscarPosMouse() { //Função que retorna a posição do mouse
        Vector3 posMouse = Mouse.current.position.ReadValue(); // Lê o valor da pos do mouse e transforma num vector3
        // Define a distância entre a câmera e o objeto no eixo Z,
        // necessária para converter a posição da tela para o mundo
        posMouse.z = transform.position.z - Camera.main.transform.position.z;
        // Converte a posição da tela para uma posição no mundo do jogo
        return Camera.main.ScreenToWorldPoint(posMouse);
    }

    public void DefinirSendoJogado(bool sendoJogada) { 
        this.sendoJogada = sendoJogada;
    }

    private void OnMouseUp()
    {   //Quando o jogador para de clicar, solta a carta
        cartaSendoCarregada = false;
        if (sendoJogada) {
            return;
        }
        transform.localScale = escalaOriginal; // Retorna para as proporções originais
        transform.localPosition = posOriginal;
        sortingGroup.sortingOrder = sortingOrderOriginal;
    }

    public DadosCartaSO BuscarDadosCarta() => dadosCarta; // Getter dos dados da carta

    private void OnDestroy() //Se a carta é destruída, é transformado em false
    {
        cartaSendoCarregada = false;
    }

    public void DefinirInteragir(bool interagir) { //Liga e desliga o objeto carta
        colliderCarta.enabled = interagir;
    }

    public void Brilhar() {
        StartCoroutine(BrilharCO());
    }

    private IEnumerator BrilharCO() {
        overlayDoBrilho.gameObject.SetActive(true);
        yield return new WaitForSeconds(duracaoBrilho);
        overlayDoBrilho.gameObject.SetActive(false);
    }
}
