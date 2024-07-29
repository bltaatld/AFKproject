using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBuffSkillBehavior : MonoBehaviour
{
    public int buffValue;
    public float remainTime;
    private float currentTime;

    public GameObject[] FindAllObjectsWithCharCombatBehavior()
    {
        // CharCombatBehavior 클래스를 가지는 모든 컴포넌트를 찾습니다.
        CharCombatBehavior[] charCombatBehaviors = FindObjectsOfType<CharCombatBehavior>();

        // CharCombatBehavior 컴포넌트를 가지는 게임 오브젝트 배열을 생성합니다.
        GameObject[] objects = new GameObject[charCombatBehaviors.Length];

        for (int i = 0; i < charCombatBehaviors.Length; i++)
        {
            objects[i] = charCombatBehaviors[i].gameObject;
        }

        return objects;
    }

    // 테스트를 위한 예제 함수
    private void Start()
    {
        BuffFireRate();
    }

    private void Update()
    {
        currentTime += Time.deltaTime;
        if(currentTime > remainTime)
        {
            ResetFireRate();
            Destroy(gameObject);
        }
    }

    private void BuffFireRate()
    {
        GameObject[] objectsWithCharCombatBehavior = FindAllObjectsWithCharCombatBehavior();
        foreach (GameObject obj in objectsWithCharCombatBehavior)
        {
            obj.GetComponent<CharCombatBehavior>().fireRate += buffValue;
        }
    }

    private void ResetFireRate()
    {
        GameObject[] objectsWithCharCombatBehavior = FindAllObjectsWithCharCombatBehavior();
        foreach (GameObject obj in objectsWithCharCombatBehavior)
        {
            obj.GetComponent<CharCombatBehavior>().fireRate -= buffValue;
        }
    }
}
