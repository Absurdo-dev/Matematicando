using System.Collections.Generic;
using UnityEngine;

public class CestosAjudante : MonoBehaviour
{
    [SerializeField] private ConjuntoCestos conjunto;
    [SerializeField] private Transform[] slotsCestos;
    [SerializeField] private GameObject prefabCesto;
    [SerializeField] private int quantidadeCestosInicial = 5;

    [SerializeField] private ParticleSystem prefabJogarCestoVFX;

    private List<Cesto> cestosComOAjudante = new List<Cesto>();

    private void HabilitarJogada() { 
        
    }
}
