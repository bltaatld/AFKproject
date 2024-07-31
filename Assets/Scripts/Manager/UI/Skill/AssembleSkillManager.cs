using System;
using TMPro;
using UnityEngine;

public class AssembleSkillManager : MonoBehaviour
{
    [Header("* Components")]
    public GameObject[] skillSlot;
    public GameObject parentGrid;
    public GameObject selectedSkill;
    public GoodsScoreManager goodsManager;
    public TextMeshProUGUI currentAssembleText;
    public TextMeshProUGUI currentAutoStoneText;

    [Header("* Values")]
    public int max_SkillPitureCount;
    public int current_SkillPitureCount;
    public int canAssembleCount;
    public int ID_selectedSkill;
    public int currentAutoIndex = 0;
    public float coolDown;
    public bool isClicked;
    public bool isAutoAcive;

    private float currentTime;
    private float resetTime;

    public void Update()
    {
        // Time Check
        currentTime += Time.deltaTime;
        resetTime += Time.deltaTime;
        AutoTimingCheck();
        AutoTimingResetCheck();

        currentAssembleText.text = canAssembleCount + " / " + max_SkillPitureCount.ToString();
        currentAutoStoneText.text = goodsManager.goods.AutoStone.ToString();
    }

    public static int GenerateRandomValue(int maxValue)
    {
        // Random Generator Reset (Use DateTime to seed)
        System.Random random = new System.Random((int)DateTime.Now.Ticks);

        // Generate Random Value between 0 ~ maxExclusive - 1 (5 = 0 ~ 4)
        return random.Next(0, maxValue);
    }

    public void AutoTimingCheck()
    {
        if (currentTime > 5f)
        {
            if (isAutoAcive)
            {
                AutoActivePicture();
            }

            canAssembleCount += 1;
            currentTime = 0f;
        }
    }

    public void AutoTimingResetCheck()
    {
        if (resetTime > 120f) // reset auto in 3 minute
        {
            isAutoAcive = false;
            resetTime = 0f;
        }
    }

    public void SpawnSkillPickture()
    {
        if (canAssembleCount > 0)
        {
            int randomValue = GenerateRandomValue(skillSlot.Length);

            if (skillSlot[randomValue] != null) // Work when skill slot is Not null
            {
                // Spawn Skill Picture in Range
                if (current_SkillPitureCount < max_SkillPitureCount)
                {
                    Instantiate(skillSlot[randomValue], parentGrid.transform);
                    current_SkillPitureCount++;
                    canAssembleCount -= 1;
                }
                else
                {
                    Debug.Log("Skill Picture is max");
                }
            }
        }
    }

    public void BuyAuto()
    {
        if (goodsManager.goods.Diamond >= 1000)
        {
            goodsManager.goods.Diamond -= 1000;
            goodsManager.goods.AutoStone += 1;
        }
    }

    public void SetAuto()
    {
        if (goodsManager.goods.AutoStone >= 1)
        {
            isAutoAcive = true;
            goodsManager.goods.AutoStone -= 1;
        }
    }

    public void AutoActivePicture()
    {
        if (currentAutoIndex < parentGrid.transform.childCount)
        {
            Transform child = parentGrid.transform.GetChild(currentAutoIndex);
            child.GetComponent<AssemblePictureBehavior>().AutoActive();
            Debug.Log($"Activated child at index {currentAutoIndex}");
            currentAutoIndex++;
        }
        else
        {
            Debug.Log("All children have been activated.");
            currentAutoIndex = 0; // Resetting the index to allow reactivation
        }
    }

    public void DestoryAllPircure()
    {
        foreach (Transform child in parentGrid.transform)
        {
            Destroy(child.gameObject);
        }
        currentAutoIndex = 0;
    }
}
