using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerHealthManager : MonoBehaviour
{
    [Header("* Components")]
    public List<GameObject> collideEnemy = new List<GameObject>();
    public TextMeshProUGUI healthText;
    public GameObject deathUI;
    public WaveManager waveManager;
    public GoodsScoreManager scoreManager;

    [Header("* Values")]
    public int currentHealth;
    public int maxHealth;
    public float healthRecoveryTime;
    public float getDamageTime;
    private float healthTime;
    private float damageTime;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void Update()
    {
        healthText.text = currentHealth.ToString();
        TimingCheck();
        HealthCheck();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            if (!collideEnemy.Contains(collision.gameObject))
            {
                collideEnemy.Add(collision.gameObject);
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            if (collideEnemy.Contains(collision.gameObject))
            {
                collideEnemy.Remove(collision.gameObject);
            }
        }
    }

    public void RebirthBehavior()
    {
        currentHealth = maxHealth;
        scoreManager.goods.RebirthStone += waveManager.current_Stage * 100 + waveManager.current_Wave * 10;
        waveManager.ResetDefalutValue();
    }

    public void TimingCheck()
    {
        healthTime += Time.deltaTime;
        damageTime += Time.deltaTime;

        if (damageTime >= getDamageTime)
        {
            currentHealth -= collideEnemy.Count;

            if (collideEnemy.Count == 0)
            {
                collideEnemy.Clear();
            }

            damageTime = 0f;
        }

        if (healthTime >= healthRecoveryTime)
        {
            if (currentHealth < maxHealth)
            {
                currentHealth += 1;
                healthTime = 0f;
            }
        }
    }

    public void HealthCheck()
    {
        if(currentHealth <= 0)
        {
            GameTimeManager.instance.TimePause();
            deathUI.SetActive(true);
        }
    }
}
