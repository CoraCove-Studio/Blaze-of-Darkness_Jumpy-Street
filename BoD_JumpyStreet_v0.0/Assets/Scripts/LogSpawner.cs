using System.Collections;
using UnityEngine;

public class LogSpawner : MonoBehaviour
{
    [SerializeField] private float minSpawnTime = 2f;
    [SerializeField] private float maxSpawnTime = 4f;
    [SerializeField] private int minSpeed = 2;
    [SerializeField] private int maxSpeed = 5;

    private float laneSpeed;

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
        StartCoroutine(SpawnLog());
    }

    private float DetermineLaneSpeed(int min, int max)
    {
        float speed = Random.Range(min, max);
        return speed;
    }
    private IEnumerator SpawnLog()
    {
        while (gameObject.activeSelf)
        {
            yield return new WaitForSeconds(Random.Range(minSpawnTime, maxSpawnTime));
            GameObject logObject = objPooler.ReturnLog(GetRandomLogLength());
            if (logObject != null)
            {
                logObject.transform.parent = transform;
                logObject.transform.SetPositionAndRotation(transform.position, transform.rotation);
                Log log = logObject.GetComponent<Log>();
                log.SetSpeed(laneSpeed);
                logObject.SetActive(true);
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
