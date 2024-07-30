using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnBehavior : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Vector2 spawnAreaMin;
    public Vector2 spawnAreaMax;
    public float cooltime;
    private float currentTime;

    private int lastSpawnCount;
    private bool isDead;
    private List<GameObject> enemys = new List<GameObject>();

    private void Update()
    {
        currentTime += Time.deltaTime;

        if (currentTime >= cooltime)
        {
            SpawnEnemies();
            currentTime = 0;
        }

        if (!isDead)
        {
            lastSpawnCount = enemys.Count;
        }
    }

    void SpawnEnemies()
    {
        Vector3 randomPosition = GetRandomPosition();
        Instantiate(enemyPrefab, randomPosition, Quaternion.identity);
    }

    Vector3 GetRandomPosition()
    {
        float randomX = Random.Range(spawnAreaMin.x, spawnAreaMax.x);
        float randomY = Random.Range(spawnAreaMin.y, spawnAreaMax.y);
        return new Vector3(randomX, randomY, 0f);
    }

    public void RemoveEnemy()
    {
        isDead = true;

        if(isDead)
        {
            for (int i = 0; i < lastSpawnCount; i++)
            {
                Destroy(enemys[i]);
            }

            enemys.Clear();
            isDead = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            enemys.Add(collision.gameObject);
        }
    }
}
