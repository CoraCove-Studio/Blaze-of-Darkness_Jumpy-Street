using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* TO DO
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
    private Vector3Int destinationPos;
    [SerializeField] private Vector3Int currentPos;
    [SerializeField] private Vector3Int prevPos;
    [SerializeField] private bool inputGiven;

    [Header("Key Codes")] 
    [SerializeField] private KeyCode forward;
    [SerializeField] private KeyCode backward;
    [SerializeField] private KeyCode left;
    [SerializeField] private KeyCode right;

    private GameObject player;

    void Start()
    {
        inputGiven = false;
        player = this.gameObject;
        destinationPos = Vector3Int.FloorToInt(player.transform.position);
        currentPos = Vector3Int.FloorToInt(player.transform.position);
        prevPos = Vector3Int.FloorToInt(player.transform.position);
    }


    void Update()
    {
        if(Vector3Int.FloorToInt(player.transform.position) != currentPos)
        {
            currentPos = Vector3Int.FloorToInt(player.transform.position);
        }
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
            inputGiven = true;
            destinationPos = new Vector3Int(currentPos.x, currentPos.y, (currentPos.z + amountToMove));
        }
        else if (Input.GetKeyDown(backward))
        {
            inputGiven = true;
            destinationPos = new Vector3Int(currentPos.x, currentPos.y, (currentPos.z - amountToMove));
        }
        else if (Input.GetKeyDown(left))
        {
            inputGiven = true;
            destinationPos = new Vector3Int((currentPos.x - amountToMove), currentPos.y, currentPos.z);
        }
        else if(Input.GetKeyDown(right))
        {
            inputGiven = true;
            destinationPos = new Vector3Int((currentPos.x + amountToMove), currentPos.y, currentPos.z);
        }
        else
        {
            inputGiven = false;
        }
    }

    private void MovePlayer(Vector3Int newPosition)
    {
            prevPos = currentPos;
            player.transform.position = Vector3.MoveTowards(currentPos, newPosition, 1f);
            inputGiven = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("tree"))
        {
            print("found tree");
        }
        if (other.CompareTag("log"))
        {
            //log behavior --> parent player to log
            //on movement have to deparent --> maybe OnTriggerExit
        }
        if (other.CompareTag("death zone"))
        {
            //for cars and invisible boundary colliders
            //ends game
        }
    }
}
