using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogSpawner : MonoBehaviour
{
    [SerializeField] Transform spawnPoint;

    [SerializeField] float minSpawnTime;
    [SerializeField] float maxSpawnTime;

    private GameObject log;
    private ObjectPooler objPooler;

    // Start is called before the first frame update
    private void Start()
    {
        objPooler = GameObject.Find("ObjectPooler").GetComponent<ObjectPooler>();
    }

    void Awake()
    {
        StartCoroutine(SpawnLog());
    }

    IEnumerator SpawnLog()
    {
        //log = objPooler.ReturnLog();

        if (log != null)
        {
            //log.transform.position = spawnPoint.position;
            //log.transform.forward = spawnPoint.forward
            //log.SetActive(true);

            yield return new WaitForSeconds(Random.Range(minSpawnTime, maxSpawnTime));
        }
    }



}
