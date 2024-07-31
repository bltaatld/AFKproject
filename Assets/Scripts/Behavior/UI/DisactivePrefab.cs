using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisactivePrefab : MonoBehaviour
{
    public List<GameObject> PrefabList;
    public Slider cooldownSlider;

    private float currentTime;
    private float coolDown = 2.5f;
    private bool canSliderMove = true;

    private void Start()
    {
        if (cooldownSlider != null)
        {
            cooldownSlider.maxValue = coolDown;
            cooldownSlider.value = 0;
        }
    }

    private void Update()
    {
        currentTime += Time.deltaTime;

        if (cooldownSlider != null && canSliderMove)
        {
            cooldownSlider.value = currentTime;
        }

        if (currentTime >= coolDown)
        {
            CleanUp();
            currentTime = 0;

            if (cooldownSlider != null)
            {
                cooldownSlider.value = 0;
            }
        }
    }

    public void CleanUp()
    {
        canSliderMove = false;

        foreach (var obj in PrefabList)
        {
            if (obj != null)
            {
                obj.SetActive(false);
            }
        }
    }
}
