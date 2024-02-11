using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    [SerializeField] List<GameObject> listOfGrassChunks = new();
    [SerializeField] List<GameObject> listOfStreetChunks = new();
    [SerializeField] List<GameObject> listOfWaterChunks = new();
    [SerializeField] List<GameObject> listOfLogs = new();
    [SerializeField] List<GameObject> listOfCars = new();

    [SerializeField] List<GameObject> listOfChunkPrefabsGrass = new();
    [SerializeField] List<GameObject> listOfChunkPrefabsStreet = new();
    [SerializeField] List<GameObject> listOfChunkPrefabsWater = new();

    [SerializeField] List<GameObject> listOfLogPrefabs = new();
    [SerializeField] List<GameObject> listOfCarPrefabs = new();

    // keep track of ALL object instances

    public GameObject ReturnChunk(ChunkTypes chunkType)
    {
        GameObject chunk = null;
        switch (chunkType)
        {
            case ChunkTypes.GRASS:
                chunk = ReturnInactiveObject(listOfGrassChunks);
                break;
            case ChunkTypes.WATER:
                chunk = ReturnInactiveObject(listOfWaterChunks);
                break;
            case ChunkTypes.STREET:
                chunk = ReturnInactiveObject(listOfStreetChunks);
                break;
            default:
                print("Error. Didn't recognize chunk type.");
                break;
        }
        if (chunk == null)
        {
            return ReturnNewChunk(chunkType);
        }
        else
        {
            return chunk;
        }
    }

    public void ReturnLog(LogLength logLength)
    {

    }

    public void ReturnCar()
    {

    }

    private GameObject ReturnInactiveObject(List<GameObject> listOfObjects)
    {
        foreach (GameObject gameObject in listOfObjects)
        {
            if (gameObject.activeInHierarchy == false)
            {
                return gameObject;
            }
        }
        return null;
    }

    private GameObject ReturnNewChunk(ChunkTypes typeOfChunk)
    {
        int randomIndex = Random.Range(0, GetLengthOfChunkListByType(typeOfChunk));
        GameObject chunk;
        switch (typeOfChunk)
        {
            case ChunkTypes.GRASS:
                chunk = Instantiate(listOfChunkPrefabsGrass[randomIndex], gameObject.transform);
                listOfGrassChunks.Add(chunk);
                break;
            case ChunkTypes.WATER:
                chunk = Instantiate(listOfChunkPrefabsWater[randomIndex], gameObject.transform);
                listOfWaterChunks.Add(chunk);
                break;
            case ChunkTypes.STREET:
                chunk = Instantiate(listOfChunkPrefabsStreet[randomIndex], gameObject.transform);
                listOfStreetChunks.Add(chunk);
                break;
            default:
                print("Error within ReturnNewChunk method call.");
                return null;
        }
        chunk.SetActive(false);
        return chunk;
    }

    private int GetLengthOfChunkListByType(ChunkTypes typeOfChunk)
    {
        switch (typeOfChunk)
        {
            case ChunkTypes.GRASS:
                return listOfChunkPrefabsGrass.Count;
            case ChunkTypes.WATER:
                return listOfChunkPrefabsWater.Count;
            case ChunkTypes.STREET:
                return listOfChunkPrefabsStreet.Count;
            default:
                print("Error. Didn't recognize chunk type.");
                return 0;
        }
    }
}
