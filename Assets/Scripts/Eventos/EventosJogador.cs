using System;
using UnityEngine;

// Classe estática responsável por centralizar os eventos do jogador.
// Como é estática, pode ser acessada de qualquer lugar do projeto.
public static class EventosJogador
{
    //Eventos do jogador
    public static event Action<DadosCartaSO> AoJogarCarta;
    public static event Action<int> AoTomarDano;
    public static event Action AoMorrer;
    public static event Action AoTentarComprarCarta;
    public static event Action AoTentarReembaralhar;
    public static event Action AoCurarJogador;

    public static event Action AoCompletarAtaque;
    //Gatilhos de cada evento

    public static void AtaqueCompleto() {
        AoCompletarAtaque?.Invoke();
    }
    public static void TomarDano(int dano)
    {
        AoTomarDano?.Invoke(dano);
    }
    public static void CartaFoiJogada(DadosCartaSO dadosCarta) { 
        AoJogarCarta?.Invoke(dadosCarta);
    }

    public static void Morreu()
    {
        AoMorrer?.Invoke();
    }

    public static void TentarComprarCarta() {
        AoTentarComprarCarta?.Invoke();
    }

    public static void TentarReembaralhar() { 
        AoTentarReembaralhar?.Invoke();
    }

    public static void JogadorCurado() { 
        AoCurarJogador?.Invoke();
    }
}
