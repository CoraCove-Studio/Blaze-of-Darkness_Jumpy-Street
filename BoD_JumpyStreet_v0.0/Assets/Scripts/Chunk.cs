using System.Collections.Generic;
using UnityEngine;

public class Chunk : MonoBehaviour
{
    [SerializeField] private List<Slice> listOfSlices = new();
    public int NumOfSlices
    {
        get => listOfSlices.Count;
    }

    public void AddSliceToList(Slice sliceToAdd)
    {
        listOfSlices.Add(sliceToAdd);
    }

    public void UnloadChunk()
    {
        gameObject.SetActive(false);
    }
}
