using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BaralhoInicialSO", menuName = "Scriptable Objects/BaralhoInicialSO")]
public class BaralhoInicialSO : ScriptableObject
{
    public List<DadosCartaSO> cartas = new List<DadosCartaSO>();
}
