using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Variáveis para gerenciar o estado geral do jogo
    private int score;
    private int lives;
    private int health;
    private bool gameover;

    // Referência ao último checkpoint alcançado pelo jogador
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

    // Método para atualizar o score do jogador
    public void UpdateScore(int value)
    {
        score += value;
        Debug.Log("Score: " + score);
    }

    // Método para atualizar as vidas do jogador
    public void UpdateLives(int value)
    {
        lives += value;
        Debug.Log("Lives: " + lives);
        if (lives <= 0)
        {
            GameOver();
        }
    }

    // Método para atualizar a saúde do jogador
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

    // Método para atualizar o último checkpoint alcançado pelo jogador
    public void UpdateCheckpoint(Vector3 position)
    {
        lastCheckpoint = position;
        Debug.Log("Last Checkpoint: " + lastCheckpoint);
    }

    // Método para finalizar o jogo
    public void GameOver()
    {
        gameover = true;
        Debug.Log("Game Over!");
        // Aqui você pode adicionar ações como reiniciar o jogo ou voltar para o menu principal
    }

    // Método para reiniciar o jogo
    public void Restart()
    {
        score = 0;
        lives = 3;
        health = 100;
        gameover = false;
        // Aqui você pode reiniciar outros objetos do jogo, como o estado dos inimigos ou os objetos coletáveis
    }

    // Método para verificar se o jogo acabou
    public bool IsGameOver()
    {
        return gameover;
    }

    internal void AddScore(int scoreValue)
    {
        throw new NotImplementedException();
    }
}
