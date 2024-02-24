using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawner : MonoBehaviour
{
    [SerializeField] private float minSpawnTime = 2f;
    [SerializeField] private float maxSpawnTime = 4f;
    [SerializeField] private int minSpeed = 6;
    [SerializeField] private int maxSpeed = 10;

    [SerializeField] private int laneSpeed;

    [SerializeField] private ObjectPooler objPooler;
    [SerializeField] private Coroutine activeCoroutine;

    // Start is called before the first frame update

    void Awake()
    {
        objPooler = GetComponentInParent<ObjectPooler>();
        laneSpeed = DetermineLaneSpeed(minSpeed, maxSpeed);
    }

    private void OnEnable()
    {
        StartCoroutine(SpawnCar());
    }

    private int DetermineLaneSpeed(int min, int max)
    {
        int speed = Random.Range(min, max);
        return speed;
    }

    private IEnumerator SpawnCar()
    {
        while (gameObject.activeSelf)
        {
            yield return new WaitForSeconds(Random.Range(minSpawnTime, maxSpawnTime));
            GameObject carObject = objPooler.ReturnCar();
            if (carObject != null)
            {
                carObject.transform.parent = transform;
                carObject.transform.SetPositionAndRotation(transform.position, transform.rotation);
                Car car = carObject.GetComponent<Car>();
                car.SetSpeed(laneSpeed);
                carObject.SetActive(true);
            }

            yield return null;
        }
    }
}