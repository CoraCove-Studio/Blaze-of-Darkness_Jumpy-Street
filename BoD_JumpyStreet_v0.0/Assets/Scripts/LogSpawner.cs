using System.Collections;
using UnityEngine;

public class LogSpawner : MonoBehaviour
{
    [SerializeField] private float minSpawnTime;
    [SerializeField] private float maxSpawnTime;

    [SerializeField] private ObjectPooler objPooler;

    // Start is called before the first frame update

    void Awake()
    {
        objPooler = GetComponentInParent<ObjectPooler>();
        StartCoroutine(SpawnLog());
    }

    private IEnumerator SpawnLog()
    {
        while (gameObject.activeSelf)
        {
            GameObject log = objPooler.ReturnLog(GetRandomLogLength());
            log.transform.parent = transform;

            if (log != null)
            {
                log.transform.SetPositionAndRotation(transform.position, transform.rotation);
                log.SetActive(true);

                yield return new WaitForSeconds(Random.Range(minSpawnTime, maxSpawnTime));
            }
        }
    }

    private LogLength GetRandomLogLength()
    {
        int randomChoice = Random.Range(0, 2);
        LogLength result = randomChoice == 0 ? LogLength.SHORT : LogLength.LONG;
        return result;
    }

}
