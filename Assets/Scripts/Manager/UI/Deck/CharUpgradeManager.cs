using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.Linq;

public class CharUpgradeManager : MonoBehaviour
{
    [Header("* Components")]
    public PlayerHealthManager healthManager;
    public AssembleSkillManager skillManager;
    public QuestManager questManager;
    public GetGoodsBehavior goodsBehavior;
    public PlayerDeckManager playerDeckManager;

    [Header("* Texts")] // 0: Health, 1: Healing, 2: Gold, 3: Skill
    public TextMeshProUGUI[] needCostTexts;
    public TextMeshProUGUI[] levelCostTexts;
    public TextMeshProUGUI[] valueTexts;
    public TextMeshProUGUI[] charUpgradeCostTexts;

    [Header("* Values")]
    public int[] needCosts = new int[4];
    public int[] levelCosts = new int[4] { 1, 1, 1, 1 };
    public int needCharUpgradeCost;
    public float currentUpgradeFirerate;
    private bool isPlayerUpgradeActive;

    private void Update()
    {
        UpdateTexts();
        UpgradePlayer();
    }

    private void UpdateTexts()
    {
        for (int i = 0; i < needCostTexts.Length; i++)
        {
            needCostTexts[i].text = needCosts[i].ToString();
            levelCostTexts[i].text = levelCosts[i].ToString();
        }

        valueTexts[0].text = (healthManager.maxHealth + 10).ToString();
        valueTexts[1].text = (healthManager.healthRecoveryTime - 0.1f).ToString();
        valueTexts[2].text = (goodsBehavior.upgradeCost + 1).ToString();
        valueTexts[3].text = (skillManager.coolDown - 0.01f).ToString();
    }

    public void SetActiveFireRate()
    {
        isPlayerUpgradeActive = true;
    }

    public void SetDefalutPlayer()
    {
        for (int i = 0; i < playerDeckManager.inGameChar.Length; i++)
        {
            playerDeckManager.inGameChar[i].GetComponent<CharCombatBehavior>().fireRate = 1f;

            if (currentUpgradeFirerate >= 0)
            {
                playerDeckManager.inGameChar[i].GetComponent<CharCombatBehavior>().fireRate += currentUpgradeFirerate;
                charUpgradeCostTexts[i].text = needCharUpgradeCost.ToString();
            }
        }
    }

    public void UpgradePlayer()
    {
        if (isPlayerUpgradeActive && playerDeckManager.inGameDeck != null && playerDeckManager.inGameDeck.All(item => item != null) && goodsBehavior.goodsManager.goods.RebirthStone >= needCharUpgradeCost)
        {
            goodsBehavior.goodsManager.goods.RebirthStone -= needCharUpgradeCost;
            needCharUpgradeCost += 100;
            currentUpgradeFirerate += 0.1f;

            for (int i = 0; i < needCostTexts.Length; i++)
            {
                charUpgradeCostTexts[i].text = needCharUpgradeCost.ToString();
                playerDeckManager.inGameChar[i].GetComponent<CharCombatBehavior>().fireRate += currentUpgradeFirerate;
            }
        }

        isPlayerUpgradeActive = false;
    }

    public void Upgrade(int type)
    {
        if (goodsBehavior.goodsManager.goods.Coin >= needCosts[type])
        {
            goodsBehavior.goodsManager.goods.Coin -= needCosts[type];

            switch (type)
            {
                case 0: // Health
                    healthManager.maxHealth += 10;
                    needCosts[type] += levelCosts[type] * 100;
                    break;
                case 1: // Healing
                    healthManager.healthRecoveryTime -= 0.1f;
                    needCosts[type] += levelCosts[type] * 150;
                    break;
                case 2: // Gold
                    goodsBehavior.upgradeCost++;
                    needCosts[type] += levelCosts[type] * 1000;
                    break;
                case 3: // Skill
                    skillManager.coolDown -= 0.01f;
                    needCosts[type] += levelCosts[type] * 1000;
                    break;
            }

            levelCosts[type]++;
            questManager.upgradeCount++;
        }
    }
}
