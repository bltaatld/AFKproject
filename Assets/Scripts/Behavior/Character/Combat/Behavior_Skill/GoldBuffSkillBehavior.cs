using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldBuffSkillBehavior : MonoBehaviour
{
    public int buffValue;
    public float remainTime;
    private float currentTime;

    public GameObject[] FindAllObjects()
    {
        GetGoodsBehavior[] goodsBehavior = FindObjectsOfType<GetGoodsBehavior>();
        GameObject[] objects = new GameObject[goodsBehavior.Length];

        for (int i = 0; i < goodsBehavior.Length; i++)
        {
            objects[i] = goodsBehavior[i].gameObject;
        }

        return objects;
    }


    private void Start()
    {
        BuffFireRate();
    }

    private void Update()
    {
        currentTime += Time.deltaTime;
        if (currentTime > remainTime)
        {
            ResetFireRate();
            Destroy(gameObject);
        }
    }

    private void BuffFireRate()
    {
        GameObject[] objectsWithCharCombatBehavior = FindAllObjects();
        foreach (GameObject obj in objectsWithCharCombatBehavior)
        {
            obj.GetComponent<GetGoodsBehavior>().upgradeCost += buffValue;
        }
    }

    private void ResetFireRate()
    {
        GameObject[] objectsWithCharCombatBehavior = FindAllObjects();
        foreach (GameObject obj in objectsWithCharCombatBehavior)
        {
            obj.GetComponent<GetGoodsBehavior>().upgradeCost -= buffValue;
        }
    }
}
