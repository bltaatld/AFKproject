using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldBuffSkill : MonoBehaviour
{
    public float remainTime;
    private float currentTime;
    private WaveManager waveManager;

    private void Start()
    {
        waveManager = GameObject.Find("CombatManager").GetComponent<WaveManager>();
    }
    private void Update()
    {
        currentTime += Time.deltaTime;
        if (currentTime > remainTime)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            waveManager.current_EnemyKillCount += 1;
            Destroy(collision.gameObject);
        }
    }
}
