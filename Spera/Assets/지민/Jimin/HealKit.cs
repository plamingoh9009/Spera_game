using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealKit : Item
{
    public override void Start()
    {
        AddItem(2, 1, "회복키트", "회복 한다.");
        base.Start();
    }
    public override void Use(GameObject target)
    {
        //회복
    }
}
