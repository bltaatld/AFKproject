using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombatBehavoir : MonoBehaviour
{
    [Header("* Components")]
    public GameObject dropItem;
    private WaveManager waveManager;

    [Header("* Values")]
    public int health;
    public float speed = 5f;

    private void Start()
    {
        waveManager = GameObject.Find("CombatManager").GetComponent<WaveManager>();
    }


    void Update()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);

        if(health <= 0)
        {
            waveManager.current_EnemyKillCount++;
            waveManager.total_EnemyKillCount++;
            Instantiate(dropItem, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            health--;
            Destroy(collision.gameObject);
        }
    }
}
