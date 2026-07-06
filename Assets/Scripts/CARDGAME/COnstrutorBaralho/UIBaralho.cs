using System.Collections.Generic;
using UnityEngine;

public class UIBaralho : MonoBehaviour
{
    [SerializeField] private GameObject prefabAbaCarta;

    private List<GameObject> abaCartaGameObjects = new List<GameObject>();

    private const float VERTICAL_SPACING = 0.75f;
    private void Start()
    {
        ConstruirUI();
    }

    private void OnEnable()
    {
        EventosBaralho.AoProcessarBaralho += ConstruirUI;
    }

    private void OnDisable()
    {
        EventosBaralho.AoProcessarBaralho -= ConstruirUI;
    }

    private void ConstruirUI() {

        foreach (GameObject abaCarta in abaCartaGameObjects) { 
            Destroy(abaCarta);
        }

        abaCartaGameObjects.Clear();

        List<DadosCartaSO> baralho = GerenteDeBaralho.Instance.BuscarBaralho();

        for (int i = 0; i < baralho.Count; i++)
        {
            GameObject abaCarta = Instantiate(prefabAbaCarta, transform);
            abaCarta.GetComponent<AbaCarta>().CarregarDadosAbaCarta(baralho[i]);
            abaCarta.transform.localPosition = new Vector3(0f, - i * VERTICAL_SPACING, 0f);
            abaCartaGameObjects.Add(abaCarta);
        }
    }
}
