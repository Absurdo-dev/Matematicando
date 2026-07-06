using System.Collections;
using UnityEngine;

public class SistemaDeRodadas : Singleton<SistemaDeRodadas>
{
    private enum EstadoRodada { RodadaAjudante, RodadaVendedor } // Define quais estados de rodada existem

    private EstadoRodada rodadaAtual = EstadoRodada.RodadaVendedor;

    [SerializeField] private int tempoEsperaTurno = 2; // Tempo de espera entre um turno e outro.
    [SerializeField] private float tempoDelayVendedor = 2f;

    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        IniciarRodadaVendedor();
    }

    private void OnEnable()
    {
        EventosAjudante.AoJogarCesto += CestoJogado;
    }

    private void OnDisable()
    {
        EventosAjudante.AoJogarCesto -= CestoJogado;
    }

    /// JOGADOR //////////////////////////////////////////////////////////////
    private void CestoJogado(DadosCestoSO dadosCesto) { //Chamado quando o ajudante usa um cesto 
        
    }
    private void IniciarRodadaAjudante() {
        Debug.Log("Iniciar rodada ajudante");
        rodadaAtual = EstadoRodada.RodadaAjudante;
        EventosRodada.IniciarTurnoAjudante();
    }
    private void FinalizarRodadaAjudante() {
        Debug.Log("Finalizar rodada ajudante");
        EventosRodada.FinalizarTurnoAjudante();
    }
    /// JOGADOR //////////////////////////////////////////////////////////////

    /// VENDEDOR //////////////////////////////////////////////////////////////
    private void RodadaVendedor() {
        EventosRodada.IniciarTurnoVendedor();
        StartCoroutine(FinalizarRodadaVendedor());

    }
    private IEnumerator IniciarRodadaVendedor() {
        Debug.Log("Iniciar rodada vendedor");
        rodadaAtual = EstadoRodada.RodadaVendedor;
        yield return new WaitForSeconds(tempoDelayVendedor);
        RodadaVendedor();
    }
    private IEnumerator FinalizarRodadaVendedor() {
        Debug.Log("Finalizar rodada Vendedor");
        EventosRodada.FinalizarTurnoVendedor();
        yield return new WaitForSeconds(tempoDelayVendedor);
    }
    /// VENDEDOR //////////////////////////////////////////////////////////////
}
