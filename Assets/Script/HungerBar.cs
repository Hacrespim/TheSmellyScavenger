using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HungerBar : MonoBehaviour
{
    public float maxTempoSemComer = 10f; // Tempo m�ximo que o jogador pode ficar sem comer
    public float danoPorSegundo = 0.1f; // Quantidade de dano por segundo quando a barra de fome est� vazia
    public int maxVida = 3; // Quantidade m�xima de vidas do jogador
    public int vidaAtual; // Quantidade atual de vidas do jogador
    public float tempoSemComer; // Tempo que o jogador ficou sem comer
    public float energiaAtual = 1f; // Quantidade atual de energia do jogador
    public Image barraDeFome; // Refer�ncia � imagem da barra de fome na interface do usu�rio
    public Transform ultimoCheckpoint; // Refer�ncia ao �ltimo checkpoint em que o jogador foi salvo
    public AudioClip collectSound; // Som que � reproduzido quando o item � coletado
    public AudioClip hitSound; // Som que � reproduzido quando o jogador � atingido pelo inimigo
    public TextMeshProUGUI gameOverText; // Texto de Game Over, que � exibido quando player n�o tiver vidas

    private float tempoUltimaRefeicao; // Tempo em que o jogador comeu pela �ltima vez
    private int colisoesComInimigo = 0; // Contador de colis�es com inimigos

    private void Start()
    {
        vidaAtual = maxVida;
        tempoUltimaRefeicao = Time.time;
    }

    private void Update()
    {
        // Atualiza o tempo que o jogador ficou sem comer
        tempoSemComer = Time.time - tempoUltimaRefeicao;

        // Calcula a quantidade de energia atual do jogador com base no tempo desde a �ltima refei��o
        energiaAtual = Mathf.Clamp01(1f - (tempoSemComer / maxTempoSemComer));

        // Reduz a energia do jogador ao longo do tempo quando a barra de fome estiver vazia
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Inimigo"))
        {
            colisoesComInimigo++;

            // Reproduz o som de hit
            AudioSource.PlayClipAtPoint(hitSound, transform.position);

            // Se o jogador encostar no inimigo 3 vezes, perde uma vida
            if (colisoesComInimigo >= 3)
            {
                // Reduz a quantidade de vidas do jogador
                vidaAtual--;
                colisoesComInimigo = 0;

                // Verifica se o jogador perdeu todas as vidas
                if (vidaAtual <= 0)
                {
                    // Reposiciona o jogador no �ltimo checkpoint visitado
                    transform.position = ultimoCheckpoint.position;

                    // Reinicia a energia do jogador e as vidas
                    vidaAtual = maxVida;
                    energiaAtual = 1f;
                    tempoUltimaRefeicao = Time.time;

                    // Verifica se o objeto TMP est� desativado e, em caso positivo, o ativa
                    if (gameOverText.gameObject.activeSelf == false)
                    {
                        gameOverText.gameObject.SetActive(true);
                    }
                }
            }
        }
    }
}