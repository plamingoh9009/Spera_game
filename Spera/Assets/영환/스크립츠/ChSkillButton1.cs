using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChSkillButton1 : MonoBehaviour
{
    public bool isFlashing { get; set; }

    public void OnClickFlashingButton()
    {
        if (isFlashing == false)
        {
            isFlashing = true;
        }
    }
}
