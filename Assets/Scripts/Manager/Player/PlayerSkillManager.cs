using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerSkillManager : MonoBehaviour
{
    public PlayerDeckManager playerDeckManager;
    public AssembleSkillManager assemblSkillManager;
    public string[] playerNameID;
    public GameObject[] skillButtonPrefab;

    public void Update()
    {
        for (int i = 0; i < playerDeckManager.inGameDeck.Length; i++)
        {
            for (int j = 0; j < playerNameID.Length; j++)
            {
                if (playerDeckManager.inGameDeck[i] != null)
                {
                    if (playerDeckManager.inGameDeck[i].GetComponent<CharCombatBehavior>().nameID == playerNameID[j])
                    {
                        assemblSkillManager.skillSlot[i] = skillButtonPrefab[j];
                    }
                }
            }
        }
    }
}
