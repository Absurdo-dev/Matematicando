using System.Collections.Generic;
using UnityEngine;

public class ConjuntoCestos : MonoBehaviour
{   //Conjunto de "Cestos"
    [SerializeField] private List<DadosCestoSO> pilhaSaque = new List<DadosCestoSO>();

    private void Awake()
    {
        pilhaSaque = GerenteDeConjunto.Instance.BuscarConjunto();
        Embaralhar();
    }

    public DadosCestoSO SacarCesto() {
        if (pilhaSaque.Count > 0) { // se a pilha de saque tiver cestos
            int idTopo = pilhaSaque.Count - 1; // como a lista começa em 0 o ID do topo é o total -1
            DadosCestoSO dados = pilhaSaque[idTopo]; // recebe os dados do cesto no topo
            pilhaSaque.RemoveAt(idTopo); // remove o cesto do topo
            return dados;
        }
        return null;
    }

    public void Embaralhar() {
        for (int i = 0; i < pilhaSaque.Count; i++) // Percorre toda a pilha de saque
        { // Basicamente fica trocando a posição do cesto atual com um cesto aleatório
            DadosCestoSO cesto = pilhaSaque[i]; //Pega a cesto atual
            int idRandom = Random.Range(i, pilhaSaque.Count); // Escolhe um número aleatório entre o numero atual e o total de cestos
            pilhaSaque[i] = pilhaSaque[idRandom]; // Pos atual recebe o cesto aleatório
            pilhaSaque[idRandom] = cesto; // pos aleatória recebe o cesto atual
        }
    }
}
