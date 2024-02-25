using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> menuPanels = new();
    [SerializeField] private GameObject currentPanel;
    [SerializeField] private TextMeshProUGUI highScoreCounter;


    private void Start()
    {
        AudioManager.instance.PlayMusic("MainMenu");
    }

    private void Awake()
    {
        GameManager.Instance.LoadGameData();
        UpdateUIScore();
        //AudioManager.instance.PlayMusic("MainMenu");
    }

    private void UpdateUIScore()
    {
        highScoreCounter.text = GameManager.Instance.HighScore.ToString();
    }

    #region button functions
    public void OnClickStartButton()
    {
        SceneManager.LoadScene(1);
    }
    
    public void OnClickMenuButton(int desiredPanelIndex)
    {
        // works for all inter-menu buttons, accepts the index of the panel to navigate to.
        currentPanel.SetActive(false);
        menuPanels[desiredPanelIndex].SetActive(true);
        currentPanel = menuPanels[desiredPanelIndex];
    }

    public void ResetScoreButton()
    {
        GameManager.Instance.ResetGameData();
        UpdateUIScore();
    }

    public void OnClickQuitButton()
    {
        GameManager.Instance.SaveGameData();
        Application.Quit();
    }

    #endregion

    public void SFXOnClick()
    {
        AudioManager.instance.PlaySFX("Click");
    }
}
