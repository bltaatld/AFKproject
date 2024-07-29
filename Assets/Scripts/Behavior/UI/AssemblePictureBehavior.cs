using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssemblePictureBehavior : MonoBehaviour
{
    public int ID_currentSkill;
    public GameObject ActivateSkillPrefab;
    private AssembleSkillManager skillManager;

    private void Start()
    {
        skillManager = GameObject.Find("SkillManager").GetComponent<AssembleSkillManager>();
    }

    public void SetSelectedSkill() 
    {
        if (!skillManager.isClicked)
        {
            SetSkillManagerValue();
            skillManager.isClicked = true;
            return;
        }

        if(skillManager.isClicked)
        {
            if (skillManager.ID_selectedSkill == ID_currentSkill && skillManager.selectedSkill != gameObject)
            {
                Instantiate(ActivateSkillPrefab);
                DestorySkillPiture();
                ResetSkillManagerValue();
            }
            else
            {
                ResetSkillManagerValue();
            }
        }
    }

    private void DestorySkillPiture()
    {
        skillManager.current_SkillPitureCount -= 2;
        Destroy(skillManager.selectedSkill);
        Destroy(gameObject);
    }

    private void SetSkillManagerValue()
    {
        skillManager.ID_selectedSkill = ID_currentSkill;
        skillManager.selectedSkill = gameObject;
    }

    private void ResetSkillManagerValue()
    {
        skillManager.ID_selectedSkill = 0;
        skillManager.selectedSkill = null;
        skillManager.isClicked = false;
    }
}
