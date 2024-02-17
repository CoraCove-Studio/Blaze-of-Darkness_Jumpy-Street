using System.Collections;
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
    private bool ableToMove = true;
    [SerializeField] private int farthestDistanceReached = 0;
    [SerializeField] private float speed = 1f;

    [Header("Key Codes")]
    [SerializeField] private KeyCode forward;
    [SerializeField] private KeyCode backward;
    [SerializeField] private KeyCode left;
    [SerializeField] private KeyCode right;

    void Start()
    {
        destinationPos = Vector3Int.FloorToInt(transform.position);
        currentPos = Vector3Int.FloorToInt(transform.position);
    }

    void Update()
    {
        GetInput();
    }

    private void CheckPlayerDistance()
    {
        if (currentPos.z > farthestDistanceReached)
        {
            farthestDistanceReached = currentPos.z;
            GameManager.Instance.IncrementPlayerScore();
        }
    }

    #region input handling
    private void GetInput()
    {
        // Emma, this is an alternative to your ProcessInput() method
        // that I propose. It uses this method and the two beneath it.
        if (ableToMove && Input.anyKeyDown)
        {
            UpdateCurrentPosition();
            Vector3Int direction = Vector3Int.zero;

            if (Input.GetKeyDown(forward))
            {
                direction = Vector3Int.forward;
            }
            else if (Input.GetKeyDown(backward))
            {
                direction = Vector3Int.back;
            }
            else if (Input.GetKeyDown(left))
            {
                direction = Vector3Int.left;
            }
            else if (Input.GetKeyDown(right))
            {
                direction = Vector3Int.right;
            }

            if (direction != Vector3Int.zero)
            {
                UpdateDestinationAndMove(direction);
            }
        }
    }

    private void UpdateCurrentPosition()
    {
        currentPos = Vector3Int.FloorToInt(gameObject.transform.position);
    }

    private void UpdateDestinationAndMove(Vector3Int direction)
    {
        destinationPos = currentPos + direction * amountToMove;
        StartCoroutine(MovePlayerCoroutine());
    }
    #endregion

    private void MovePlayer(Vector3Int newPosition)
    {
        var step = speed * Time.deltaTime; // calculate distance to move
        transform.position = Vector3.MoveTowards(transform.position, newPosition, step);
    }

    private IEnumerator MovePlayerCoroutine()
    {
        ableToMove = false;
        while (transform.position != destinationPos)
        {
            MovePlayer(destinationPos);
            yield return null;
        }
        CheckPlayerDistance();
        ableToMove = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(TagManager.OBSTACLE))
        {
            destinationPos = currentPos;
            print("found tree");
        }
        if (other.CompareTag(TagManager.LOG))
        {
            gameObject.transform.SetParent(other.gameObject.transform);
        }
        if (other.CompareTag(TagManager.HAZARD))
        {
            //for cars and invisible boundary colliders
            //ends game
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(TagManager.LOG))
        {
            gameObject.transform.SetParent(null);
        }
    }
}
