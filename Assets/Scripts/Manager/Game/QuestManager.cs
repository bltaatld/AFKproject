using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    [Header("* Components")]
    public GameObject questScreen;
    public WaveManager waveManager;
    public GoodsScoreManager goodsScoreManager;
    public string[] descriptionString;
    public string needString;
    public string price1String;
    public string price2String;

    #region Texts
    public TextMeshProUGUI descriptionText;
    public TextMeshProUGUI needText;
    public TextMeshProUGUI price1Text;
    public TextMeshProUGUI price2Text;
    #endregion

    [Header("* Values")]
    public int diamondPrice;
    public int autoStonePrice;

    public int currentQuest;
    public int upgradeCount = 0;
    public int stageClearCount = 0;
    private int needKillCount = 5;
    private int needUpgradeCount = 4;
    private int needStageCount = 4;
    private bool isFirstActive;

    private void Update()
    {
        CheckCurrentQuest();

        descriptionText.text = descriptionString[currentQuest].ToString();
        needText.text = needString;
        price1Text.text = autoStonePrice.ToString();
        price2Text.text = diamondPrice.ToString();
    }

    public void CheckCurrentQuest()
    {
        switch (currentQuest)
        {
            case 0:
                if (QuestKill())
                {
                    currentQuest++;
                }
                break;
            case 1:
                if (QuestUpgrade())
                {
                    currentQuest++;
                }
                break;
            case 2:
                if (QuestStage())
                {
                    currentQuest = 0;
                }
                break;
        }
    }

    public void QuestScreenActive()
    {
        if (questScreen != null)
        {
            // Toggle the active state of questScreen
            questScreen.SetActive(!questScreen.activeSelf);
            Debug.Log("asdasd");
        }
        else
        {
            Debug.LogWarning("questScreen is not assigned.");
        }
    }

    public void QuestClear()
    {
        goodsScoreManager.goods.Diamond += diamondPrice;
        goodsScoreManager.goods.AutoStone += autoStonePrice;
    }

    public bool QuestKill()
    {
        needString = "( " + waveManager.total_EnemyKillCount + "/" + needKillCount + " )";


        if (!isFirstActive)
        {
            waveManager.total_EnemyKillCount = 0;
            isFirstActive = true;
        }

        if (waveManager.total_EnemyKillCount >= needKillCount)
        {
            QuestClear();
            waveManager.total_EnemyKillCount = 0;
            isFirstActive = false;

            return true;
        }
        return false;
    }

    public bool QuestUpgrade()
    {
        needString = "( " + upgradeCount + "/" + needUpgradeCount + " )";

        if (upgradeCount >= needUpgradeCount)
        {
            QuestClear();
            upgradeCount = 0;
            return true;
        }
        return false;
    }

    public bool QuestStage()
    {
        needString = "( " + stageClearCount + "/" + needStageCount + " )";

        if (!isFirstActive)
        {
            stageClearCount = 0;
            isFirstActive = true;
        }

        if (stageClearCount >= needStageCount)
        {
            QuestClear();
            stageClearCount = 0;
            isFirstActive = false;
            
            return true;
        }
        return false;
    }
}
