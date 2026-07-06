using System.Collections.Generic;
using UnityEngine;

public class Baralho : MonoBehaviour
{   //Lista de "cartas"
    [SerializeField] private List<DadosCartaSO> pilhaSaque = new List<DadosCartaSO>();
    //Objeto para fundo do baralho
    [SerializeField] private GameObject costasCarta;
    [SerializeField] private PilhaDescarte pilhaDescarte;

    private const float OFFSET = 0.1f;
    private void Awake()
    {
        pilhaSaque = GerenteDeBaralho.Instance.BuscarBaralho();
        Embaralhar();
    }
    private void Start()
    {
        VisualSaqueBaralho();
    }

    public DadosCartaSO SacarCarta() {
        if (pilhaSaque.Count > 0) {
            int idTopo = pilhaSaque.Count - 1;
            DadosCartaSO dados = pilhaSaque[idTopo];
            pilhaSaque.RemoveAt(idTopo);
            VisualSaqueBaralho();
            return dados;
        }
        //if (pilhaSaque.Count <= 0) {
        //    return null;
        //}
        return null;
    }

    public void ReembaralharDaPilhaDeDescarte() {
        pilhaDescarte.MoverCartasParaBaralho(pilhaSaque);
        Embaralhar();
        VisualSaqueBaralho();
    }

    private void VisualSaqueBaralho() {
        foreach (Transform cartaDescartada in transform) 
        {
            Destroy(cartaDescartada.gameObject);
        }
        for (int i = 0; i < pilhaSaque.Count; i++) //Percorre toda lista de saque
        {
            GameObject novaCostasCarta = Instantiate(costasCarta, transform); //Instancia uma nova carta na pos do baralho
            novaCostasCarta.GetComponent<SpriteRenderer>().sortingOrder = i; //Ajeita a sorting Order
            novaCostasCarta.transform.localPosition = new Vector3(-i * OFFSET,-i * OFFSET,0f); //offset de posição
        }
    }

    public void Embaralhar(){
        for (int i = 0; i < pilhaSaque.Count; i++) //Percorre toda a pilha de saque
        { //Basicamente fica trocando a posição da carta atual com uma carta aleatória
            DadosCartaSO carta = pilhaSaque[i]; //Pega a carta atual
            int idRandom = Random.Range(i, pilhaSaque.Count); //Escolhe um número aleatório entre o número atual e o total de cartas
            pilhaSaque[i] = pilhaSaque[idRandom]; //Pos atual recebe a carta aleatória
            pilhaSaque[idRandom] = carta; //Pos aleatória rebece a carta atual
        }

    }

    private void OnMouseDown()
    {   //Ao clicar
        if (pilhaSaque.Count <= 0) {
            return; //Se não houverem cartas para sacar, nada acontece
        }
        if (SistemaDeTurnos.Instance.TemAcoes()) { // Verifica no sistema de turnos se ainda tem ações
            EventosJogador.TentarComprarCarta(); // Usa eventosJogador para tentar comprar carta
            //EventosJogador é uma classe static 
        }
    }
}
