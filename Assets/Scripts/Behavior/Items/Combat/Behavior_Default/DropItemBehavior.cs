using System.Collections;
using UnityEngine;

public class DropItemBehavior : MonoBehaviour
{
    public GameObject[] prefabs; // 프리펩 배열
    public float[] probabilities; // 각 프리펩의 생성 확률
    public Transform center;
    public float radius = 5f;
    public int numberOfPrefabs = 10;
    public float moveDuration = 1f; // 오브젝트가 이동할 시간
    public float minSpeed = 5f; // 최소 이동 속도
    public float maxSpeed = 10f; // 최대 이동 속도

    private void Start()
    {
        if (prefabs.Length != probabilities.Length)
        {
            Debug.LogError("Prefab and probabilities arrays are not of the same length");
            return;
        }

        SpawnPrefabsInCircle();
    }

    private void SpawnPrefabsInCircle()
    {
        for (int i = 0; i < numberOfPrefabs; i++)
        {
            float angle = Random.Range(0f, Mathf.PI * 2);
            float distance = Random.Range(0f, radius);

            // 초기 위치 계산
            Vector2 spawnPosition = new Vector2(
                center.position.x + Mathf.Cos(angle) * distance,
                center.position.y + Mathf.Sin(angle) * distance
            );

            // 랜덤하게 프리펩 선택
            GameObject selectedPrefab = SelectRandomPrefab();

            GameObject spawnedObject = Instantiate(selectedPrefab, spawnPosition, Quaternion.identity);

            // 목표 위치 계산
            Vector2 targetPosition = center.position + (Vector3)(spawnPosition - (Vector2)center.position).normalized * radius;

            // 이동 속도 랜덤 설정
            float speed = Random.Range(minSpeed, maxSpeed);

            StartCoroutine(MoveTowards(spawnedObject, targetPosition, speed, moveDuration));
        }

        Destroy(gameObject, 0.5f);
    }

    private GameObject SelectRandomPrefab()
    {
        // 확률에 따라 랜덤하게 프리펩 선택
        float totalProbability = 0f;
        foreach (float prob in probabilities)
        {
            totalProbability += prob;
        }

        float randomValue = Random.Range(0f, totalProbability);
        float cumulativeProbability = 0f;

        for (int i = 0; i < prefabs.Length; i++)
        {
            cumulativeProbability += probabilities[i];
            if (randomValue < cumulativeProbability)
            {
                return prefabs[i];
            }
        }

        // Fallback (should not be reached if probabilities are correctly defined)
        return prefabs[0];
    }

    private IEnumerator MoveTowards(GameObject obj, Vector2 targetPosition, float speed, float duration)
    {
        float elapsedTime = 0f;
        Vector2 startingPosition = obj.transform.position;

        while (elapsedTime < duration)
        {
            // 이동 진행 정도를 기반으로 목표 위치로 이동
            float step = speed * Time.deltaTime;
            obj.transform.position = Vector2.MoveTowards(obj.transform.position, targetPosition, step);

            // 이동 진행 시간 업데이트
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // 정확한 목표 위치로 설정
        obj.transform.position = targetPosition;
    }
}
