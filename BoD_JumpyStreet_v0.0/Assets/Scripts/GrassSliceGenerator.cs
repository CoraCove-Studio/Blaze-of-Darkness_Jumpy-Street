using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassSliceGenerator : MonoBehaviour
{
    public GameObject slicePrefab;
    public List<GameObject> treePrefabs = new();
    private List<int> treeNoiseValues = new();
    private GameObject currentSlice;

    private void GenerateEmptySlice(Transform transform, GameObject slicePrefab)
    {
        currentSlice = Instantiate(slicePrefab, transform);
    }

    private void PopulateTreeNoise(List<int> treeNoiseList)
    {
        for (int i = 0; i < 20; i++)
        {
            int randomValue = Random.Range(0, 4);
            if (randomValue < 1)
            {
                treeNoiseList[i] = 1;
            }
            else
            {
                treeNoiseList[i] = 0;
            }
        }
    }
}
