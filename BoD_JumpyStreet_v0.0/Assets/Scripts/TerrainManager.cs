using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainManager : MonoBehaviour
{
    [SerializeField] private ObjectPooler objectPooler;

    private List<GameObject> listOfActiveChunks = new();
    private Vector3 positionForNextChunk = new(0, 0, 0);

    private int maxChunks = 7;

    private void Start()
    {
        GenerateStartTerrain();
    }

    private void GenerateStartTerrain()
    {
        for (int i = 0; i <= maxChunks; i++)
        {
            if (i == 0)
            {
                GameObject newChunk = RequestNewChunk(ChunkTypes.GRASS);
                PushChunk(newChunk);
                PositionChunk(newChunk);
            }
            else
            {
                GameObject newChunk = RequestNewChunk(ChunkTypes.GRASS);
                PushChunk(newChunk);
                PositionChunk(newChunk);
            }
        }
    }

    private GameObject RequestNewChunk(ChunkTypes chunkType)
    {
        // gets a new chunk from the object pooler
        GameObject newChunk = objectPooler.ReturnChunk(chunkType);
        PushChunk(newChunk);
        return newChunk;
    }

    private void PositionChunk(GameObject chunk)
    {
        chunk.transform.position = positionForNextChunk;
        Chunk _ = chunk.GetComponent<Chunk>();
        Vector3 newPos = new(0, 0, _.NumOfSlices);
        positionForNextChunk = newPos;
    }

    private void ShiftChunk()
    {
        // removes and returns the first of the ordered list
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
