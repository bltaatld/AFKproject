using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnBehavior : MonoBehaviour
{
    public GameObject enemyPrefab; // 소환할 적 프리팹
    public Vector2 spawnAreaMin; // 스폰 영역의 최소값 (좌측 하단)
    public Vector2 spawnAreaMax; // 스폰 영역의 최대값 (우측 상단)
    public float cooltime;
    private float currentTime;

    private void Update()
    {
        currentTime += Time.deltaTime;

        if (currentTime >= cooltime)
        {
            SpawnEnemies();
            currentTime = 0;
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
        return new Vector3(randomX, randomY, 0f); // z축이 0인 2D 게임을 가정
    }
}
