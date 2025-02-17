using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBuffSkillBehavior : MonoBehaviour
{
    public int buffValue;
    public float remainTime;
    private float currentTime;

    public GameObject[] FindAllObjects()
    {
        CharCombatBehavior[] charCombatBehaviors = FindObjectsOfType<CharCombatBehavior>();
        GameObject[] objects = new GameObject[charCombatBehaviors.Length];

        for (int i = 0; i < charCombatBehaviors.Length; i++)
        {
            objects[i] = charCombatBehaviors[i].gameObject;
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
        if(currentTime > remainTime)
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
            obj.GetComponent<CharCombatBehavior>().fireRate += buffValue;
        }
    }

    private void ResetFireRate()
    {
        GameObject[] objectsWithCharCombatBehavior = FindAllObjects();
        foreach (GameObject obj in objectsWithCharCombatBehavior)
        {
            obj.GetComponent<CharCombatBehavior>().fireRate -= buffValue;
        }
    }
}
