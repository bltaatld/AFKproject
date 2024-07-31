using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBuffSkill : MonoBehaviour
{
    public int buffValue;
    public float remainTime;
    private float currentTime;

    public GameObject[] FindAllObjects()
    {
        PlayerHealthManager[] healthManager = FindObjectsOfType<PlayerHealthManager>();
        GameObject[] objects = new GameObject[healthManager.Length];

        for (int i = 0; i < healthManager.Length; i++)
        {
            objects[i] = healthManager[i].gameObject;
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
            obj.GetComponent<PlayerHealthManager>().healingPower += buffValue;
        }
    }

    private void ResetFireRate()
    {
        GameObject[] objectsWithCharCombatBehavior = FindAllObjects();
        foreach (GameObject obj in objectsWithCharCombatBehavior)
        {
            obj.GetComponent<PlayerHealthManager>().healingPower -= buffValue;
        }
    }
}
