using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CharUpgradeManager : MonoBehaviour
{
    [Header("* Components")]
    public PlayerHealthManager healthManager;
    public AssembleSkillManager skillManager;
    public GetGoodsBehavior goodsBehavior;
    public PlayerDeckManager playerDeckManager;

    [Header("* Texts")]
    #region TextMeshs
    public TextMeshProUGUI needHealthCostText;
    public TextMeshProUGUI needHealingCostText;
    public TextMeshProUGUI needGoldCostText;
    public TextMeshProUGUI needSkillCostText;

    public TextMeshProUGUI levelHealthCostText;
    public TextMeshProUGUI levelHealingCostText;
    public TextMeshProUGUI levelGoldCostText;
    public TextMeshProUGUI levelSkillCostText;

    public TextMeshProUGUI valueTextHealthCost;
    public TextMeshProUGUI valueTextHealingCost;
    public TextMeshProUGUI valueTextGoldCost;
    public TextMeshProUGUI valueTextSkillCost;
    #endregion

    [Header("* Values")]
    public int needHealthCost;
    public int needHealingCost;
    public int needGoldCost;
    public int needSkillCost;
    public int needCharUpgradeCost;

    public int levelHealthCost = 1;
    public int levelHealingCost = 1;
    public int levelGoldCost = 1;
    public int levelSkillCost = 1;


    public void Update()
    {
        needHealthCostText.text = needHealthCost.ToString();
        needHealingCostText.text = needHealingCost.ToString();
        needGoldCostText.text = needGoldCost.ToString();
        needSkillCostText.text = needSkillCost.ToString();

        levelHealthCostText.text = levelHealthCost.ToString();
        levelHealingCostText.text = levelHealingCost.ToString();
        levelGoldCostText.text = levelGoldCost.ToString();
        levelSkillCostText.text = levelSkillCost.ToString();

        valueTextHealthCost.text = (healthManager.maxHealth + 10).ToString();
        valueTextHealingCost.text = (healthManager.healthRecoveryTime - 0.1).ToString();
        valueTextGoldCost.text = (goodsBehavior.upgradeCost + 1).ToString();
        valueTextSkillCost.text = (skillManager.coolDown - 0.01f).ToString();
    }

    public void UpgreadePlayer(int id)
    {
        if (playerDeckManager.inGameDeck[id] != null)
        {
            if (goodsBehavior.goodsManager.goods.RebirthStone >= needCharUpgradeCost)
            {
                goodsBehavior.goodsManager.goods.RebirthStone -= needCharUpgradeCost;
                needCharUpgradeCost += 100;
                playerDeckManager.inGameDeck[id].GetComponent<CharCombatBehavior>().fireRate += 0.1f;
            }
        }
    }

    public void UpgradeHealth()
    {
        if (goodsBehavior.goodsManager.goods.Coin >= needHealthCost)
        {
            goodsBehavior.goodsManager.goods.Coin -= needHealthCost;
            healthManager.maxHealth += 10;
            needHealthCost += levelHealthCost * 100;

            levelHealthCost++;
        }
    }

    public void UpgradeHealing()
    {
        if (goodsBehavior.goodsManager.goods.Coin >= needHealingCost)
        {
            goodsBehavior.goodsManager.goods.Coin -= needHealingCost;
            healthManager.healthRecoveryTime -= 0.1f;
            needHealingCost += levelHealingCost * 150;

            levelHealingCost++;
        }
    }

    public void UpgradeGold()
    {
        if (goodsBehavior.goodsManager.goods.Coin >= needGoldCost)
        {
            goodsBehavior.goodsManager.goods.Coin -= needGoldCost;
            goodsBehavior.upgradeCost++;
            needGoldCost += levelGoldCost * 1000;

            levelGoldCost++;
        }
    }

    public void UpgradeSkill()
    {
        if (goodsBehavior.goodsManager.goods.Coin >= needSkillCost)
        {
            goodsBehavior.goodsManager.goods.Coin -= needSkillCost;
            skillManager.coolDown -= 0.01f;
            needSkillCost += levelSkillCost * 1000;

            levelSkillCost++;
        }
    }
}
