using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HighScoreUpdater : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI highScoreCounter;
    [SerializeField] private TextMeshProUGUI playerScoreCounter;
    // Start is called before the first frame update
    private void Awake()
    {
        DisplayHighScore();
        DisplayPlayerScore();
        AddSelfToGameManager();
    }
    public void DisplayHighScore()
    {
        int highScore = PlayerPrefs.GetInt("highScore");
        highScoreCounter.text = highScore.ToString();
    }

    public void DisplayPlayerScore()
    {
        playerScoreCounter.text = GameManager.Instance.playerScore.ToString();
    }
    public void AddSelfToGameManager()
    {
        GameManager.Instance.highScoreUpdater = this;
    }
}
