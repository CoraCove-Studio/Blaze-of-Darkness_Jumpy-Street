using System.Collections;
using UnityEngine;

public class LogSpawner : MonoBehaviour
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
        StartCoroutine(SpawnLog());
    }

    private IEnumerator SpawnLog()
    {
        while (gameObject.activeSelf)
        {
            yield return new WaitForSeconds(Random.Range(minSpawnTime, maxSpawnTime));
            GameObject log = objPooler.ReturnLog(GetRandomLogLength());
            if (log != null)
            {
                log.transform.parent = transform;
                log.transform.SetPositionAndRotation(transform.position, transform.rotation);
                log.SetActive(true);
            }

            yield return null;
        }
    }

    private LogLength GetRandomLogLength()
    {
        int randomChoice = Random.Range(0, 2);
        LogLength result = randomChoice == 0 ? LogLength.SHORT : LogLength.LONG;
        return result;
    }

}
