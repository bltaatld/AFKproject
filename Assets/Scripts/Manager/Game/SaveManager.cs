using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class GameData
{
    // Player Deck Info
    public List<string> playerCharIDs = new List<string>();
    public List<int> pickedValue = new List<int>();
    public string[] inGameDeckIDs;
    public string[] inGameCharIDs;

    // Goods Data
    public double rebirthStone;
    public double coin;
    public double diamond;
    public double autoStone;

    // Wave Data
    public int currentStage;
    public int currentWave;

    // Upgrade Data
    public float currentUpgradeFirerate;
    public int needUpgradeCost;

    // Skill Data
    public string[] skillSlotIDs;

    // Quest Data
    public int currentQuest;
}

public class SaveManager : MonoBehaviour
{
    private string dataPath;
    public static SaveManager instance;

    [Header("* Components")]
    #region Components
    public DeckPickUpManager deckPickUpManager;
    public PlayerDeckManager playerDeckManager;
    public GoodsScoreManager goodsScoreManager;
    public WaveManager waveManager;
    public CharUpgradeManager charUpgradeManager;
    public AssembleSkillManager assembleSkillManager;
    public QuestManager questManager;
    #endregion

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    } // Get instance & Don't Destroy On Load

    void Start()
    {
        dataPath = Path.Combine(Application.persistentDataPath, "gamedata.json");
        LoadData();
        StartCoroutine(SaveDataPeriodically());
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SaveData();
        }
    }

    private IEnumerator SaveDataPeriodically()
    {
        while (true)
        {
            SaveData();
            Debug.Log("Auto Saved");
            yield return new WaitForSeconds(10.0f);
        }
    }

    public void SaveData()
    {
        GameData data = new GameData();

        SaveDataCheck(data);

        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(dataPath, json);
    }

    private void LoadData()
    {
        if (File.Exists(dataPath))
        {
            string json = File.ReadAllText(dataPath);
            GameData data = JsonUtility.FromJson<GameData>(json);
            Debug.Log("Loaded JSON: " + json);
            LoadDataCheck(data);
            Debug.Log("Data Loaded");
        }
        else
        {
            Debug.Log(dataPath);
            deckPickUpManager.LoadIcons();
            playerDeckManager.LoadDeck(0);
            playerDeckManager.LoadDeck(1);
            playerDeckManager.LoadDeck(2);
            playerDeckManager.LoadDeck(3);
            Debug.LogWarning("Save file not found!");
        }
    }

    public void SaveDataCheck(GameData gameData)
    {
        gameData.playerCharIDs = GetUniqueIDs(deckPickUpManager.playerChar);
        gameData.pickedValue = deckPickUpManager.pickedValue;

        gameData.inGameDeckIDs = GetUniqueIDs(playerDeckManager.inGameDeck);
        gameData.inGameCharIDs = GetUniqueIDs(playerDeckManager.inGameChar);

        gameData.rebirthStone = goodsScoreManager.goods.RebirthStone;
        gameData.coin = goodsScoreManager.goods.Coin;
        gameData.diamond = goodsScoreManager.goods.Diamond;
        gameData.autoStone = goodsScoreManager.goods.AutoStone;

        gameData.currentStage = waveManager.current_Stage;
        gameData.currentWave = waveManager.current_Wave;

        gameData.currentUpgradeFirerate = charUpgradeManager.currentUpgradeFirerate;
        gameData.needUpgradeCost = charUpgradeManager.needCharUpgradeCost;

        gameData.skillSlotIDs = GetUniqueIDs(assembleSkillManager.skillSlot);
        gameData.currentQuest = questManager.currentQuest;
    }

    public void LoadDataCheck(GameData gameData)
    {
        deckPickUpManager.playerChar = FindGameObjectsByIDs(gameData.playerCharIDs);
        deckPickUpManager.pickedValue = gameData.pickedValue;
        deckPickUpManager.LoadIcons();

        playerDeckManager.inGameDeck = FindGameObjectsByIDs(gameData.inGameDeckIDs);
        playerDeckManager.inGameChar = FindGameObjectsByIDs(gameData.inGameCharIDs);
        playerDeckManager.LoadDeck(0);
        playerDeckManager.LoadDeck(1);
        playerDeckManager.LoadDeck(2);
        playerDeckManager.LoadDeck(3);

        goodsScoreManager.goods.RebirthStone = gameData.rebirthStone;
        goodsScoreManager.goods.Coin = gameData.coin;
        goodsScoreManager.goods.Diamond = gameData.diamond;
        goodsScoreManager.goods.AutoStone = gameData.autoStone;

        waveManager.current_Stage = gameData.currentStage;
        waveManager.current_Wave = gameData.currentWave;

        assembleSkillManager.skillSlot = FindGameObjectsByIDs(gameData.skillSlotIDs);
        charUpgradeManager.currentUpgradeFirerate = gameData.currentUpgradeFirerate;
        charUpgradeManager.needCharUpgradeCost = gameData.needUpgradeCost;
        charUpgradeManager.SetDefalutPlayer();
        questManager.currentQuest = gameData.currentQuest;
    }

    private List<string> GetUniqueIDs(List<GameObject> gameObjects)
    {
        List<string> uniqueIDs = new List<string>();
        foreach (GameObject obj in gameObjects)
        {
            if (obj != null)
            {
                UniqueIdentifier identifier = obj.GetComponent<UniqueIdentifier>();
                if (identifier != null)
                {
                    uniqueIDs.Add(identifier.uniqueID);
                }
                else
                {
                    Debug.LogWarning($"GameObject {obj.name} does not have a UniqueIdentifier component.");
                }
            }
        }
        return uniqueIDs;
    }

    private string[] GetUniqueIDs(GameObject[] gameObjects)
    {
        List<string> uniqueIDs = new List<string>();
        foreach (GameObject obj in gameObjects)
        {
            if (obj != null)
            {
                UniqueIdentifier identifier = obj.GetComponent<UniqueIdentifier>();
                if (identifier != null)
                {
                    uniqueIDs.Add(identifier.uniqueID);
                }
                else
                {
                    Debug.LogWarning($"GameObject {obj.name} does not have a UniqueIdentifier component.");
                }
            }
        }
        return uniqueIDs.ToArray();
    }

    private List<GameObject> FindGameObjectsByIDs(List<string> uniqueIDs)
    {
        List<GameObject> gameObjects = new List<GameObject>();
        foreach (string id in uniqueIDs)
        {
            GameObject obj = FindGameObjectByID(id);
            if (obj != null)
            {
                gameObjects.Add(obj);
            }
            else
            {
                gameObjects.Add(null);
                Debug.LogWarning($"GameObject with ID {id} not found.");
            }
        }
        return gameObjects;
    }

    private GameObject FindGameObjectByID(string uniqueID)
    {
        GameObject[] allObjects = FindObjectsOfType<GameObject>();
        foreach (GameObject obj in allObjects)
        {
            UniqueIdentifier identifier = obj.GetComponent<UniqueIdentifier>();
            if (identifier != null && identifier.uniqueID == uniqueID)
            {
                return obj;
            }
        }
        Debug.LogWarning($"GameObject with uniqueID {uniqueID} not found in the scene.");
        return null;
    }

    private GameObject[] FindGameObjectsByIDs(string[] uniqueIDs)
    {
        List<GameObject> gameObjects = new List<GameObject>();
        foreach (string id in uniqueIDs)
        {
            GameObject obj = FindGameObjectByID(id);
            if (obj != null)
            {
                gameObjects.Add(obj);
            }
            else
            {
                gameObjects.Add(null);
                Debug.LogWarning($"GameObject with ID {id} not found.");
            }
        }
        return gameObjects.ToArray();
    }
}
