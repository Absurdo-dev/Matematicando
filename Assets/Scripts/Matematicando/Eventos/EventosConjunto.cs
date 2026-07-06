using System;
using UnityEngine;

public class EventosConjunto
{
    public static event Action<DadosCestoSO> AoRemoverCestoDoConjunto;
    public static event Action<DadosCestoSO> AoAdicionarCestoDoConjunto;
    public static event Action AoProcessarConjunto;

    public static void RemoverCestoDoConjunto(DadosCestoSO cesto) {
        AoRemoverCestoDoConjunto?.Invoke(cesto);
    }

    public static void AdicionarCestoNoConjunto(DadosCestoSO cesto) { 
        AoAdicionarCestoDoConjunto(cesto);
    }

    public static void ProcessarConjunto() { 
        AoProcessarConjunto?.Invoke();
    }
}
