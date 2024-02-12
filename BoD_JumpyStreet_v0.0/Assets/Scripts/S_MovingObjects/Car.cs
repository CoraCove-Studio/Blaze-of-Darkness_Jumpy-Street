using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    [SerializeField] private float speed;

    void FixedUpdate()
    {
        transform.Translate((Vector3.forward * Time.deltaTime) * speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(TagManager.KILL_BOX))
        {
            this.gameObject.SetActive(false);
        }
    }
}
