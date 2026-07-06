using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GerenteDeJogo : Singleton<GerenteDeJogo>
{
    private bool jogoAtivo = true; //Controla atividade do jogo

    [SerializeField] private float tempoRestart; //Timer para recomeçar o jogo
    [SerializeField] private TextMeshProUGUI ganhaPerdeDisplay; //Display de gameOver
    private void OnEnable()
    {   //
        EventosBoss.AoMorrer += JogadorVence;
        EventosJogador.AoMorrer += JogadorPerde;
    }

    private void OnDisable()
    {
        EventosBoss.AoMorrer -= JogadorVence;
        EventosJogador.AoMorrer -= JogadorPerde;
    }

    private void JogadorVence() {
        jogoAtivo = false;
        ganhaPerdeDisplay.text = "Você venceu!";
        StartCoroutine(RestartJogo());
    }

    private void JogadorPerde() {
        jogoAtivo = false;
        ganhaPerdeDisplay.text = "Você perdeu!";
        StartCoroutine(RestartJogo());
    }

    private IEnumerator RestartJogo() { 
        yield return new WaitForSeconds(tempoRestart);
        SceneManager.LoadScene("CenaJogo");
    }

    public bool JogoAtivo() => jogoAtivo;
}
