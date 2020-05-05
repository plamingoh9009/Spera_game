using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLight : Item
{
    public override void Start()
    {
        AddItem(3, 2, "손전등", "적에게 스턴을 준다.");
        base.Start();
    }

    public override void Use(GameObject target)
    {
        //공격
    }
}
