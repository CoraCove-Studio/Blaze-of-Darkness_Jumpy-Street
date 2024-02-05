using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //hardcoded later once method of movement is settled
    [Header("Values")]
    [SerializeField] private int amountToMove;
    [SerializeField] private Vector3Int destinationPos;
    [SerializeField] private Vector3Int currentPos;

    [Header("Key Codes")] //hardcoded later
    [SerializeField] private KeyCode forward;
    [SerializeField] private KeyCode backward;
    [SerializeField] private KeyCode left;
    [SerializeField] private KeyCode right;

    private GameObject player;

    void Start()
    {
        player = this.gameObject;
        destinationPos = Vector3Int.FloorToInt(player.transform.position);
    }


    void Update()
    {
        currentPos = Vector3Int.FloorToInt(player.transform.position);
        ProcessInput();
    }

    private void FixedUpdate()
    {
        MovePlayer(destinationPos);
    }
    private void ProcessInput()
    {
        if (Input.GetKeyDown(forward))
        {
            destinationPos = new Vector3Int(currentPos.x, currentPos.y, (currentPos.z + amountToMove));
        }
        else if (Input.GetKeyDown(backward))
        {
            destinationPos = new Vector3Int(currentPos.x, currentPos.y, (currentPos.z - amountToMove));
        }
        else if (Input.GetKeyDown(left))
        {
            destinationPos = new Vector3Int((currentPos.x - amountToMove), currentPos.y, currentPos.z);
        }
        else if(Input.GetKeyDown(right))
        {
            destinationPos = new Vector3Int((currentPos.x + amountToMove), currentPos.y, currentPos.z);
        }
    }

    private void MovePlayer(Vector3Int newPosition)
    {
        player.transform.position = new Vector3Int(newPosition.x, newPosition.y, newPosition.z);
    }
}
