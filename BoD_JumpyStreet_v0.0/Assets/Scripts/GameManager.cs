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

    [SerializeField] private int _playerScore = 0;
    [SerializeField] private int highScore = 0;

    public HighScoreUpdater highScoreUpdater;
    public PlayerZPosition playerZPosition;

    #region Getters/Setters
    public int PlayerScore
    {
        get => _playerScore;
        private set => _playerScore = value;
    }
    public int HighScore
    {
        get => highScore;
        private set => highScore = value;
    }
    #endregion

    #region Data Methods
    public void LoadGameData()
    {
        HighScore = PlayerPrefs.GetInt("highScore", 0);
    }

    public void ResetGameData()
    {
        PlayerPrefs.SetInt("highScore", 0);
        PlayerPrefs.Save();
        HighScore = 0;
    }

    public void SaveGameData()
    {
        PlayerPrefs.SetInt("highScore", HighScore);
        PlayerPrefs.Save();
    }
    #endregion

    public void UpdatePlayerScore()
    {
        if (playerZPosition.ZPosition >= 0)
        {
            PlayerScore = playerZPosition.ZPosition;
        }
        if (PlayerScore > HighScore) 
        { 
            HighScore = PlayerScore;
        }
        DisplayScores();
    }
    public void ResetPlayerScore()
    {
        PlayerScore = 0;
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
