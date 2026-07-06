using System;
using UnityEngine;

// Classe estática responsável por disparar eventos relacionados aos turnos.
public static class EventosTurno
{
    public static event Action AoInicioTurnoJogador; // Evento disparado quando o turno do jogador começa.
    public static event Action AoFimDoTurnoJogador; // Evento disparado quando o turno do jogador termina.

    public static event Action AoInicioTurnoBoss; // Evento disparado quando o turno do boss começa.
    public static event Action AoFimTurnoBoss; // Evento disparado quando o turno do boss termina.

    public static void IniciarTurnoJogador() { // Dispara o evento de início do turno do jogador.
        AoInicioTurnoJogador?.Invoke();// Chama todos os métodos inscritos no evento. O ? evita erro caso ninguém esteja inscrito.
    }

    public static void FinalizarTurnoJogador() { // Dispara o evento de fim do turno do jogador.
        AoFimDoTurnoJogador?.Invoke();
    }

    public static void IniciarTurnoBoss() { // Dispara o evento de início do turno do boss.

        AoInicioTurnoBoss?.Invoke();
    }
    public static void FinalizarTurnoBoss() { // Dispara o evento de fim do turno do boss.
        AoFimTurnoBoss?.Invoke();
    }
}
