using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    [Header("Active Objects")]
    [SerializeField] List<GameObject> listOfGrassChunks = new();
    [SerializeField] List<GameObject> listOfStreetChunks = new();
    [SerializeField] List<GameObject> listOfWaterChunks = new();
    [SerializeField] List<GameObject> listOfShortLogs = new();
    [SerializeField] List<GameObject> listOfLongLogs = new();
    [SerializeField] List<GameObject> listOfCars = new();

    [Header("Chunk Prefabs")]
    [SerializeField] List<GameObject> listOfChunkPrefabsGrass = new();
    [SerializeField] List<GameObject> listOfChunkPrefabsStreet = new();
    [SerializeField] List<GameObject> listOfChunkPrefabsWater = new();

    [Header("Other Prefabs")]
    [SerializeField] List<GameObject> listOfLogPrefabs = new();
    [SerializeField] List<GameObject> listOfCarPrefabs = new();

    #region chunk methods
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
            chunk = GetNewChunk(chunkType);
        }
        return chunk;
    }
    private GameObject GetNewChunk(ChunkTypes typeOfChunk)
    {
        int randomIndex = Random.Range(0, GetLengthOfChunkListByType(typeOfChunk));
        GameObject chunk;
        switch (typeOfChunk)
        {
            case ChunkTypes.GRASS:
                chunk = Instantiate(listOfChunkPrefabsGrass[randomIndex], transform);
                listOfGrassChunks.Add(chunk);
                break;
            case ChunkTypes.WATER:
                chunk = Instantiate(listOfChunkPrefabsWater[randomIndex], transform);
                listOfWaterChunks.Add(chunk);
                break;
            case ChunkTypes.STREET:
                chunk = Instantiate(listOfChunkPrefabsStreet[randomIndex], transform);
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

    #endregion

    #region log methods

    public GameObject ReturnLog(LogLength logLength)
    {
        GameObject log;
        if (logLength == LogLength.SHORT)
        {
            log = ReturnInactiveObject(listOfShortLogs);
        }
        else
        {
            log = ReturnInactiveObject(listOfLongLogs);
        }

        if (log == null)
        {
            log = GetNewLog(logLength);
        }
        return log;
    }

    private GameObject GetNewLog(LogLength logLength)
    {
        GameObject log;
        if (logLength == LogLength.SHORT)
        {
            log = Instantiate(listOfLogPrefabs[0], transform);
            Log _ = log.GetComponent<Log>();
            _.SetObjectPoolerReference(this);
            listOfShortLogs.Add(log);
        }
        else
        {
            log = Instantiate(listOfLogPrefabs[1], transform);
            listOfLongLogs.Add(log);
        }
        log.SetActive(false);
        return log;
    }

    #endregion

    #region car methods
    public void ReturnCar()
    {

    }

    #endregion

    #region general methods
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

    #endregion
}