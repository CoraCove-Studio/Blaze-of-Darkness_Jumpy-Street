using System.Collections.Generic;
using UnityEngine;

public class TerrainManager : MonoBehaviour
{
    [SerializeField] private ObjectPooler objectPooler;

    [SerializeField] private List<GameObject> listOfActiveChunks = new();
    [SerializeField] private int zPositionForNextChunk;
    [SerializeField] private PlayerZPosition playerZ;

    private readonly int maxChunks = 12;
    private readonly int buffer = 24;

    private void Start()
    {
        zPositionForNextChunk = (int)transform.position.z;
        GenerateStartTerrain();
    }

    private void Update()
    {
        DevKeys();
    }

    private void DevKeys()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            LoadChunk(ChunkTypes.WATER);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            LoadChunk(ChunkTypes.GRASS);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            LoadChunk(ChunkTypes.STREET);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            OffloadChunk();
        }
    }

    private void GenerateStartTerrain()
    {
        for (int i = 0; i <= maxChunks; i++)
        {
            if (i < 3)
            {
                LoadChunk(ChunkTypes.GRASS);
            }
            else
            {
                LoadChunk(GetRandomChunkType());
            }
        }
    }

    public void CheckChunkBuffer(int playerZPos)
    {
        if (playerZPos >= zPositionForNextChunk - buffer)
        {
            CycleChunks();
        }
    }

    private void CycleChunks()
    {
        OffloadChunk();
        LoadChunk(GetRandomChunkType());
    }

    private void LoadChunk(ChunkTypes chunkType)
    {
        GameObject newChunk = RequestNewChunk(chunkType);
        PushChunk(newChunk);
        PositionChunk(newChunk);
        newChunk.SetActive(true);
    }

    private void OffloadChunk()
    {
        GameObject chunk = ShiftChunk();
        chunk.SetActive(false);
        listOfActiveChunks.Remove(chunk);
    }

    private GameObject RequestNewChunk(ChunkTypes chunkType)
    {
        // gets a new chunk from the object pooler
        GameObject newChunk = objectPooler.ReturnChunk(chunkType);
        return newChunk;
    }

    private void PositionChunk(GameObject chunk)
    {
        Vector3 newPos = new(0, 0, zPositionForNextChunk);
        chunk.transform.position = newPos;
        UpdateNextChunkPosition(chunk);
    }

    private void UpdateNextChunkPosition(GameObject currentChunk)
    {
        int slicesInLastChunk = currentChunk.GetComponent<Chunk>().NumOfSlices;
        zPositionForNextChunk += slicesInLastChunk;
    }

    private GameObject ShiftChunk()
    {
        GameObject chunk = listOfActiveChunks[0];
        return chunk;
    }

    private void PushChunk(GameObject chunkToPush)
    {
        listOfActiveChunks.Add(chunkToPush);
    }

    private ChunkTypes GetRandomChunkType()
    {
        int randomChoice = Random.Range(1, 4);
        if (randomChoice == 1)
        {
            return ChunkTypes.GRASS;
        }
        else if (randomChoice == 2)
        {
            return ChunkTypes.WATER;
        }
        else
        {
            return ChunkTypes.STREET;
        }
    }
}
