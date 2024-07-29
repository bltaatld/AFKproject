using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTimeManager : MonoBehaviour
{
    public static GameTimeManager instance;

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

    public void TimeStart()
    {
        Time.timeScale = 1f;
    }

    public void TimePause()
    {
        Time.timeScale = 0f;
    }
}
