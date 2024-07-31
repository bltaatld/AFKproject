using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PlayerIconBehavior : MonoBehaviour
{
    public PlayerDeckManager deckManager;
    public GameObject[] currentDeck;
    public Image[] targetImage;
    public Sprite[] targetSprites;

    private void Update()
    {
        if (deckManager.inGameDeck != null && deckManager.inGameDeck.All(item => item != null))
        {
            currentDeck = deckManager.inGameDeck;
            CheckPlayerIcon(currentDeck);
        }
    }

    public void CheckPlayerIcon(GameObject[] currentDeck)
    {
        if (currentDeck != null && targetImage.Length == targetSprites.Length)
        {
            for (int i = 0; i < targetImage.Length; i++)
            {
                if (currentDeck[i].GetComponent<UniqueIdentifier>().uniqueID == "Player_0")
                {
                    targetImage[i].sprite = targetSprites[0];
                }

                if (currentDeck[i].GetComponent<UniqueIdentifier>().uniqueID == "Player_1")
                {
                    targetImage[i].sprite = targetSprites[1];
                }

                if (currentDeck[i].GetComponent<UniqueIdentifier>().uniqueID == "Player_2")
                {
                    targetImage[i].sprite = targetSprites[2];
                }

                if (currentDeck[i].GetComponent<UniqueIdentifier>().uniqueID == "Player_3")
                {
                    targetImage[i].sprite = targetSprites[3];
                }
            }
        }
        else
        {
            Debug.LogWarning("deckManager is null or target arrays do not match in length.");
        }
    }
}