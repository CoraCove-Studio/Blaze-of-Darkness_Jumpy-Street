using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerZPosition : MonoBehaviour
{
    [SerializeField] private int currentZPos;
    public int ZPosition
    {
        get => currentZPos;
        private set => currentZPos = value;
    }

    private void Awake()
    {
        AddSelfToGameManager();
    }
    public void UpdateCurrentZPosition(int zPos)
    {
        currentZPos = zPos;
    }

    public void AddSelfToGameManager()
    {
        GameManager.Instance.playerZPosition = this;
    }
}
