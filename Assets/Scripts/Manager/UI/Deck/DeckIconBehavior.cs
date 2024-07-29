using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckIconBehavior : MonoBehaviour
{
    public GameObject playerPrefab;
    private PlayerDeckManager deckManager;

    private void Start()
    {
        deckManager = GameObject.Find("DeckManager").GetComponent<PlayerDeckManager>();
    }

    public void SetCurrentSelectedChar()
    {
        deckManager.currentSelectedChar = playerPrefab;
    }
}
