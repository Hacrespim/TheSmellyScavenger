using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Vari�veis para gerenciar o estado geral do jogo
    private int score;
    private int lives;
    private int health;
    private bool gameover;

    // Refer�ncia ao �ltimo checkpoint alcan�ado pelo jogador
    private Vector3 lastCheckpoint;

    // Singleton GameManager para acesso global
    private static GameManager instance;

    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameManager>();
            }
            return instance;
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // M�todo para atualizar o score do jogador
    public void UpdateScore(int value)
    {
        score += value;
        Debug.Log("Score: " + score);
    }

    // M�todo para atualizar as vidas do jogador
    public void UpdateLives(int value)
    {
        lives += value;
        Debug.Log("Lives: " + lives);
        if (lives <= 0)
        {
            GameOver();
        }
    }

    // M�todo para atualizar a sa�de do jogador
    public void UpdateHealth(int value)
    {
        health += value;
        Debug.Log("Health: " + health);
        if (health <= 0)
        {
            UpdateLives(-1);
            health = 100;
        }
    }

    // M�todo para atualizar o �ltimo checkpoint alcan�ado pelo jogador
    public void UpdateCheckpoint(Vector3 position)
    {
        lastCheckpoint = position;
        Debug.Log("Last Checkpoint: " + lastCheckpoint);
    }

    // M�todo para finalizar o jogo
    public void GameOver()
    {
        gameover = true;
        Debug.Log("Game Over!");
        // Aqui voc� pode adicionar a��es como reiniciar o jogo ou voltar para o menu principal
    }

    // M�todo para reiniciar o jogo
    public void Restart()
    {
        score = 0;
        lives = 3;
        health = 100;
        gameover = false;
        // Aqui voc� pode reiniciar outros objetos do jogo, como o estado dos inimigos ou os objetos colet�veis
    }

    // M�todo para verificar se o jogo acabou
    public bool IsGameOver()
    {
        return gameover;
    }

    internal void AddScore(int scoreValue)
    {
        throw new NotImplementedException();
    }
}
