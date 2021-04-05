using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;


public class GameManager : MonoBehaviour
{
    public enum GameState
    {
        loading,
        inGame,
        gameOver
    }

    public GameState gameState;
    public List<GameObject> targetPrefabs;
    private float spawnRate = 1.5f;
    private int numberOfLives = 4;
    public List<GameObject> lives;
    
    
    
    public ParticleSystem explosionParticle;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI bestScoreText;
    public GameObject gameoverPanel;
    public GameObject starGamePanel;
    public GameObject scorePanel;
    public GameObject livePanel;
    public Button restartButton;

    private const string MAX_SCORE = "MAX_SCORE";
    

    private int _score;
    private int score
    {
        set
        {
            _score = Mathf.Max(value, 0);

        }
        get
        {
            return _score;
        }
    }
    private int initScore = 0;


    private void Start()
    {
        ShowMaxScore();
    }

    /// <summary>
    /// Método que inicia la partida cambiando el valor del estado del juego
    /// </summary>
    /// <param name="difficulty">Número entero que indica el grado de dificultad del juego</param>
    public void StartGame(int difficulty)
    {
        gameState = GameState.inGame;
        spawnRate /= difficulty; //spawnRate = spawnRate / difficulty;
        numberOfLives -= difficulty;
        Debug.Log(numberOfLives);
        for (int i = 0; i < numberOfLives; i++)
        {
            lives[i].SetActive(true);
        }
        StartCoroutine(SpawnTargetCoroutine());
        UpdateScore(initScore);
        starGamePanel.SetActive(false);
        scorePanel.SetActive(true);
        livePanel.SetActive(true);
    }


    /// <summary>
    /// Instancia un GameObject aleatoria de la lista targetPrefabs, cada cierto tiempo
    /// </summary>
    /// <returns></returns>
    private IEnumerator SpawnTargetCoroutine()
    {
        while (gameState == GameState.inGame)
        {
            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0, targetPrefabs.Count);
            Instantiate(targetPrefabs[index]);
        }
    }

    /// <summary>
    /// Actualizar puntuación y mostrarlo por pantalla
    /// </summary>
    /// <param name="scoreToAdd">Números a añadir a la puntuación global</param>
    public void UpdateScore(int scoreToAdd)
    { 
       score+=scoreToAdd;
       scoreText.text = "Score: " + score;
      
    }

    public void ShowMaxScore()
    {
        int maxScore = PlayerPrefs.GetInt(MAX_SCORE,0);
        bestScoreText.text = "Best score: " + maxScore;
    }
    
    private void SetMaxScore()
    {
        int maxScore = PlayerPrefs.GetInt(MAX_SCORE,0);
        if (score > maxScore)
        {
            PlayerPrefs.SetInt(MAX_SCORE,score);
            //TODO Si hay puntuación máxima añadir celebración
        }
    }

    /// <summary>
    /// Mostrar mensaje de Game Over
    /// </summary>
    public void GameOver()
    {
        numberOfLives--;
        if (numberOfLives >= 0)
        {

            Image starImage = lives[numberOfLives].GetComponent<Image>();
            var tempColor = starImage.color;
            tempColor.a = 0.3f;
            starImage.color = tempColor; 
        }
   
        if (numberOfLives <= 0)
        {
            SetMaxScore();
            gameState = GameState.gameOver;
            gameoverPanel.SetActive(true);

        }
            
    }
    
    /// <summary>
    /// Reinicia la escena actual
    /// </summary>

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    
}
