using UnityEngine;
//ScriptableObject que dá base à carta
[CreateAssetMenu(fileName = "DadosCartaSO", menuName = "CardGame/DadosCartaSO")]
public class DadosCartaSO : ScriptableObject
{
    public string nomeCarta;
    public string descricao;
    public int custo;
    public Sprite imagem;
    public int poderDeAtaque;
    public int poderDeCura;
}
