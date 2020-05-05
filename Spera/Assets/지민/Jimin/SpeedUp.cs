using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUp :  Item
{
    public override void Start()
    {
        AddItem(1, 0, "속도업", "5초간 속도가 증가한다.");
        base.Start();   
    }
    public override void Use(GameObject target)
    {
        //타겟의 속도가 빨라짐
        StartCoroutine(SpeedUpItem(target));
    }

    IEnumerator SpeedUpItem(GameObject target)
    {
        //5초뒤 속도 원래대로 돌아옴
        target.GetComponent<charactorMovingCamera>().speed += 10;
        yield return new WaitForSeconds(5);
        target.GetComponent<charactorMovingCamera>().speed -= 10;
    }
}
