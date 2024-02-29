using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("Variables")]
    [Tooltip("Aqui se guardan las coordenadas de los puntos donde puede spawnear los enemigos")]
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private GameObject enemy;
    [SerializeField, Tooltip("Cantidad de Enemigos en la ronda")]
    private int amountOfEnemies;

    [SerializeField, Tooltip("Cantidad de Enemigos que tendremos a disposición")]
    private int totalAmountEnemies;

    [SerializeField] private float spawnRate;
    [SerializeField] Queue<GameObject> enemyPool;
    [SerializeField] private Transform poolParent;

    public static Spawner Instance; // Singleton pattern

    private void Start()
    {
        Instance = this;
        PoolStart();
    }

    private void PoolStart()
    {
        enemyPool = new Queue<GameObject>();
        for (int i = 0; i < totalAmountEnemies; i++)
        {
            GameObject newEnemy = Instantiate(enemy);
            newEnemy.name = "Enemy" + i;
            newEnemy.transform.parent = poolParent;
            enemyPool.Enqueue(newEnemy);
            newEnemy.SetActive(false);
        }

        StartCoroutine(SpawnEnemiesQueue());
    }

    private IEnumerator SpawnEnemiesQueue()
    {
        for (int i = 0; i < amountOfEnemies; i++)
        {
            GameObject spawnedEnemy = CalledEnemy();
            if (spawnedEnemy != null)
            {
                StartCoroutine(CallEnemy(spawnedEnemy));
            }
            yield return new WaitForSeconds(spawnRate);
        }
    }

    private IEnumerator CallEnemy(GameObject enemy)
    {
        yield return new WaitForSeconds(5);
        enemy.transform.position = GetRandomSpawnPoint();
        StartCoroutine(WaitForDeath(enemy));
    }

    private IEnumerator WaitForDeath(GameObject enemy)
    {
        Enemy enemyComponent = enemy.GetComponent<Enemy>();
        while (enemyComponent.health > 0)
        {
            yield return null;
        }
        EnqueueEnemy(enemy);
    }

    private GameObject CalledEnemy()
    {
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        if (enemyPool.Count > 0)
        {
            GameObject enemy = enemyPool.Dequeue();
            enemy.SetActive(true);
            enemy.transform.position = spawnPoint.position;
            enemy.transform.rotation = spawnPoint.rotation;
            return enemy;
        }
        else
        {
            return null;
        }
    }

    public void EnqueueEnemy(GameObject enemy)
    {
        Enemy enemyComponent = enemy.GetComponent<Enemy>();
        if (enemyComponent != null && enemyComponent.health <= 0)
        {
            enemyPool.Enqueue(enemy);
        }
        else
        {
            Debug.LogWarning("Intento de encolar un enemigo con salud > 0 o sin el componente Enemy.");
        }
    }

    private Vector3 GetRandomSpawnPoint()
    {
        return spawnPoints[Random.Range(0, spawnPoints.Length)].position;
    }
}
