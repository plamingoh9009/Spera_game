using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowMoving : MonoBehaviour
{
   [SerializeField]
   private enum Direct
   {
       STAY , HORIZONTAL ,VERTICAL
   }

    public float speed = 0.1f;

    [SerializeField]
    private float moveRange = 0.5f;
    private Transform originalPosition;
    private float tempHorizontalRange = 0.0f;
    private float tempVerticalRange = 0.0f;
    private bool isHmove = false;


    [SerializeField]
    Direct crowDirect;

    private int randomNum = 1;

    private void Awake()
    {
        crowDirect = Direct.HORIZONTAL;
        originalPosition = this.transform; // 원래 좌표(중심점)
    }

    private void FixedUpdate()
    {
        //랜덤 방향별로 움직인다.
        if (randomNum == 0) crowDirect = Direct.STAY;
        if (randomNum == 1) crowDirect = Direct.HORIZONTAL;
        if (randomNum == 2) crowDirect = Direct.VERTICAL;
       

        // 방향 설정
        switch (crowDirect)
        {
            case Direct.STAY: 
                break;

            case Direct.HORIZONTAL:
                if (tempHorizontalRange <= 5.0f && isHmove == false)
                {
                    StartCoroutine("returnWaitTime");
                    this.transform.Translate(0, 0, speed);
                    tempHorizontalRange += 0.5f;
                }
                else if(tempHorizontalRange == 5.0)
                {
                    StartCoroutine("returnWaitTime");
                    isHmove = true;
                }
              
                if(isHmove == true)
                {
                    this.transform.Translate(0, 0, -speed);
                    tempHorizontalRange -= 0.5f;
                }

                if(isHmove == true && this.transform.position.z == originalPosition.position.z)
                {
                    isHmove = false;

                }

                break;

            case Direct.VERTICAL:
                break;
        }

    }
    
    IEnumerator returnWaitTime()
    {
        yield return new WaitForSeconds(3f);
    }
    
    // 시간단위로 랜덤하게 방향이 나온다.
    IEnumerator waitRandom()
    {
        yield return new WaitForSeconds(5f);
        randomNum = Random.Range(0, 2);
    }
}
