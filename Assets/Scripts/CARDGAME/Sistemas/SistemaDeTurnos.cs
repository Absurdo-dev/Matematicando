using System.Collections;
using TMPro;
using UnityEngine;
// Gerencia a alternância entre turno do jogador e do boss.
public class SistemaDeTurnos : Singleton<SistemaDeTurnos>
{
    private enum EstadoTurno { TurnoJogador, TurnoBoss } // Define quais estados de turno existem no jogo.

    private EstadoTurno turnoAtual = EstadoTurno.TurnoJogador; // Armazena de quem é o turno atual.

    [SerializeField] private int maxAcoesPorTurno = 1; // Quantidade máxima de ações permitidas por turno.
    [SerializeField] private int custoComprar = 1; // Custo para comprar uma carta.
    [SerializeField] private int custoReembaralhar = 3; // Custo para reembaralhar o deck.

    [SerializeField] private TextMeshProUGUI acoesRestantesTexto; // Texto da interface que exibe ações restantes.
    [SerializeField] private int tempoEsperaTurno = 2; // Tempo de espera entre um turno e outro.
    [SerializeField] private float tempoDelayBoss = 2f;

    [SerializeField] private TextMeshProUGUI estadoTurnoDisplay;

    private int acoesRestantes; // Quantidade atual de ações disponíveis.

    protected override void Awake()
    {
        base.Awake(); // Inicializa o Singleton.
        acoesRestantes = maxAcoesPorTurno; // Começa o jogo com o máximo de ações.
    }
    private void Start()
    {
        estadoTurnoDisplay.text = "Turno do Jogador!";
        IniciarTurnoJogador(); // Inicia o primeiro turno do jogador.
    }
    private void OnEnable()
    { // Inscreve métodos nos eventos do jogador.
        EventosJogador.AoTentarComprarCarta += TentarComprar;
        EventosJogador.AoTentarReembaralhar += TentarReembaralhar;
        EventosJogador.AoJogarCarta += CartaJogada;
        EventosBoss.AoMorrer += LimparDisplayTurno;
        EventosJogador.AoMorrer += LimparDisplayTurno;
    }

    private void OnDisable()
    {// Remove as inscrições para evitar erros e vazamento de memória.
        EventosJogador.AoTentarComprarCarta -= TentarComprar;
        EventosJogador.AoTentarReembaralhar -= TentarReembaralhar;
        EventosJogador.AoJogarCarta -= CartaJogada;
        EventosBoss.AoMorrer -= LimparDisplayTurno;
        EventosJogador.AoMorrer -= LimparDisplayTurno;
    }

    private void IniciarTurnoJogador() { // Prepara o início do turno do jogador.
        Debug.Log("IniciarTurnoJogador");
        turnoAtual = EstadoTurno.TurnoJogador; // Define o turno atual como sendo do jogador.
        acoesRestantes = maxAcoesPorTurno; // Restaura todas as ações do turno.
        AtualizarUIAcoes(); // Atualiza a interface.
        EventosTurno.IniciarTurnoJogador(); // Notifica outros sistemas que o turno começou.
    }

    private void FinalizarTurnoJogador() { // Finaliza o turno do jogador.
        Debug.Log("FinalizarTurnoJogador");
        EventosTurno.FinalizarTurnoJogador(); // Notifica outros sistemas.
        StartCoroutine(EsperaEntreTurnoCO()); // Inicia a espera antes do próximo turno.
    }

    private IEnumerator IniciarTurnoBoss() { // Inicia o turno do boss.
        Debug.Log("IniciarTurnoBoss");
        turnoAtual = EstadoTurno.TurnoBoss; // Define que agora é o turno do boss.
        yield return new WaitForSeconds(tempoDelayBoss);
        TurnoBoss(); // Executa a lógica do boss.
    }

    private IEnumerator FinalizarTurnoBoss() { // Finaliza o turno do boss.
        Debug.Log("Finalizar turno Boss");
        EventosTurno.FinalizarTurnoBoss(); // Notifica o fim do turno.
        yield return new WaitForSeconds(tempoDelayBoss);
        StartCoroutine(EsperaEntreTurnoCO()); // Aguarda antes de trocar de turno.
    }

    private void LimparDisplayTurno() {
        estadoTurnoDisplay.text = "";
    }

    private IEnumerator EsperaEntreTurnoCO() { // Aguarda alguns segundos antes de iniciar o próximo turno.

        for (int i = 3; i > 0; i--)
        {
            estadoTurnoDisplay.text = i + "...";
            yield return new WaitForSeconds(1f);
        }

        if (GerenteDeJogo.Instance.JogoAtivo()) {  // Só troca o turno se o jogo ainda estiver ativo.

        if (turnoAtual != EstadoTurno.TurnoJogador) // Se não for turno do jogador, inicia o turno dele.
        {
            estadoTurnoDisplay.text = "Turno do jogador!";
            IniciarTurnoJogador();
        }
        else { // Caso contrário, inicia o turno do boss.
            estadoTurnoDisplay.text = "Turno do boss!";
            StartCoroutine(IniciarTurnoBoss());
        }
        }
    }

    private void CartaJogada(DadosCartaSO dadosCarta) { // Chamado quando o jogador utiliza uma carta.
        ConsumirAcao(dadosCarta.custo); // Consome ações baseado no custo da carta.
    }

    private void TentarComprar() { // Chamado quando o jogador tenta comprar uma carta.
        ConsumirAcao(custoComprar);
    }

    private void TentarReembaralhar() {  // Chamado quando o jogador tenta reembaralhar.
        ConsumirAcao(custoReembaralhar);
    }

    public bool TemAcoes() {  // Verifica se ainda existem ações disponíveis.
        return acoesRestantes > 0;
    }

    private void ConsumirAcao(int valor) {   // Remove ações do turno atual.
        acoesRestantes -= valor;
        AtualizarUIAcoes(); // Atualiza a interface após gastar ações.
        if (acoesRestantes <= 0) { // Se não restarem ações, encerra o turno.
            FinalizarTurnoJogador();
        }
    }

    private void TurnoBoss()
    { // Executa toda a lógica do turno do boss.
        EventosTurno.IniciarTurnoBoss(); // Informa aos sistemas que o turno começou.
        StartCoroutine(FinalizarTurnoBoss()); // Após agir, finaliza o turno.
    }

    private void AtualizarUIAcoes() { // Atualiza o texto da interface.
        if (acoesRestantes < 0) { // Evita que valores negativos sejam exibidos.
            acoesRestantes = 0;
        }
        acoesRestantesTexto.text = "Ações restantes: " + acoesRestantes;  // Atualiza o contador visual.
    }
}
