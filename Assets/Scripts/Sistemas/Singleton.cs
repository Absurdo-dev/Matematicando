using UnityEngine;
//O SingleTon existe para que exista apenas uma instância de uma classe.
//Que essa instância possa ser acessada globalmente.
// Classe genérica Singleton.
// T representa o tipo da classe que irá herdar deste Singleton.
// Restrição:
// O tipo T obrigatoriamente deve herdar de Singleton<T>.
// Exemplo válido:
// class GameManager : Singleton<GameManager>
public class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    // Propriedade estática que armazenará a única instância.
    // Como é estática, pertence à classe e não ao objeto.
    public static T Instance { get; private set; }
    protected virtual void Awake()
    {
        // Se já existe uma instânciae essa instância não é este objeto atual...
        if (Instance != null && Instance != this)
        {
            // Destrói este objeto duplicado.
            Destroy(gameObject);
            // Interrompe a execução do método.
            return;
        }

        // Define este objeto como a instância única, "as T" converte o objeto atual para o tipo T.
        // Exemplo: Se T = GameManager Instance = this as GameManager
        Instance = this as T;
    }


    // Chamado quando o objeto for destruído.
    protected virtual void OnDestroy()
    {
        // Se a instância atual é este objeto...
        if (Instance == this)
        {
            // Remove a referência.
            // Isso evita que Instance continue apontando
            // para um objeto destruído.
            Instance = null;
        }
    }
}
