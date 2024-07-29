using System;
using UnityEngine;

public class AssembleSkillManager : MonoBehaviour
{
    [Header("* Components")]
    public GameObject[] skillSlot;
    public GameObject parentGrid;
    public GameObject selectedSkill;

    [Header("* Values")]
    public int max_SkillPitureCount;
    public int current_SkillPitureCount;
    public int ID_selectedSkill;
    public float coolDown;
    public bool isClicked;
    private bool canClick;
    private float currentTime;

    public void Update()
    {
        currentTime += Time.deltaTime;
        if (currentTime > coolDown)
        {
            canClick = true;
            currentTime = 0f;
        }
    }

    public static int GenerateRandomValue(int maxValue)
    {
        // Random Generator Reset (Use DateTime to seed)
        System.Random random = new System.Random((int)DateTime.Now.Ticks);

        // Generate Random Value between 0 ~ maxExclusive - 1 (5 = 0 ~ 4)
        return random.Next(0, maxValue);
    }

    public void SpawnSkillPickture()
    {
        if (canClick)
        {
            int randomValue = GenerateRandomValue(skillSlot.Length);

            if (skillSlot[randomValue] != null) // Work when skill slot is Not null
            {
                // Spawn Skill Picture in Range
                if (current_SkillPitureCount < max_SkillPitureCount)
                {
                    Instantiate(skillSlot[randomValue], parentGrid.transform);
                    current_SkillPitureCount++;
                }
                else
                {
                    Debug.Log("Skill Picture is max");
                }
                canClick = false;
            }
        }
    }

    public void DestoryAllPircure()
    {
        foreach (Transform child in parentGrid.transform)
        {
            Destroy(child.gameObject);
        }
    }
}
