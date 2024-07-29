using System.Collections;
using UnityEngine;

public class DropItemBehavior : MonoBehaviour
{
    public GameObject[] prefabs; // ������ �迭
    public float[] probabilities; // �� �������� ���� Ȯ��
    public Transform center;
    public float radius = 5f;
    public int numberOfPrefabs = 10;
    public float moveDuration = 1f; // ������Ʈ�� �̵��� �ð�
    public float minSpeed = 5f; // �ּ� �̵� �ӵ�
    public float maxSpeed = 10f; // �ִ� �̵� �ӵ�

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

            // �ʱ� ��ġ ���
            Vector2 spawnPosition = new Vector2(
                center.position.x + Mathf.Cos(angle) * distance,
                center.position.y + Mathf.Sin(angle) * distance
            );

            // �����ϰ� ������ ����
            GameObject selectedPrefab = SelectRandomPrefab();

            GameObject spawnedObject = Instantiate(selectedPrefab, spawnPosition, Quaternion.identity);

            // ��ǥ ��ġ ���
            Vector2 targetPosition = center.position + (Vector3)(spawnPosition - (Vector2)center.position).normalized * radius;

            // �̵� �ӵ� ���� ����
            float speed = Random.Range(minSpeed, maxSpeed);

            StartCoroutine(MoveTowards(spawnedObject, targetPosition, speed, moveDuration));
        }

        Destroy(gameObject, 0.5f);
    }

    private GameObject SelectRandomPrefab()
    {
        // Ȯ���� ���� �����ϰ� ������ ����
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
            // �̵� ���� ������ ������� ��ǥ ��ġ�� �̵�
            float step = speed * Time.deltaTime;
            obj.transform.position = Vector2.MoveTowards(obj.transform.position, targetPosition, step);

            // �̵� ���� �ð� ������Ʈ
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // ��Ȯ�� ��ǥ ��ġ�� ����
        obj.transform.position = targetPosition;
    }
}
