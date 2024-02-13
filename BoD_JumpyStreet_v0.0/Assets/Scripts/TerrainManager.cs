using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainManager : MonoBehaviour
{
    [SerializeField] private ObjectPooler objectPooler;

    [SerializeField] private List<GameObject> listOfActiveChunks = new();
    [SerializeField] private int zPositionForNextChunk;

    private int maxChunks = 4;

    private void Start()
    {
        zPositionForNextChunk = ((int)transform.position.z);
        // GenerateStartTerrain();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            LoadChunk(ChunkTypes.WATER);
        }
        else if (Input.GetKeyDown(KeyCode.G))
        {
            LoadChunk(ChunkTypes.GRASS);
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            LoadChunk(ChunkTypes.STREET);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            OffloadChunk();
        }
    }

    private void GenerateStartTerrain()
    {
        for (int i = 0; i <= maxChunks; i++)
        {
            if (i < 2)
            {
                LoadChunk(ChunkTypes.GRASS);
            }
            else
            {
                // TODO: Make random type
                LoadChunk(GetRandomChunkType());
            }
        }
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
