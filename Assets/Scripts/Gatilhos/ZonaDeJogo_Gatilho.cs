using Unity.VisualScripting;
using UnityEngine;

public class ZonaDeJogo_Gatilho : MonoBehaviour
{
    [SerializeField] private CartasJogador jogador;


    private void OnTriggerEnter2D(Collider2D impacto)
    {
        if (impacto.TryGetComponent(out Carta carta)) {
            jogador.JogarCarta(carta);
        }
    }

    private void OnTriggerExit2D(Collider2D impacto)
    {
        if (impacto.TryGetComponent(out Carta carta))
        {
        }
    }
}
