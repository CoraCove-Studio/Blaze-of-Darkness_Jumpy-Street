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

    [SerializeField] private int playerScore = 0;
    [SerializeField] private int highScore = 0;

    public int HighScore
    {
        get => highScore;
        private set => highScore = value;
    }

    public void IncrementPlayerScore()
    {
        playerScore++;
        if (playerScore > HighScore) { HighScore = playerScore; }
    }

    // the rest of your code here
}
