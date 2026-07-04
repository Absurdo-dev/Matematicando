using UnityEngine;

public class GerenteDeAudio : Singleton<GerenteDeAudio>
{
    [SerializeField] private AudioClip jogarCartaSFX;
    [SerializeField] private AudioClip comprarCartaSFX;
    [SerializeField] private AudioClip golpeEspadaSFX;
    [SerializeField] private AudioClip golpeCajadoSFX;
    [SerializeField] private AudioClip morteBossSFX;
    [SerializeField] private AudioClip morteJogadorSFX;
    [SerializeField] private AudioClip curaSFX;
    [SerializeField] private AudioClip reembaralharSFX;

    private AudioSource fonteAudio;


    protected override void Awake() {
        base.Awake();

        fonteAudio = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        EventosJogador.AoJogarCarta += CartaJogada;
        EventosJogador.AoTentarComprarCarta += ComprarCarta;
        EventosBoss.AoTomarDano += DanoEspada;
        EventosJogador.AoTomarDano += DanoCajado;
        EventosBoss.AoMorrer += MorteBoss;
        EventosJogador.AoMorrer += MorteJogador;
        EventosJogador.AoCurarJogador += JogadorCurado;
        EventosJogador.AoTentarReembaralhar += Reembaralhar;
    }

    private void OnDisable()
    {
        EventosJogador.AoJogarCarta -= CartaJogada;
        EventosJogador.AoTentarComprarCarta -= ComprarCarta;
        EventosBoss.AoTomarDano -= DanoEspada;
        EventosJogador.AoTomarDano -= DanoCajado;
        EventosBoss.AoMorrer -= MorteBoss;
        EventosJogador.AoMorrer -= MorteJogador;
        EventosJogador.AoCurarJogador -= JogadorCurado;
        EventosJogador.AoTentarReembaralhar -= Reembaralhar;
    }

    private void CartaJogada(DadosCartaSO _) {
        TocarSFX(jogarCartaSFX);
    }
    private void ComprarCarta()
    {
        TocarSFX(comprarCartaSFX);
    }
    private void DanoEspada(DadosCartaSO _)
    {
        TocarSFX(golpeEspadaSFX);
    }
    private void DanoCajado(int _)
    {
        TocarSFX(golpeCajadoSFX);
    }
    private void MorteBoss()
    {
        TocarSFX(morteBossSFX);
    }
    private void MorteJogador()
    {
        TocarSFX(morteJogadorSFX);
    }
    private void JogadorCurado()
    {
        TocarSFX(curaSFX);
    }
    private void Reembaralhar()
    {
        TocarSFX(reembaralharSFX);
    }

    private void TocarSFX(AudioClip clipeDeAudio) {
        if (clipeDeAudio) { 
            fonteAudio.PlayOneShot(clipeDeAudio);
        }
    }
}
