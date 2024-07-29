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

    // 각 요소에 대한 확률 (합이 100%가 되도록 설정)
    private float[] probabilities = { 0.1f, 0.2f, 0.3f, 0.4f }; // 예: 0번 10%, 1번 20%, 2번 30%, 3번 40%

    void Start()
    {
        // Random 객체 초기화
        random = new System.Random();
    }

    private int GenerateRandomValueByProbability()
    {
        // 0 ~ 1 사이의 랜덤 값 생성
        float randomValue = (float)random.NextDouble();
        float cumulativeProbability = 0f;

        // 확률에 따라 인덱스를 선택
        for (int i = 0; i < probabilities.Length; i++)
        {
            cumulativeProbability += probabilities[i];
            if (randomValue < cumulativeProbability)
            {
                return i;
            }
        }

        // 확률 합계에 오류가 있을 경우 기본값 반환
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

    // 부모 오브젝트의 자식 오브젝트가 있는지 확인하고 삭제
    public void RemoveChildrenIfAny()
    {
        // 부모 오브젝트가 null이 아닌지 확인
        if (pickUpIconParent != null)
        {
            // 부모 오브젝트의 자식 오브젝트가 있는지 확인
            if (pickUpIconParent.transform.childCount > 0)
            {
                // 모든 자식 오브젝트 제거
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
