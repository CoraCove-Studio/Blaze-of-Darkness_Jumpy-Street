using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> menuPanels = new();
    [SerializeField] private GameObject currentPanel;
    [SerializeField] private TextMeshProUGUI highScoreCounter;


    #region button functions

    public void OnClickStartButton()
    {
        // GameManager load main scene
    }
    
    public void OnClickMenuButton(int desiredPanelIndex)
    {
        // works for all inter-menu buttons, accepts the index of the panel to navigate to.
        currentPanel.SetActive(false);
        menuPanels[desiredPanelIndex].SetActive(true);
        currentPanel = menuPanels[desiredPanelIndex];
    }

    public void OnClickQuitButton()
    {
        Application.Quit();
    }

    #endregion
}
