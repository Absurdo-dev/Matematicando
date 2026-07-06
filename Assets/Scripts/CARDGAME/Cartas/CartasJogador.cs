using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartasJogador : MonoBehaviour
{
    [SerializeField] private Baralho baralho;
    [SerializeField] private Transform[] slotsCarta;
    [SerializeField] private GameObject prefabCarta;
    [SerializeField] private int quantidadeCartasInicial = 2;

    [SerializeField] private PilhaDescarte pilhaDescarte;
    [SerializeField] private float atrasoCarta = .3f;

    [SerializeField] private ParticleSystem prefabJogarCartaVFX;

    private List<Carta> cartasComOJogador = new List<Carta>();

    private void Start()
    {
        for (int i = 0; i < quantidadeCartasInicial; i++)
        {
            ComprarCarta();
        }
    }

    private void OnEnable()
    {
        EventosTurno.AoFimDoTurnoJogador += DesabilitarJogada;
        EventosTurno.AoInicioTurnoJogador += HabilitarJogada;
        EventosJogador.AoTentarComprarCarta += ComprarCarta;
        EventosJogador.AoCompletarAtaque += HabilitarJogada;
    }

    private void OnDisable()
    {
        EventosTurno.AoFimDoTurnoJogador -= DesabilitarJogada;
        EventosTurno.AoInicioTurnoJogador -= HabilitarJogada;
        EventosJogador.AoTentarComprarCarta -= ComprarCarta;
        EventosJogador.AoCompletarAtaque -= DesabilitarJogada;
    }

    private void HabilitarJogada() {

        if (SistemaDeTurnos.Instance.TemAcoes()) { 
            foreach (var carta in cartasComOJogador)
            {
                carta.DefinirInteragir(true);
            }
        }

    }
    private void DesabilitarJogada()
    {
        foreach (var carta in cartasComOJogador)
        {
            carta.DefinirInteragir(false);
        }
    }

    public void ComprarCarta() {
        if (slotsCarta == null || cartasComOJogador.Count >= slotsCarta.Length) {
            Debug.Log("Mão está cheia ou slots estão vazios");
            return;
        }
        Debug.Log(baralho);
        Debug.Log(baralho == null);

        DadosCartaSO dadosCarta = baralho.SacarCarta();

        if (dadosCarta == null) {
            Debug.Log("Não há cartas no baralho.");
            return;
        }

        int idSlot = cartasComOJogador.Count;
        GameObject novaCarta = Instantiate(prefabCarta, slotsCarta[idSlot].position, Quaternion.identity);
        Carta componenteCarta = novaCarta.GetComponent<Carta>();
        componenteCarta.CarregarDadosCarta(dadosCarta);
        cartasComOJogador.Add(componenteCarta);
        cartasComOJogador[idSlot].transform.SetParent(slotsCarta[idSlot]);

        if (!SistemaDeTurnos.Instance.TemAcoes()) {
            componenteCarta.DefinirInteragir(false);
        }
    }

    private IEnumerator JogarCartaComDelay(Carta carta) {
        DesabilitarJogada();
        carta.DefinirSendoJogado(true);

        ParticleSystem jogarCartaVFX = Instantiate(prefabJogarCartaVFX, carta.transform.position, Quaternion.identity);
        jogarCartaVFX.Play();
        carta.Brilhar();

        Destroy(jogarCartaVFX.gameObject, jogarCartaVFX.main.duration);

        cartasComOJogador.Remove(carta);
        pilhaDescarte.DescartarCarta(carta.BuscarDadosCarta());
        EventosJogador.CartaFoiJogada(carta.BuscarDadosCarta());

        yield return new WaitForSeconds(atrasoCarta);

        Destroy(carta.gameObject);
        RealocarCartas();
    }

    public void JogarCarta(Carta carta) {
        StartCoroutine(JogarCartaComDelay(carta));
    }

    private void RealocarCartas() {
        //tira carta do slot atual
        for (int i = 0; i < cartasComOJogador.Count; i++)
        {
            cartasComOJogador[i].transform.SetParent(null);
        }
        for (int i = 0; i < cartasComOJogador.Count; i++)
        {
            cartasComOJogador[i].transform.SetParent(slotsCarta[i]);
            cartasComOJogador[i].transform.position = slotsCarta[i].position;
        }
    }
}
