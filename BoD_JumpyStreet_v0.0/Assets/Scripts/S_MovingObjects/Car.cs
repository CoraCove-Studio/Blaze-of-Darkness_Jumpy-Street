using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    [SerializeField] private float speed;
    ObjectPooler objectPooler;

    void FixedUpdate()
    {
        transform.Translate((Vector3.forward * Time.deltaTime) * speed);
    }

    public void SetObjectPoolerReference(ObjectPooler reference)
    {
        objectPooler = reference;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(TagManager.KILL_BOX))
        {
            gameObject.transform.parent = objectPooler.transform;
            gameObject.SetActive(false);
        }
    }
}
