using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* TO DO
 * wrap currentPos in conditional so it only rewrites value if different
 * wrap MovePlayer() in conditional so it only moves player if player input has occured
 * score call on valid forward movement
 * parent player to log
 * collision handler 
 * check valid movement - need to find prevPos and update as player moves
 */

public class PlayerMovement : MonoBehaviour
{

    [Header("Values")]
    [SerializeField] private int amountToMove;
    [SerializeField] private Vector3Int destinationPos;
    [SerializeField] private Vector3Int currentPos;

    [Header("Key Codes")] 
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

    private void OnTriggerEnter(Collider other)
    {

    }
}
