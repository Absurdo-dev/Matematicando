using UnityEngine;

public class ZonaDeckGatilho : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D impacto)
    {
        if (impacto.TryGetComponent(out Carta carta)) {
            EventosBaralho.AdicionarCartaNoBaralho(carta.BuscarDadosCarta());
            Debug.Log("Adicionado carta no baralho");
        }
    }
}
