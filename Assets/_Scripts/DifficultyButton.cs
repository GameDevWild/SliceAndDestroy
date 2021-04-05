using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifficultyButton : MonoBehaviour
{
    private Button _difficultyButton;
    private GameManager gameManager;
    [Range(1,3)]
    public int difficulty;
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        _difficultyButton = GetComponent<Button>();
        _difficultyButton.onClick.AddListener(SetDifficulty);
    }

    /// <summary>
    /// Configura la dificultad de la partida
    /// </summary>
    
    void SetDifficulty()
    {
        gameManager.StartGame(difficulty);
    }
}
