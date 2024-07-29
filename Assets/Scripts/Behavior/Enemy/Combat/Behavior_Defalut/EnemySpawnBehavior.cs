using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnBehavior : MonoBehaviour
{
    public GameObject enemyPrefab; // ��ȯ�� �� ������
    public Vector2 spawnAreaMin; // ���� ������ �ּҰ� (���� �ϴ�)
    public Vector2 spawnAreaMax; // ���� ������ �ִ밪 (���� ���)
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
        return new Vector3(randomX, randomY, 0f); // z���� 0�� 2D ������ ����
    }
}
