using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookPointMove : MonoBehaviour
{

    public bool IsStart = false;
    public float _moveSpeed = 5f;

    void Update()
    {
        if (!IsStart) return;
        //시작되면 룩포인트를 왼쪽으로 쭉 옮겨줌
        transform.Translate(-_moveSpeed * Time.deltaTime, 0, 0);
       
        if (transform.localPosition.x <= -17.0f) IsStart = false;

    }
}
