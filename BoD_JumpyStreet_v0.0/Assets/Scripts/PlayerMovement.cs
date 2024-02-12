using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* TO DO
 * parent player to log
 * collision handler 
 */

public class PlayerMovement : MonoBehaviour
{

    [Header("Values")]
    [SerializeField] private int amountToMove;
    private Vector3Int destinationPos;
    private Vector3Int currentPos;
    private Vector3Int prevPos;
    private bool ableToMove;
    [SerializeField] private int farthestDistanceReached;

    [Header("Key Codes")] 
    [SerializeField] private KeyCode forward;
    [SerializeField] private KeyCode backward;
    [SerializeField] private KeyCode left;
    [SerializeField] private KeyCode right;

    [Header("Other Scripts")]

    private GameObject player;

    void Start()
    {
        ableToMove = true;
        player = this.gameObject;
        destinationPos = Vector3Int.FloorToInt(player.transform.position);
        currentPos = Vector3Int.FloorToInt(player.transform.position);
        prevPos = Vector3Int.FloorToInt(player.transform.position);
        farthestDistanceReached = 0;
    }


    void Update()
    {
        UpdatePlayerPosition();

        HasPlayerArrivedAtDestination();

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
    
    private void HasPlayerArrivedAtDestination()
    {
        if(currentPos == destinationPos)
        {
            ableToMove = true;
            CheckPlayerDistance();
        }
        else
        {
            ableToMove = false;
        }
    }

    private void CheckPlayerDistance()
    {
        if(currentPos.z > farthestDistanceReached)
        {
            farthestDistanceReached = currentPos.z;
            GameManager.Instance.IncrementPlayerScore();
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
        if (other.CompareTag(TagManager.LOG))
        {
            //log behavior --> parent player to log
            //on movement have to deparent --> maybe OnTriggerExit
        }
        if (other.CompareTag(TagManager.HAZARD))
        {
            //for cars and invisible boundary colliders
            //ends game
        }
    }
}
