using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDeckManager : MonoBehaviour
{
    public GameObject[] inGameDeck;
    public GameObject[] inGameChar;
    public Transform[] charSpawnPoints;
    public GameObject currentSelectedChar;

    public void RemoveChildrenIfAny(int i)
    {
        // 부모 오브젝트가 null이 아닌지 확인
        if (charSpawnPoints[i] != null)
        {
            // 부모 오브젝트의 자식 오브젝트가 있는지 확인
            if (charSpawnPoints[i].transform.childCount > 0)
            {
                // 모든 자식 오브젝트 제거
                foreach (Transform child in charSpawnPoints[i].transform)
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

    public void SetDeckToSelected(int i)
    {
        if (inGameDeck[i] != null)
        {
            RemoveChildrenIfAny(i);
            inGameDeck[i] = currentSelectedChar;
            inGameChar[i] = Instantiate(inGameDeck[i], charSpawnPoints[i]);
        }
        else
        {
            inGameDeck[i] = currentSelectedChar;
            inGameChar[i] = Instantiate(inGameDeck[i], charSpawnPoints[i]);
        }
    }

    public void LoadDeck(int i)
    {
        RemoveChildrenIfAny(i);
        inGameChar[i] = Instantiate(inGameDeck[i], charSpawnPoints[i]);
    }
}
