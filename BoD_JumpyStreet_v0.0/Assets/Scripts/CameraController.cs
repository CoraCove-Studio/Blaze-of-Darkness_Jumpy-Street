using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private GameObject target;
    private Vector3 desiredPos;
    private Vector3 offset = new(0, 5, -4);
    [SerializeField] private float speed = 0.65f;
    private Vector3 velocity = Vector3.zero;


    private void FixedUpdate()
    {
        desiredPos = UpdateDesiredPosition(offset);
        if (transform.position != desiredPos)
        {
            MoveCamera(transform.position, desiredPos, speed);
        }
    }

    private void MoveCamera(Vector3 currentPos, Vector3 targetPos, float speed)
    {
        // Use SmoothDamp for smoother transitions
        transform.position = Vector3.SmoothDamp(currentPos, targetPos, ref velocity, speed);
    }


    private Vector3 UpdateDesiredPosition(Vector3 offset)
    {
        Vector3 desiredPosition = target.transform.position + offset;
        return desiredPosition;
    }
}
