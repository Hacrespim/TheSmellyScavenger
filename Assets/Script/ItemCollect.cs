using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollect : MonoBehaviour
{    
    public AudioClip collectSound; // Som que é reproduzido quando o item é coletado

    private bool collected = false; // Verifica se o item já foi coletado

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !collected) // Verifica se o item colidiu com o jogador e não foi coletado ainda
        {
            collected = true; // Marca o item como coletado            
            AudioSource.PlayClipAtPoint(collectSound, transform.position); // Reproduz o som de coleta
            Destroy(gameObject); // Destroi o objeto do item
        }
    }
}
