using System;
using UnityEngine;

public class EventosBaralho
{
    public static event Action<DadosCartaSO> AoRemoverCartaDoBaralho;
    public static event Action<DadosCartaSO> AoAdicionarCartaNoBaralho;
    public static event Action AoProcessarBaralho;

    public static void RemoverCartaDoDeck(DadosCartaSO carta) { 
        AoRemoverCartaDoBaralho?.Invoke(carta);
    }
    public static void AdicionarCartaNoBaralho(DadosCartaSO carta) { 
        AoAdicionarCartaNoBaralho?.Invoke(carta);
    }
    public static void ProcessarBaralho() { 
        AoProcessarBaralho?.Invoke();
    }
}
