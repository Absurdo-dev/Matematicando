using System;
using UnityEngine;

// Classe estática responsável por centralizar os eventos do jogador(Ajudante).
// Como é estática, pode ser acessada de qualquer lugar do projeto.
public static class EventosAjudante
{
    public static event Action<DadosCestoSO> AoJogarCesto;

    public static void CestoFoiJogado(DadosCestoSO dadosCesto) { 
        AoJogarCesto?.Invoke(dadosCesto);
    }
}
