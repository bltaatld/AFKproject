using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckPickUpManager : MonoBehaviour
{
    [Header("* Parents")]
    public GameObject pickUpIconParent;
    public GameObject deckIconParent;
    public GoodsScoreManager goodsScoreManager;

    [Header("* CharData")]
    public GameObject[] charData;
    public GameObject[] charIconData;
    public GameObject[] charIconImageData;

    [Header("* List")]
    public List<int> pickedValue;
    public List<GameObject> playerChar;

    private System.Random random;
    private float[] probabilities = { 0.1f, 0.2f, 0.3f, 0.4f };

    void Start()
    {
        random = new System.Random();
    }

    private int GenerateRandomValueByProbability()
    {
        float randomValue = (float)random.NextDouble();
        float cumulativeProbability = 0f;

        for (int i = 0; i < probabilities.Length; i++)
        {
            cumulativeProbability += probabilities[i];
            if (randomValue < cumulativeProbability)
            {
                return i;
            }
        }

        return probabilities.Length - 1;
    }

    public void PickUpCharOnce()
    {
        if (goodsScoreManager.goods.Diamond >= 100)
        {
            RemoveChildrenIfAny();
            int randomValue = GenerateRandomValueByProbability();

            pickedValue.Add(randomValue);
            playerChar.Add(charData[randomValue]);
            InstantiateIcons(randomValue);

            goodsScoreManager.goods.Diamond -= 100;
        }
    }

    public void PickUpCharTenTimes()
    {
        if (goodsScoreManager.goods.Diamond >= 1000)
        {
            RemoveChildrenIfAny();
            for (int i = 0; i < 10; i++)
            {
                int randomValue = GenerateRandomValueByProbability();

                pickedValue.Add(randomValue);
                playerChar.Add(charData[randomValue]);
                InstantiateIcons(randomValue);
            }
        }
    }

    public void InstantiateIcons(int randomValue)
    {
        Instantiate(charIconImageData[randomValue], pickUpIconParent.transform);
        Instantiate(charIconData[randomValue], deckIconParent.transform);
    }

    public void LoadIcons()
    {
        RemoveIcon();

        for (int i=0; i < pickedValue.Count; i++)
        {
            Instantiate(charIconData[pickedValue[i]], deckIconParent.transform);
        }
    }

    public void RemoveIcon()
    {
        foreach (Transform child in deckIconParent.transform)
        {
            Destroy(child.gameObject);
        }
    }

    public void RemoveChildrenIfAny()
    {
        if (pickUpIconParent != null)
        {
            if (pickUpIconParent.transform.childCount > 0)
            {
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
