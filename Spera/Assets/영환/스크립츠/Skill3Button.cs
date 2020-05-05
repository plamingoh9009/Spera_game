using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill3Button : MonoBehaviour
{
    public bool isSummonCrow { get; set; }

    public void SummonCrow()
    {
        if (isSummonCrow == false)
        {
            isSummonCrow = true;
        }
    }
}
