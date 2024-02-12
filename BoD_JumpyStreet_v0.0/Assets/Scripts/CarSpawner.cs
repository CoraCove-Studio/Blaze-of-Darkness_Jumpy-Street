using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawner : MonoBehaviour
{
    [SerializeField] Transform spawnPoint;

    [SerializeField] float minSpawnTime;
    [SerializeField] float maxSpawnTime;

    private GameObject car;
    private ObjectPooler objPooler;

    private void Start()
    {
        objPooler = GameObject.Find("ObjectPooler").GetComponent<ObjectPooler>();
    }

    // Start is called before the first frame update
    void Awake()
    {
        StartCoroutine(SpawnCar());
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