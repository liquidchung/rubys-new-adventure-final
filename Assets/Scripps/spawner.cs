using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawner : MonoBehaviour
{
    [SerializeField]
    private GameObject dronePrefab;

    [SerializeField]
    private float droneInterval = 4;

    public int droneCount;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(spawnEnemy(droneInterval, dronePrefab));
    }

    // Update is called once per frame
    private IEnumerator spawnEnemy(float interval, GameObject enemy)
    {
        yield return new WaitForSeconds(interval);
        GameObject newEnemy = Instantiate(enemy, new Vector2(Random.Range(-5f, 5), Random.Range(-6, 6)), Quaternion.identity);
        StartCoroutine(spawnEnemy(interval, enemy));

        droneCount = droneCount + 1;
    }

    void Update()
    {
        if (droneCount == 5)
        {
            StopCoroutine(spawnEnemy(droneInterval, dronePrefab));
        }
    }
}
