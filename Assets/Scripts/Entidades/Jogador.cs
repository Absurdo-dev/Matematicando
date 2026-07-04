using System.Collections;
using UnityEngine;

public class Jogador : MonoBehaviour
{
    [SerializeField] private GameObject spriteJogador;
    private Vector3 posOriginal;
    private Animator animControlador;

    private ParticleSystem curaVFX;

    private VidaEntidade vida;

    private void OnEnable()
    {
        EventosJogador.AoTomarDano += ManipularTomarDano;
        EventosJogador.AoJogarCarta += ManipularCartaJogada;
    }

    private void OnDisable()
    {
        EventosJogador.AoTomarDano -= ManipularTomarDano;
        EventosJogador.AoJogarCarta -= ManipularCartaJogada;
    }

    private void Awake()
    {
        animControlador = GetComponentInChildren<Animator>();
        vida = GetComponent<VidaEntidade>();
        curaVFX = GetComponentInChildren<ParticleSystem>();
    }

    private void Start()
    {
        posOriginal = spriteJogador.transform.position;
    }
    private void ManipularCartaJogada(DadosCartaSO dadosCarta) {
        if (dadosCarta.poderDeAtaque > 0) {
            Atacar(dadosCarta);
        }
        if (dadosCarta.poderDeCura > 0) {
            Curar(dadosCarta);
        }
    }
    private void ManipularTomarDano(int dano)
    {
        animControlador.Play("LevouDano");
        vida.TomarDano(dano);

        if (!vida.Vivo())
        {
            Morrer();
        }
    }
    private void Curar(DadosCartaSO dadosCarta) {
        vida.CurarDano(dadosCarta.poderDeCura);
        curaVFX.Play();
        EventosJogador.JogadorCurado();
    }
    private void Atacar(DadosCartaSO dadosCarta) {
        StartCoroutine(AnimAtaqueJogadorCO(dadosCarta));
    }

    private IEnumerator AnimAtaqueJogadorCO(DadosCartaSO dadosCarta) {
        Vector3 posAlvo = posOriginal + new Vector3(6f, 0, 0);
        float duracao = .3f;
        float tempoPassado = 0f;

        while (tempoPassado < duracao) {
            spriteJogador.transform.position = Vector3.Lerp(posOriginal, posAlvo, tempoPassado / duracao);
            tempoPassado += Time.deltaTime;
            yield return null;
        }

        animControlador.Play("Ataque");
        EventosBoss.TomarDano(dadosCarta);
        yield return new WaitForSeconds(0.5f);

        tempoPassado = 0f;
        while (tempoPassado < duracao)
        {
            spriteJogador.transform.position = Vector3.Lerp(posAlvo, posOriginal, tempoPassado / duracao);
            tempoPassado += Time.deltaTime;
            yield return null;
        }
        EventosJogador.AtaqueCompleto();
        yield return null;
    }

    private void Morrer()
    {
        animControlador.Play("Morte");
        EventosJogador.Morreu();
    }

}
