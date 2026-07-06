using System;
using UnityEngine;

public static class EventosBoss
{
    public static event Action<DadosCartaSO> AoTomarDano;
    public static event Action AoMorrer;

    public static void TomarDano(DadosCartaSO dadosCarta)
    {
        AoTomarDano?.Invoke(dadosCarta);
    }

    public static void Morreu() {
        AoMorrer?.Invoke();
    }
}
