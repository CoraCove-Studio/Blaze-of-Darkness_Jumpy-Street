using UnityEngine;

public class PlayerZPosition : MonoBehaviour
{
    [SerializeField] private int currentZPos;
    [SerializeField] private TerrainManager terrainManager;
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
        if (zPos > 7)
        {
            TerrainBufferCheck();
        }
    }

    private void TerrainBufferCheck()
    {
        terrainManager.CheckChunkBuffer(currentZPos);
    }

    public void AddSelfToGameManager()
    {
        GameManager.Instance.playerZPosition = this;
    }
}
