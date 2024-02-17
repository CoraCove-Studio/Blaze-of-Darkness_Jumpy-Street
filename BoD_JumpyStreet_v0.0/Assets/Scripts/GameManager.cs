using System.Runtime.CompilerServices;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Singleton pattern

    private static GameManager instance;

    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject gameManager = new("GameManager");
                instance = gameManager.AddComponent<GameManager>();
                DontDestroyOnLoad(gameManager);
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
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    #endregion

    [SerializeField] public int playerScore = 0;
    [SerializeField] private int highScore = 0;

    public HighScoreUpdater highScoreUpdater;
    public PlayerZPosition playerZPosition;

    public int HighScore
    {
        get => highScore;
        private set => highScore = value;
    }

    public void IncrementPlayerScore()
    {
        playerScore = playerZPosition.ZPosition;
        print("incremented score)");
        if (playerScore > HighScore) 
        { 
            HighScore = playerScore;
            SetHighScore();
            print("set high score");
        }
        DisplayScores();
    }

    public void SetHighScore()
    {
        PlayerPrefs.SetInt("highScore", HighScore);
        PlayerPrefs.Save();
        print("saved high score");
    }
    
    private void DisplayScores()
    {
        if (highScoreUpdater != null)
        {
            highScoreUpdater.DisplayHighScore();
            highScoreUpdater.DisplayPlayerScore();
        }
    }
}
