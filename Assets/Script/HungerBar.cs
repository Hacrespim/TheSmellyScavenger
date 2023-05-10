using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HungerBar : MonoBehaviour
{
    public float maxTempoSemComer = 10f; // Tempo m�ximo que o jogador pode ficar sem comer
    public float danoPorSegundo = 0.1f; // Quantidade de dano por segundo quando a barra de fome est� vazia
    public int maxVida = 7; // Quantidade m�xima de vidas do jogador
    public int vidaAtual; // Quantidade atual de vidas do jogador
    public float tempoSemComer; // Tempo que o jogador ficou sem comer
    public Image barraDeFome; // Refer�ncia � imagem da barra de fome na interface do usu�rio
    public Transform ultimoCheckpoint; // Refer�ncia ao �ltimo checkpoint em que o jogador foi salvo
    public Image[] vidasUI; // Refer�ncia �s imagens das vidas na interface do usu�rio

    private float tempoUltimaRefeicao; // Tempo em que o jogador comeu pela �ltima vez

    private void Start()
    {
        vidaAtual = maxVida;
        tempoUltimaRefeicao = Time.time;

        // Define as vidas iniciais na interface do usu�rio
        for (int i = 0; i < vidasUI.Length; i++)
        {
            vidasUI[i].gameObject.SetActive(i < maxVida);
        }
    }

    private void Update()
    {
        // Atualiza o tempo que o jogador ficou sem comer
        tempoSemComer = Time.time - tempoUltimaRefeicao;

        // Calcula a quantidade de energia atual do jogador com base no tempo desde a �ltima refei��o
        barraDeFome.fillAmount = Mathf.Clamp01(1f - (tempoSemComer / maxTempoSemComer));

        // Verifica se o tempo sem comer excedeu o tempo m�ximo permitido e aplica dano � vida do jogador se for o caso
        if (tempoSemComer >= maxTempoSemComer)
        {
            vidaAtual -= Mathf.RoundToInt(danoPorSegundo * Time.deltaTime * maxVida);
            vidaAtual = Mathf.Clamp(vidaAtual, 0, maxVida);

            // Chama o m�todo MorrerDeFome() quando a vida do jogador for igual a 0
            if (vidaAtual <= 0)
            {
                StartCoroutine(MorrerDeFome());
            }
        }
        else
        {
            StopCoroutine(MorrerDeFome());
        }

        // Remove uma vida na interface do usu�rio quando o jogador perde uma vida
        for (int i = 0; i < vidasUI.Length; i++)
        {
            vidasUI[i].gameObject.SetActive(i < vidaAtual);
        }

        // Verifica se o jogador perdeu todas as vidas
        if (vidaAtual <= 0)
        {
            // Reposiciona o jogador no �ltimo checkpoint salvo
            transform.position = ultimoCheckpoint.position;

            // Reinicia a vida do jogador e a barra de fome
            vidaAtual = maxVida;
            barraDeFome.fillAmount = 1f;
            tempoUltimaRefeicao = Time.time;
        }
    }
    IEnumerator MorrerDeFome()
    {
        while (true)
        {
            // Reduz a vida do jogador a cada segundo enquanto ele estiver sem comer
            vidaAtual -= Mathf.RoundToInt(danoPorSegundo * maxVida);
            vidaAtual = Mathf.Clamp(vidaAtual, 0, maxVida);

            // Remove uma vida na interface do usu�rio quando o jogador perde uma vida
            for (int i = 0; i < vidasUI.Length; i++)
            {
                vidasUI[i].gameObject.SetActive(i < vidaAtual);
            }

            // Verifica se o jogador perdeu todas as vidas
            if (vidaAtual <= 0)
            {
                // Reposiciona o jogador no �ltimo checkpoint salvo
                transform.position = ultimoCheckpoint.position;

                // Reinicia a vida do jogador e a barra de fome
                vidaAtual = maxVida;
                barraDeFome.fillAmount = 1f;
                tempoUltimaRefeicao = Time.time;
                break;
            }
            yield return new WaitForSeconds(1f);
        }
    }
}