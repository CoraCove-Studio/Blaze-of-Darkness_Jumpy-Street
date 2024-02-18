using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardZoneBehavior : MonoBehaviour
{
    [SerializeField] private GameObject target;

    private void FixedUpdate()
    {
        FollowPlayer();
    }

    private void FollowPlayer()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, target.transform.position.z);
    }
}
