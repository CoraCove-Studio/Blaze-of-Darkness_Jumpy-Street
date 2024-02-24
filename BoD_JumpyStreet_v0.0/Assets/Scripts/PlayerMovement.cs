using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    [Header("Values")]
    [SerializeField] private PlayerZPosition playerZPosition;
    [SerializeField] private GameObject model;
    [SerializeField] private int amountToMove;
    [SerializeField] private float speed = 1f;
    [SerializeField] private bool overWater = false;

    private int farthestDistanceReached = 0;
    private readonly float rotationSpeed = 1250f;
    private bool ableToMove = true;
    private Vector3Int destinationPos;
    private Vector3Int currentPos;
    private Rigidbody rb;

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
        if (!overWater)
        {
            GameManager.Instance.UpdatePlayerScore();
        }
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
            else if (Input.GetKeyDown(backward) && CanPlayerMoveBackward()) //add conditional to block player from moving back if farthestDistance
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
        if (currentPos.z > farthestDistanceReached)
        {
            farthestDistanceReached = currentPos.z + 1;
        }
    }

    private void UpdateDestinationAndMove(Vector3Int direction)
    {
        destinationPos = currentPos + direction * amountToMove;
        StartCoroutine(MovePlayerCoroutine());
    }

    private bool CanPlayerMoveBackward()
    {
        int difference = farthestDistanceReached - currentPos.z;
        if(difference <= 6)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    #endregion

    private void MovePlayer(Vector3 newPosition)
    {
        // Calculate distance to move
        var step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, newPosition, step);

        // Calculate the direction vector and create a rotation towards it
        Vector3 movementDirection = (newPosition - transform.position).normalized;
        if (movementDirection != Vector3.zero) // Prevent LookRotation from creating errors with a zero vector
        {
            Quaternion targetRotation = Quaternion.LookRotation(new Vector3(-movementDirection.x, 0, -movementDirection.z));
            model.transform.rotation =  Quaternion.RotateTowards(model.transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
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
        CheckForWaterBelow();
        CheckPlayerDistance();
        ableToMove = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(TagManager.OBSTACLE))
        {
            destinationPos = currentPos;
        }
        if (other.CompareTag(TagManager.LOG))
        {
            gameObject.transform.SetParent(other.gameObject.transform);
        }
        if (other.CompareTag(TagManager.HAZARD))
        {
            GameManager.Instance.ResetPlayerScore();
            GameManager.Instance.SaveGameData();
            SceneManager.LoadScene(0);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(TagManager.LOG))
        {
            gameObject.transform.SetParent(null);
        }
    }

    private void CheckForWaterBelow()
    {
        Vector3 rayStart = transform.position;
        Vector3 rayDirection = Vector3.down;
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
                overWater = true;
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
