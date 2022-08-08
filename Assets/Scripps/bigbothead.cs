using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class bigbothead : MonoBehaviour
{
    public float displayTime = 7.0f;
    public GameObject dialogBox;
    float timerDisplay;
    public bool spawnWave;
    public GameObject winTextObject;

    [SerializeField]
    private GameObject dronePrefab;

    [SerializeField]
    private float droneInterval = 4;

    public int droneCount;
    public int maxHealth = 10;
    public int health { get { return currentHealth; } }
    public int currentHealth;

    void Start()
    {
        dialogBox.SetActive(false);
        timerDisplay = -1.0f;
        spawnWave = false;
        currentHealth = maxHealth;

        winTextObject.SetActive(false);
    }

    void Update()
    {
        if (timerDisplay >= 0)
        {
            timerDisplay -= Time.deltaTime;
            if (timerDisplay < 0)
            {
                dialogBox.SetActive(false);
                spawnWave = true;
                StartCoroutine(spawnEnemy(droneInterval, dronePrefab));
                Debug.Log("commencehell");
            }
        }

        if (currentHealth <= 0)
        {
            winTextObject.SetActive(true);
        }

    }

    public void ChangeHealth(int amount)
    {
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);

        EnemyHealthBar.instance.SetValue(currentHealth / (float)maxHealth);
    }

    public void DisplayDialog()
    {
        timerDisplay = displayTime;
        dialogBox.SetActive(true);
    }

    private IEnumerator spawnEnemy(float interval, GameObject enemy)
    {
        yield return new WaitForSeconds(interval);
        GameObject newEnemy = Instantiate(enemy, new Vector2(Random.Range(-9f, 10), Random.Range(40f, 30f)), Quaternion.identity);
        StartCoroutine(spawnEnemy(interval, enemy));

        droneCount = droneCount + 1;
    }
}
