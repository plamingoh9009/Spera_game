using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChSkillButton2 : MonoBehaviour
{
   public bool isHidePlayerShow { get; set; }

    public void Skill2Button()
    {
        if(isHidePlayerShow == false)
        {
            isHidePlayerShow = true;
        }
    }
}
