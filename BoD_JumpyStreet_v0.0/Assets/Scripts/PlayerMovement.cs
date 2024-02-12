using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* TO DO
 * parent gameObject to log
 * collision handler 
 */

public class PlayerMovement : MonoBehaviour
{

    [Header("Values")]
    [SerializeField] private int amountToMove;
    private Vector3Int destinationPos;
    private Vector3Int currentPos;
    private Vector3Int prevPos;
    private bool ableToMove = true;
    [SerializeField] private int farthestDistanceReached = 0;
    [SerializeField] private float maxDistanceDelta = 1f;

    [Header("Key Codes")] 
    [SerializeField] private KeyCode forward;
    [SerializeField] private KeyCode backward;
    [SerializeField] private KeyCode left;
    [SerializeField] private KeyCode right;

    void Start()
    {
        destinationPos = Vector3Int.FloorToInt(gameObject.transform.position);
        currentPos = Vector3Int.FloorToInt(gameObject.transform.position);
        prevPos = Vector3Int.FloorToInt(gameObject.transform.position);
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
        if (Vector3Int.FloorToInt(gameObject.transform.position) != currentPos)
        {
            currentPos = Vector3Int.FloorToInt(gameObject.transform.position);
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
        // var step = maxDistanceDelta * Time.deltaTime; // calculate distance to move
        gameObject.transform.position = Vector3.MoveTowards(currentPos, newPosition, maxDistanceDelta);
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
            //log behavior --> parent gameObject to log
            //on movement have to deparent --> maybe OnTriggerExit
        }
        if (other.CompareTag(TagManager.HAZARD))
        {
            //for cars and invisible boundary colliders
            //ends game
        }
    }
}
