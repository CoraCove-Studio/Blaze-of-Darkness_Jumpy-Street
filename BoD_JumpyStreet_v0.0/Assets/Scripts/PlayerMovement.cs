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
    private Vector3Int currentPos;
    private Vector3Int prevPos;
    private bool ableToMove;

    [Header("Key Codes")] 
    [SerializeField] private KeyCode forward;
    [SerializeField] private KeyCode backward;
    [SerializeField] private KeyCode left;
    [SerializeField] private KeyCode right;

    [Header("Other Scripts")]
    private GameManager gm;

    private GameObject player;

    void Start()
    {
        ableToMove = true;
        player = this.gameObject;
        destinationPos = Vector3Int.FloorToInt(player.transform.position);
        currentPos = Vector3Int.FloorToInt(player.transform.position);
        prevPos = Vector3Int.FloorToInt(player.transform.position);
    }


    void Update()
    {
        UpdatePlayerPosition();

        ableToMove = currentPos == destinationPos ? true : false;

        ProcessInput();
    }

    private void FixedUpdate()
    {
        MovePlayer(destinationPos);
    }

    private void UpdatePlayerPosition()
    {
        if (Vector3Int.FloorToInt(player.transform.position) != currentPos)
        {
            currentPos = Vector3Int.FloorToInt(player.transform.position);
        }
    }

    private void ProcessInput()
    {
        if (Input.GetKeyDown(forward) && ableToMove)
        {
            destinationPos = new Vector3Int(currentPos.x, currentPos.y, (currentPos.z + amountToMove));
            prevPos = currentPos;
        }
        else if (Input.GetKeyDown(backward) && ableToMove)
        {
            destinationPos = new Vector3Int(currentPos.x, currentPos.y, (currentPos.z - amountToMove));
            prevPos = currentPos;
        }
        else if (Input.GetKeyDown(left) && ableToMove)
        {
            destinationPos = new Vector3Int((currentPos.x - amountToMove), currentPos.y, currentPos.z);
            prevPos = currentPos;
        }
        else if(Input.GetKeyDown(right) && ableToMove)
        {
            destinationPos = new Vector3Int((currentPos.x + amountToMove), currentPos.y, currentPos.z);
            prevPos = currentPos;
        }
    }

    private void MovePlayer(Vector3Int newPosition)
    {
        player.transform.position = Vector3.MoveTowards(currentPos, newPosition, 1f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(TagManager.OBSTACLE))
        {
            destinationPos = prevPos;
            print("found tree");
        }
        //if (other.CompareTag("log"))
        //{
        //    //log behavior --> parent player to log
        //    //on movement have to deparent --> maybe OnTriggerExit
        //}
        //if (other.CompareTag("death zone"))
        //{
        //    //for cars and invisible boundary colliders
        //    //ends game
        //}
    }
}
