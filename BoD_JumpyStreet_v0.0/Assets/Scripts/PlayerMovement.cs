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
    private bool onLog = false;
    private Rigidbody rb;
    [SerializeField] private float speed = 1f;
    [SerializeField] private PlayerZPosition playerZPosition;

    [Header("Key Codes")]
    [SerializeField] private KeyCode forward;
    [SerializeField] private KeyCode backward;
    [SerializeField] private KeyCode left;
    [SerializeField] private KeyCode right;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        destinationPos = Vector3Int.FloorToInt(transform.position);
        currentPos = Vector3Int.FloorToInt(transform.position);
        playerZPosition.UpdateCurrentZPosition((int)gameObject.transform.position.z);
    }

    void Update()
    {
        GetInput();
    }

    private void CheckPlayerDistance()
    {
        GameManager.Instance.UpdatePlayerScore();
    }

    #region input handling
    private void GetInput()
    {
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
        playerZPosition.UpdateCurrentZPosition((int)gameObject.transform.position.z);
        CheckPlayerDistance();
        ableToMove = true;
        CheckForWaterBelow();
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
            onLog = true;
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
            onLog = false;
            gameObject.transform.SetParent(null);
        }
    }

    void CheckForWaterBelow()
    {
        // The starting point of the ray is the player's position
        Vector3 rayStart = transform.position;

        // The direction of the ray is straight down
        Vector3 rayDirection = Vector3.down;

        // The length of the ray
        float rayLength = 1f;

        // Perform the raycast
        if (Physics.Raycast(rayStart, rayDirection, out RaycastHit hitInfo, rayLength))
        {
            // Check if the collider of the hit object has the tag "water"
            if (hitInfo.collider.CompareTag(TagManager.WATER))
            {
                Debug.Log("Hit water!");
                rb.isKinematic = false;
                rb.useGravity = true;
            }
            else
            {
                Debug.Log("Did not hit water, hit: " + hitInfo.collider.name);
            }
        }
        else
        {
            Debug.Log("Raycast did not hit anything.");
        }
    }
}
