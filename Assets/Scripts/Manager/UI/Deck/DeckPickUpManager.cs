using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckPickUpManager : MonoBehaviour
{
    public GameObject pickUpIconParent;
    public GameObject deckIconParent;
    public GameObject[] charData;
    public GameObject[] charIconData;
    public GameObject[] charIconImageData;
    public List<GameObject> playerChar;

    private System.Random random;

    // �� ��ҿ� ���� Ȯ�� (���� 100%�� �ǵ��� ����)
    private float[] probabilities = { 0.1f, 0.2f, 0.3f, 0.4f }; // ��: 0�� 10%, 1�� 20%, 2�� 30%, 3�� 40%

    void Start()
    {
        // Random ��ü �ʱ�ȭ
        random = new System.Random();
    }

    private int GenerateRandomValueByProbability()
    {
        // 0 ~ 1 ������ ���� �� ����
        float randomValue = (float)random.NextDouble();
        float cumulativeProbability = 0f;

        // Ȯ���� ���� �ε����� ����
        for (int i = 0; i < probabilities.Length; i++)
        {
            cumulativeProbability += probabilities[i];
            if (randomValue < cumulativeProbability)
            {
                return i;
            }
        }

        // Ȯ�� �հ迡 ������ ���� ��� �⺻�� ��ȯ
        return probabilities.Length - 1;
    }

    public void PickUpCharOnce()
    {
        RemoveChildrenIfAny();
        int randomValue = GenerateRandomValueByProbability();

        playerChar.Add(charData[randomValue]);
        InstantiateIcons(randomValue);
    }

    public void PickUpCharTenTimes()
    {
        RemoveChildrenIfAny();
        for (int i = 0; i < 10; i++)
        {
            int randomValue = GenerateRandomValueByProbability();

            playerChar.Add(charData[randomValue]);
            InstantiateIcons(randomValue);
        }
    }

    public void InstantiateIcons(int randomValue)
    {
        Instantiate(charIconImageData[randomValue], pickUpIconParent.transform);
        Instantiate(charIconData[randomValue], deckIconParent.transform);
    }

    // �θ� ������Ʈ�� �ڽ� ������Ʈ�� �ִ��� Ȯ���ϰ� ����
    public void RemoveChildrenIfAny()
    {
        // �θ� ������Ʈ�� null�� �ƴ��� Ȯ��
        if (pickUpIconParent != null)
        {
            // �θ� ������Ʈ�� �ڽ� ������Ʈ�� �ִ��� Ȯ��
            if (pickUpIconParent.transform.childCount > 0)
            {
                // ��� �ڽ� ������Ʈ ����
                foreach (Transform child in pickUpIconParent.transform)
                {
                    Destroy(child.gameObject);
                }

                Debug.Log("All child objects have been removed.");
            }
            else
            {
                Debug.Log("No child objects found.");
            }
        }
        else
        {
            Debug.LogWarning("Parent object is not assigned.");
        }
    }
}
