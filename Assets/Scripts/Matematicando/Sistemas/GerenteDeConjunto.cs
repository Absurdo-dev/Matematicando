using System.Collections.Generic;
using UnityEngine;

public class GerenteDeConjunto : Singleton<GerenteDeConjunto>
{   //Lista de conjunto de cestos atual
    private List<DadosCestoSO> conjuntoAtual = new List<DadosCestoSO>();
    //Tamanho máximo da "mão" do jogador
    [SerializeField] private int tamanhoMaxConjunto = 5;
    //Conjunto base
    [SerializeField] private ConjuntoInicialSO conjuntoInicial;

    protected override void Awake()
    {
        base.Awake(); // Necessária a criação da instância com base no Singleton
        DontDestroyOnLoad(gameObject); // O baralho não altera de uma cena para outra
        conjuntoAtual = new List<DadosCestoSO>(conjuntoInicial.Cestos); // Todo início de jogo ó baralho inicial é criado
    }

    private void OnEnable()
    {   //Eventos que recebem funções desse código
        EventosConjunto.AoAdicionarCestoDoConjunto += AdicionarCesto;
        EventosConjunto.AoRemoverCestoDoConjunto += RemoverCesto;
        
    }
    private void OnDisable()
    {
        EventosConjunto.AoAdicionarCestoDoConjunto -= AdicionarCesto;
        EventosConjunto.AoRemoverCestoDoConjunto -= RemoverCesto;
    }
    //Adiciona um cesto no conjunto atual
    private void AdicionarCesto(DadosCestoSO cesto) {
        //Se o limite de cestos por conjunto já tiver sido atingido, não é adicionado novo cesto
        if (conjuntoAtual.Count >= tamanhoMaxConjunto) {
            Debug.Log("Conjunto cheio!");
            return;
        }
        //Adiciona Cesto no conjunto
        conjuntoAtual.Add(cesto);
        EventosConjunto.ProcessarConjunto(); //Necessário para atualização da UI
    }

    private void RemoverCesto(DadosCestoSO cesto) { 
        conjuntoAtual.Remove(cesto); //Remove cesto no conjunto
        EventosConjunto.ProcessarConjunto(); //Necessário para atualização da UI
    }

    public List<DadosCestoSO> BuscarConjunto() {
        return new List<DadosCestoSO>(conjuntoAtual); //Getter dos conjuntos atuais
    }
}
