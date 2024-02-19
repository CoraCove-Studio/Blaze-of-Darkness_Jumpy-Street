using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawner : MonoBehaviour
{
    [SerializeField] private float minSpawnTime = 2f;
    [SerializeField] private float maxSpawnTime = 4f;

    [SerializeField] private ObjectPooler objPooler;
    [SerializeField] private Coroutine activeCoroutine;

    // Start is called before the first frame update

    void Awake()
    {
        objPooler = GetComponentInParent<ObjectPooler>();
    }

    private void OnEnable()
    {
        StartCoroutine(SpawnCar());
    }

    private IEnumerator SpawnCar()
    {
        while (gameObject.activeSelf)
        {
            yield return new WaitForSeconds(Random.Range(minSpawnTime, maxSpawnTime));
            GameObject car = objPooler.ReturnCar();
            if (car != null)
            {
                car.transform.parent = transform;
                car.transform.SetPositionAndRotation(transform.position, transform.rotation);
                car.SetActive(true);
            }

            yield return null;
        }
    }

    //private LogLength GetRandomLogLength()
    //{
    //    int randomChoice = Random.Range(0, 2);
    //    LogLength result = randomChoice == 0 ? LogLength.SHORT : LogLength.LONG;
    //    return result;
    //}

}