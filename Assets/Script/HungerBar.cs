using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HungerBar : MonoBehaviour
{
    public float maxTempoSemComer = 10f; // Tempo máximo que o jogador pode ficar sem comer
    public float danoPorSegundo = 0.1f; // Quantidade de dano por segundo quando a barra de fome está vazia
    public int maxVida = 3; // Quantidade máxima de vidas do jogador
    public int vidaAtual; // Quantidade atual de vidas do jogador
    public float tempoSemComer; // Tempo que o jogador ficou sem comer
    public float energiaAtual = 1f; // Quantidade atual de energia do jogador
    public Image barraDeFome; // Referência à imagem da barra de fome na interface do usuário
    public Transform ultimoCheckpoint; // Referência ao último checkpoint em que o jogador foi salvo

    private float tempoUltimaRefeicao; // Tempo em que o jogador comeu pela última vez

    private void Start()
    {
        vidaAtual = maxVida;
        tempoUltimaRefeicao = Time.time;
    }

    private void Update()
    {
        // Atualiza o tempo que o jogador ficou sem comer
        tempoSemComer = Time.time - tempoUltimaRefeicao;

        // Calcula a quantidade de energia atual do jogador com base no tempo desde a última refeição
        energiaAtual = Mathf.Clamp01(1f - (tempoSemComer / maxTempoSemComer));

        // Reduz a energia do jogador ao longo do tempo quando a barra de fome estiver vazia
        if (energiaAtual <= 0f)
        {
            vidaAtual -= (int)(danoPorSegundo * Time.deltaTime);
            vidaAtual = Mathf.Clamp(vidaAtual, 0, maxVida);
        }

        // Atualiza a barra de fome na interface do usuário
        barraDeFome.fillAmount = energiaAtual;

        // Verifica se o jogador perdeu todas as vidas
        if (vidaAtual <= 0)
        {
            // Reposiciona o jogador no último checkpoint salvo
            transform.position = ultimoCheckpoint.position;

            // Reinicia a energia do jogador e as vidas
            vidaAtual = maxVida;
            energiaAtual = 1f;
            tempoUltimaRefeicao = Time.time;
        }
    }

    // Método chamado quando o jogador come um item que restaura sua energia
    public void Comer(float quantidadeDeEnergia)
    {
        tempoUltimaRefeicao = Time.time;
        energiaAtual = Mathf.Clamp01(energiaAtual + quantidadeDeEnergia);
    }
}
