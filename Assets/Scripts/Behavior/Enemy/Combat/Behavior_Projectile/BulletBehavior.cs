using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    public float destoryTime;
    private float currentTime;

    public void Update()
    {
        currentTime += Time.deltaTime;

        if (currentTime >= destoryTime)
        {
            Destroy(gameObject);
        }
    }
}
