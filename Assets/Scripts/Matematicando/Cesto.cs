using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class Cesto : MonoBehaviour
{
    [Header("Dados visuais do Cesto")]
    [SerializeField] private SpriteRenderer imagemFrutas;
    [SerializeField] private TextMeshPro nomeCestaTexto;
    [SerializeField] private TextMeshPro valorCestoTexto;

    [Header("Movimentação do Cesto")]
    private Vector3 escalaOriginal;
    private Vector3 posOriginal;
    [SerializeField] private float escalaMouseEnter = 1.5f;
    [SerializeField] private float escalaMouseEnterOffset = 1.5f;
    private int ordemNoGrupoOriginal;
    private static bool cestoSendoCarregado = false; // Todos os outros cestos conseguem ver que está sendo carregado

    private DadosCestoSO dadosCesto;
    private Collider2D colliderCesto;
    private SortingGroup ordemNoGrupo;

    private bool sendoJogado = false;

    [SerializeField] private float duracaoBrilho = .5f;
    [SerializeField] private SpriteRenderer overlayDoBrilho;

    private void Awake()
    {
        ordemNoGrupo = GetComponent<SortingGroup>();   
        colliderCesto = GetComponent<Collider2D>();
    }

    private void Start()
    {
        escalaOriginal = transform.localScale; // Escala atual do objeto
        posOriginal = transform.localPosition; // Pos atual do objeto
        ordemNoGrupoOriginal = ordemNoGrupo.sortingOrder; // Ordem atual do objeto
    }

    //Função para preencher os valores visuais do cesto
    private void CarregarDadosCesto(DadosCestoSO dadosCesto) {
        this.dadosCesto = dadosCesto;
        imagemFrutas.sprite = dadosCesto.imagem;
        nomeCestaTexto.text = dadosCesto.nomeCesto;
        valorCestoTexto.text = dadosCesto.valor.ToString();
    }

    private void OnMouseEnter()
    {   //Garante que se o cesto estiver sendo carregado, outros cestos não podem ser acessados
        if (cestoSendoCarregado) {
            return;
        }
        transform.localScale = escalaOriginal * escalaMouseEnter; // AUmenta o tamanho do cesto quando o mouse entra
        transform.localPosition += new Vector3(0, escalaMouseEnterOffset, 0); // Eleva o cesto no eixo y
        ordemNoGrupo.sortingOrder += 1; //Eleva o cesto na ordem
    }

    private void OnMouseExit()
    {   //Garante que se o cesto estiver sendo carregado, outros cestos não podem ser acessados
        if (cestoSendoCarregado)
        {
            return;
        }
        transform.localScale = escalaOriginal; //Retorna à escala original quando o mouse sai
        transform.localPosition = posOriginal; //Retorna à pos original quando o mouse sai
        ordemNoGrupo.sortingOrder = ordemNoGrupoOriginal; // Retorna a sorting order originak
    }

    private void OnMouseDrag()
    {   //Carrega a carta de acordo com a direção do mouse
        cestoSendoCarregado = true;
        gameObject.transform.position = BuscarPosMouse();
    }

    private Vector3 BuscarPosMouse() { //função que retorna a pos do mouse
        Vector3 posMouse = Mouse.current.position.ReadValue(); //Lê o valor da pos do mouse e transforma num Vector3
        //Define a distância entre a câmera e o objeto no eixo Z,
        //Necessária para converter a pos da tela para o mundo
        posMouse.z = transform.position.z - Camera.main.transform.position.z;
        return Camera.main.ScreenToWorldPoint(posMouse);
    }
    //Setter pra sendo jogado
    public void DefinirSendoJogado(bool sendoJogado) { 
        this.sendoJogado = sendoJogado;
    }

    private void OnMouseUp()
    {   //Quando o jogador para de clicar, solta o cesto
        cestoSendoCarregado &= false;
        if (sendoJogado) { //Garante que se o cesto estiver sendo jogado, outros cestos não podem ser acessados
            return;
        }
        transform.localScale = escalaOriginal; //Retorna para as proporções originais
        transform.localPosition = posOriginal; 
        ordemNoGrupo.sortingOrder = ordemNoGrupoOriginal;
    }

    public DadosCestoSO BuscarDadosCesto() => dadosCesto; // Getter dos dados do cesto

    private void OnDestroy()
    {
        cestoSendoCarregado = false;
    }

    public void DefinirInteragir(bool interagir) { //Liga e desliga o objeto cesto
        colliderCesto.enabled = interagir;
    }

    public void Brilhar() { // Função que ativa SFX de quando o cesto é usado
        StartCoroutine(BrilharCO());
    }

    private IEnumerator BrilharCO() {
        overlayDoBrilho.gameObject.SetActive(true);
        yield return new WaitForSeconds(duracaoBrilho);
        overlayDoBrilho.gameObject.SetActive(false);
    }
}
