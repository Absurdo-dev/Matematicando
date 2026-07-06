using System;
using UnityEngine;
//classe estática responsável por disparar eventos relacionados aos turnos (só existe 1)
public static class EventosRodada
{
    public static event Action AoInicioTurnoAjudante;
    public static event Action AoFimTurnoAjudante;

    public static event Action AoInicioTurnoVendedor;
    public static event Action AoFimTurnoVendedor;

    public static void IniciarTurnoAjudante() { 
        AoInicioTurnoAjudante?.Invoke(); //Chama todos os métodos inscritos no evento
    }

    public static void FinalizarTurnoAjudante() { 
        AoFimTurnoAjudante?.Invoke();
    }

    public static void IniciarTurnoVendedor() { 
        AoInicioTurnoVendedor?.Invoke();
    }

    public static void FinalizarTurnoVendedor() { 
        AoFimTurnoVendedor?.Invoke();
    }
}
