using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollect : MonoBehaviour
{
    public int scoreValue; // Valor do score que o item adiciona
    public AudioClip collectSound; // Som que é reproduzido quando o item é coletado

    private bool collected = false; // Verifica se o item já foi coletado

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !collected) // Verifica se o item colidiu com o jogador e não foi coletado ainda
        {
            collected = true; // Marca o item como coletado
            GameManager.Instance.AddScore(scoreValue); // Adiciona o valor do score ao jogo
            AudioSource.PlayClipAtPoint(collectSound, transform.position); // Reproduz o som de coleta
            Destroy(gameObject); // Destroi o objeto do item
        }
    }
}
