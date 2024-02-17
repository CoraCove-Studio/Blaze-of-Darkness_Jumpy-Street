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
        activeCoroutine = StartCoroutine(Test());
    }

    private IEnumerator Test()
    {
        print("coroutine started");
        yield return new WaitForSecondsRealtime(2);
        print("coroutine ended");
    }

    void OnDestroy()
    {
        Debug.Log("Destroyed");
    }

    private IEnumerator SpawnLog()
    {
        print("LogSpawner Coroutine started.");
        print(gameObject.activeSelf);
        while (gameObject.activeSelf)
        {
            print("Log spawner Coroutine while loop started.");
            print(Time.timeScale);

            yield return new WaitForSeconds(2); // why the fuck isn't this ending

            print("Requesting a new log.");

            GameObject log = objPooler.ReturnLog(GetRandomLogLength());
            print("Got a new log. Object name is " + log.name);
            if (log != null)
            {
                log.transform.parent = transform;
                log.transform.SetPositionAndRotation(transform.position, transform.rotation);
                log.SetActive(true);
                print("Log spawned, position set and active.");
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
