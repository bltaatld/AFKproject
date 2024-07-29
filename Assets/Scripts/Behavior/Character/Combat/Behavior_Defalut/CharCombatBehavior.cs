using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharCombatBehavior : MonoBehaviour
{
    [Header("* Components")]
    public GameObject projectilePrefab;
    public Transform spawnPoint;
    private Transform closestTarget;
    private Coroutine fireCoroutine;

    [Header("* Values")]
    public string nameID;
    public string shootTargetTag = "Enemy";
    public float projectileSpeed = 10f;
    public float detectionRadius = 10f;
    public float fireRate = 1f; 

    void Update()
    {
        FindClosestTarget();

        if (closestTarget != null && fireCoroutine == null)
        {
            fireCoroutine = StartCoroutine(FireAtTarget());
        }
        else if (closestTarget == null && fireCoroutine != null)
        {
            StopCoroutine(fireCoroutine);
            fireCoroutine = null;
        }
    }

    void FindClosestTarget()
    {
        GameObject[] targets = GameObject.FindGameObjectsWithTag(shootTargetTag);
        float closestDistance = detectionRadius;
        closestTarget = null;

        foreach (GameObject target in targets)
        {
            float distance = Vector2.Distance(transform.position, target.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestTarget = target.transform;
            }
        }
    }

    IEnumerator FireAtTarget()
    {
        while (closestTarget != null)
        {
            GameObject projectile = Instantiate(projectilePrefab, spawnPoint.position, spawnPoint.rotation);

            Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                Vector2 direction = (closestTarget.position - spawnPoint.position).normalized;
                rb.velocity = direction * projectileSpeed;
            }

            yield return new WaitForSeconds(1f / fireRate);
        }
    }
}
