using System.Collections.Generic;
using UnityEngine;

public class Chunk : MonoBehaviour
{
    [SerializeField] private List<Slice> listOfSlices = new();
    private int numOfSlices;
    public int NumOfSlices
    {
                get => numOfSlices;
        private set => numOfSlices = value;
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
