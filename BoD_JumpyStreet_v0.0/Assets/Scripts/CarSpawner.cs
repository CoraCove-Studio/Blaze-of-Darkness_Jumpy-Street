using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawner : MonoBehaviour
{
    [SerializeField] Transform spawnPoint;

    [SerializeField] float minSpawnTime;
    [SerializeField] float maxSpawnTime;

    private GameObject car;
    [SerializeField] private ObjectPooler objPooler;

    void Awake()
    {
        StartCoroutine(SpawnCar());
        objPooler = GetComponentInParent<ObjectPooler>();
    }

    IEnumerator SpawnCar()
    {
        //car = objPooler.ReturnCar();

        if (car != null)
        {
            //car.transform.position = spawnPoint.position;
            //car.transform.forward = spawnPoint.forward
            //car.SetActive(true);

            yield return new WaitForSeconds(Random.Range(minSpawnTime, maxSpawnTime));
        }
    }



}