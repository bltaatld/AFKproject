using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [Header("* Components")]
    public EnemySpawnBehavior enemySpawnBehavior;
    public GameObject[] enemyPrefabs;
    public GameObject[] middleStageIcons;
    public GameObject nextStageButton;
    public TextMeshProUGUI waveText;

    [Header("* Values")]
    public int needEnemyKillCount;
    public int current_EnemyKillCount;
    public int stageCount;

    [Header("* Enemy Upgrade Values")]
    public int current_Stage = 1;
    public int current_Wave;
    public float upgradeCoolTime;
    private readonly int[] stageUpgradeValues = { 20, 25, 50, 100 };
    public int middleStageValue;
    private bool isStageUpgrade;

    private void Update()
    {
        waveText.text = "Wave " + current_Stage.ToString() + " - " + current_Wave.ToString();

        // Wave Clear Behavior
        if (current_EnemyKillCount >= needEnemyKillCount)
        {
            // Set up to next stage
            current_Wave++;

            // Upgrade Enemy
            enemySpawnBehavior.enemyPrefab = enemyPrefabs[current_Stage];
            enemySpawnBehavior.cooltime += upgradeCoolTime;
            
            // Need Kill Count Upgrade
            needEnemyKillCount += current_Wave;

            // Reset Value
            current_EnemyKillCount = 0;

            if (current_Wave >= stageUpgradeValues[3] + 1)
            {
                nextStageButton.SetActive(true);
            }
        }


        // Stage "Info Screen" Behavior
        isStageUpgrade = CheckValue(current_Wave);

        // Stage Upgrade Behavior, Only 4 wave in Stage
        if (isStageUpgrade)
        {
            middleStageIcons[middleStageValue].SetActive(true);
            middleStageValue++;

            if (current_Wave >= stageUpgradeValues.Length)
            {
                middleStageValue = 0;
            }
        }
    }

    public bool CheckValue(int value)
    {
        if (stageUpgradeValues[middleStageValue] == value)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void StageUpgrade()
    {
        for (int i = 0; i < middleStageIcons.Length; i++)
        {
            middleStageIcons[i].SetActive(false);
        }

        current_Stage++;

        // Reset Default Value
        ResetDefalutValue();
    }

    public void ResetDefalutValue()
    {
        // Reset Default Value
        current_Wave = 1;
        stageCount = 0;
        needEnemyKillCount = 10;
        enemySpawnBehavior.cooltime = 1.5f;
    } 
}
