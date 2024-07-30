using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameData
{
    // Player Deck Info
    public List<GameObject> playerChar = new List<GameObject>();
    public List<int> pickedValue = new List<int>();
    public GameObject[] inGameDeck;
    public GameObject[] inGameChar;

    // Goods Data
    public double rebirthStone;
    public double coin;
    public double diamond;
    public double autoStone;

    // Wave Data
    public int currentStage;
    public int currentWave;

    // Upgrade Data
    public int[] upgradeCount;

    // Skill Data
    public GameObject[] skillSlot;

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
    } //Get instance & Don't Destory On Load

    void Start()
    {
        dataPath = Path.Combine(Application.persistentDataPath, "gamedata.json");
        Debug.Log(dataPath);
        LoadData();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            SaveData();
        }
    }

    public void SaveData()
    {
        GameData data = new GameData();
        
        SaveDataCheck(data);

        string json = JsonUtility.ToJson(data, true);
        Debug.Log(dataPath);
        File.WriteAllText(dataPath, json);
    }

    public GameData LoadData()
    {
        dataPath = Path.Combine(Application.persistentDataPath, "gamedata.json");

        if (File.Exists(dataPath))
        {
            string json = File.ReadAllText(dataPath);
            GameData data = JsonUtility.FromJson<GameData>(json);
            LoadDataCheck(data);
            Debug.Log("Data Loaded");
            return data;
        }
        else
        {
            Debug.Log(dataPath);
            Debug.LogWarning("Save file not found!");
            return null;
        }
    }

    public void SaveDataCheck(GameData gameData)
    {
        gameData.playerChar = deckPickUpManager.playerChar;
        gameData.pickedValue = deckPickUpManager.pickedValue;

        gameData.inGameDeck = playerDeckManager.inGameDeck;
        gameData.inGameChar = playerDeckManager.inGameChar;

        gameData.rebirthStone = goodsScoreManager.goods.RebirthStone;
        gameData.coin = goodsScoreManager.goods.Coin;
        gameData.diamond = goodsScoreManager.goods.Diamond;
        gameData.autoStone = goodsScoreManager.goods.AutoStone;

        gameData.currentStage = waveManager.current_Stage;
        gameData.currentWave = waveManager.current_Wave;

        gameData.skillSlot = assembleSkillManager.skillSlot;
        gameData.currentQuest = questManager.currentQuest;
    }

    public void LoadDataCheck(GameData gameData)
    {
        deckPickUpManager.playerChar = gameData.playerChar;
        deckPickUpManager.pickedValue = gameData.pickedValue;
        deckPickUpManager.LoadIcons();

        playerDeckManager.inGameDeck = gameData.inGameDeck;
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

        assembleSkillManager.skillSlot = gameData.skillSlot;
        questManager.currentQuest = gameData.currentQuest;
    }
}
