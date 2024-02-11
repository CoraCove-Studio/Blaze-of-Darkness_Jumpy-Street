using UnityEngine;

public class Slice : MonoBehaviour
{
    private Chunk parentChunk;
    private void Awake()
    {
        parentChunk = GetComponentInParent<Chunk>();
        AddSelfToParentList();
    }
    private void AddSelfToParentList()
    {
        parentChunk.AddSliceToList(this);
    }
}
