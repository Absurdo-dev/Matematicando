using System.Collections;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [SerializeField] private int danoDeAtaque = 5;
    private VidaEntidade vida;
    private Animator animControlador;

    private Vector3 posOriginal;

    [SerializeField] private GameObject spriteBoss;

    private void OnEnable()
    {
        EventosBoss.AoTomarDano += ManipularTomarDano;
        EventosTurno.AoInicioTurnoBoss += Atacar;
    }

    private void OnDisable()
    {
        EventosBoss.AoTomarDano -= ManipularTomarDano;
        EventosTurno.AoInicioTurnoBoss -= Atacar;
    }

    private void Awake()
    {
        vida = GetComponent<VidaEntidade>();
        animControlador = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        posOriginal = spriteBoss.transform.position;
    }

    private void ManipularTomarDano(DadosCartaSO dadosCarta) {
        animControlador.Play("LevouDano");
        vida.TomarDano(dadosCarta.poderDeAtaque);

        if (!vida.Vivo()) {
            Morrer();
        }
    }

    private void Atacar() {
        StartCoroutine(AnimAtaqueBossCO());
    }

    private IEnumerator AnimAtaqueBossCO()
    {
        Vector3 posAlvo = posOriginal + new Vector3(-6f, 0, 0);
        float duracao = .3f;
        float tempoPassado = 0f;

        while (tempoPassado < duracao)
        {
            spriteBoss.transform.position = Vector3.Lerp(posOriginal, posAlvo, tempoPassado / duracao);
            tempoPassado += Time.deltaTime;
            yield return null;
        }

        animControlador.Play("Ataque");
        EventosJogador.TomarDano(danoDeAtaque);
        yield return new WaitForSeconds(0.5f);

        tempoPassado = 0f;
        while (tempoPassado < duracao)
        {
            spriteBoss.transform.position = Vector3.Lerp(posAlvo, posOriginal, tempoPassado / duracao);
            tempoPassado += Time.deltaTime;
            yield return null;
        }

        yield return null;
    }

    private void Morrer() {
        animControlador.Play("Morte");
        EventosBoss.Morreu();
    }
}
