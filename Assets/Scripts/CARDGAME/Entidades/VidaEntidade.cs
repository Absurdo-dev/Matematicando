using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VidaEntidade : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textoVida;
    [SerializeField] private Slider sliderVida;

    [SerializeField] private int vidaTotal = 100;
    [SerializeField] private int vidaAtual;

    private Flash flash;

    private void Awake()
    {
        flash = GetComponentInChildren<Flash>();
    }
    private void Start()
    {
        vidaAtual = vidaTotal;
        AtualizarUIVida();
    }

    private void AtualizarUIVida() { 
        textoVida.text =  vidaAtual + " / " + vidaTotal;
        sliderVida.maxValue = vidaTotal;
        sliderVida.value = vidaAtual;
    }

    public void CurarDano(int valor) {

        if (vidaAtual <= 0) {
            return;
        }

        vidaAtual += valor;

        if (vidaAtual > vidaTotal) {
            vidaAtual = vidaTotal;
        }
        AtualizarUIVida();
    }

    public void TomarDano(int valor) {
        StartCoroutine(flash.FlashRoutine());
        vidaAtual -= valor;
        if (vidaAtual < 0) {
            vidaAtual = 0;
        }
        AtualizarUIVida();
    }

    public bool Vivo() {
        return vidaAtual > 0;
    }
}
