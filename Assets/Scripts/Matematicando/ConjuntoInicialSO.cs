using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ConjuntoInicialSO", menuName = "Matematicando/ConjuntoInicialSO")]
public class ConjuntoInicialSO : ScriptableObject
{
    public List<DadosCestoSO> Cestos = new List<DadosCestoSO>();
}
